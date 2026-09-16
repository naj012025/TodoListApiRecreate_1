using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TodoListApiRecreate_1.Dto;

namespace TodoListApiRecreate_1.Services;

public sealed class AtmApiClient(HttpClient httpClient)
{
    public async Task<AtmAccountResponse?> GetMyAccountAsync(
        string accessToken,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage request =
            new(HttpMethod.Get, "api/accounts/me");

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        using HttpResponseMessage response =
            await httpClient.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException(
                "AtmApi Rejected the Forwarded JWT");
        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<AtmAccountResponse>(
            cancellationToken: cancellationToken);
    }
}

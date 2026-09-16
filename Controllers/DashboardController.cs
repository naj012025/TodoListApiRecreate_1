using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoListApiRecreate_1.Dto;
using TodoListApiRecreate_1.Services;
namespace TodoListApiRecreate_1.Controllers;

[ApiController]
[Route("api/Dashboard")]
[Authorize]
public sealed class DashboardController(
    DashboardService dashboardService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<DashboardResponse>> Get(
        CancellationToken cancellationToken)
    {
        int accountId = GetAccountId();
        string token = GetAccessToken();

        DashboardResponse response =
            await dashboardService.GetAsync(
                accountId, token, cancellationToken);

        return Ok(response);

    }

    private int GetAccountId()
    {
        string value =
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new UnauthorizedAccessException();

        return int.Parse(value);
    }

    private string GetAccessToken()
    {
        string authorization =
            Request.Headers.Authorization.ToString();
        const string prefix = "Bearer ";

        if (!authorization.StartsWith(
            prefix, StringComparison.OrdinalIgnoreCase))
        {
            throw new UnauthorizedAccessException();
        }

        return authorization[prefix.Length..].Trim();
    }
}

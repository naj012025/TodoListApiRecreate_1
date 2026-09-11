using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Text;
using TodoListApiRecreate_1.Data;
using TodoListApiRecreate_1.Services;



var builder = WebApplication.CreateBuilder(args);


// Controllers
builder.Services.AddControllers();




// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "DefaultConnection")));


// Services
builder.Services.AddScoped<TodoService>();


// JWT Authentication
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };

        //this is to check where im failing in auth.
        //options.Events = new JwtBearerEvents
        //{
        //    OnAuthenticationFailed = context =>
        //    {
        //        Console.WriteLine("JWT auth failed");
        //        Console.WriteLine(context.Exception.Message);

        //        return Task.CompletedTask;
        //    },
        //    OnTokenValidated = context =>
        //    {
        //        Console.WriteLine("JWT success");
        //        return Task.CompletedTask;
        //    }
        //};
    });

builder.Services.AddAuthorization();

builder.Services.AddOpenApi(options =>
{

    options.AddDocumentTransformer<
        BearerSecuritySchemeTransformer>();


    options.AddOperationTransformer<
        BearerSecurityRequirementTransformer>();
});



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

internal sealed class BearerSecuritySchemeTransformer(
    IAuthenticationSchemeProvider authenticationSchemeProvider)
    : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var authenticationSchemes =
            await authenticationSchemeProvider
                .GetAllSchemesAsync();

        bool hasBearerScheme =
            authenticationSchemes.Any(
                scheme =>
                    scheme.Name ==
                    JwtBearerDefaults.AuthenticationScheme);

        if (!hasBearerScheme)
            return;

        var securitySchemes =
            new Dictionary<string, IOpenApiSecurityScheme>
            {
                ["Bearer"] =
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header
                    }
            };

        document.Components ??=
            new OpenApiComponents();

        document.Components.SecuritySchemes =
            securitySchemes;
    }
}


internal sealed class BearerSecurityRequirementTransformer
    : IOpenApiOperationTransformer
{
    public Task TransformAsync(
        OpenApiOperation operation,
        OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        var metadata =
            context.Description
                .ActionDescriptor
                .EndpointMetadata;

        bool requiresAuthorization =
            metadata
                .OfType<IAuthorizeData>()
                .Any();

        bool allowsAnonymous =
            metadata
                .OfType<IAllowAnonymous>()
                .Any();

        if (!requiresAuthorization || allowsAnonymous)
            return Task.CompletedTask;

        operation.Security ??= [];

        operation.Security.Add(
            new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference(
                    "Bearer",
                    context.Document)] = []
            });

        return Task.CompletedTask;
    }
}
using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using TimeProject.APIs.Configurations;
using TimeProject.APIs.Controllers.Middlewares;

var builder = WebApplication.CreateBuilder(args);

const string customCorsName = "_customCors";
builder.Services.AddCorsConfiguration(customCorsName);

builder.Services
    .AddServicesConfiguration(builder.Configuration)
    .AddCustomAuthorizationConfiguration(builder.Configuration);

builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddOpenApi(o => o
        .AddDocumentTransformer<BearerSecuritySchemeTransformer>()
        .AddScalarTransformers());

builder.Services.AddDatabaseConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error-development");

    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseCors(customCorsName);
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<UserChallengeMiddleware>();
app.MapControllers();

app.Run();
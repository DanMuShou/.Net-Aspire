var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.AddServiceDefaults();
builder
    .Services.AddAuthentication()
    .AddKeycloakJwtBearer(
        serviceName: "keycloak",
        realm: "overflow",
        options =>
        {
            // 不允许https, 防止Docker签名复杂
            options.RequireHttpsMetadata = false;
            options.Audience = "overflow";
        }
    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();
app.MapDefaultEndpoints();

app.Run();

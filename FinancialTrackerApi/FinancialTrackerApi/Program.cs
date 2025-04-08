using System.Text.Json.Serialization;
using FinancialTrackerApi;
using FinancialTrackerApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Add authentication with Auth0
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-k0sl1xaa1o87ofbn.us.auth0.com/";
    options.Audience =
        "https://dev-k0sl1xaa1o87ofbn.us.auth0.com/api/v2/"; // This should be the Identifier of your API in Auth0
});

// CORS settings
const string allowSpecificOrigins = "clientOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowSpecificOrigins, policy =>
    {
        policy.AllowAnyOrigin() // Allow requests from any origin
            .AllowAnyMethod() // Allow any HTTP method
            .AllowAnyHeader(); // Allow any header
    });
});

// parsed json enum values from its int position to its description
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// MongoDB configuration
var mongoDbSettings = builder.Configuration
    .GetSection("MongoDbSettings")
    .Get<MongoDbSettings>();

builder.Services.AddSingleton(mongoDbSettings);
builder.Services.AddSingleton<FinancialTrackerDbContext>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
/*builder.Services.AddDbContext<FinancialTrackerDbContext>(options =>
    options.UseMongoDB(
        mongoDbSettings?.AtlasUri ?? "",
        mongoDbSettings?.DatabaseName ?? ""));*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseCors(allowSpecificOrigins);
// nginx settings
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
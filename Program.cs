using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using M223PunchclockDotnet.Service;
using M223PunchclockDotnet.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


// MVC
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Databse
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql("Host=db;Database=postgres;Username=postgres;Password=postgres"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JwtAuthApp API", Version = "v1" });

    // Füge eine Sicherheitsdefinition für JWT (Bearer) hinzu
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Lege die Sicherheitsanforderung fest
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// Services & Repositories
builder.Services.AddScoped<EntryService, EntryService>();
builder.Services.AddScoped<CategoryService, CategoryService>();
builder.Services.AddScoped<TagService, TagService>();
builder.Services.AddScoped<TestDataRepository, TestDataRepository>();

// JWT-Authentifizierung
var publicKey = RSA.Create();
publicKey.ImportFromPem(System.IO.File.ReadAllText("Keys/public.key"));
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new RsaSecurityKey(publicKey) // Verifizierung mit öffentlichem Schlüssel
        };
    });
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DatabaseContext>();
    context.Database.EnsureCreated();
    
    //DbInitializer.Initialize(context); // TODO: Comment this line to stop dropping the db on startup.
    //DbInitializer.InsertExampleData(context);
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

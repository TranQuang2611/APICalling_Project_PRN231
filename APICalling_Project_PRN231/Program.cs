using APICalling_Project_PRN231;
using APICalling_Project_PRN231.AccessDataFromDatabase;
using APICalling_Project_PRN231.DTO;
using APICalling_Project_PRN231.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .SetMaxTop(20)
        .Count()
        .Expand()
    ); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
    
builder.Services.AddDbContextFactory<ReviewStoreContext>(o => o.UseSqlServer("Server=localhost;Database=ReviewStore;user=sa;password=12345678"), ServiceLifetime.Transient);

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<BrandRepository>();
builder.Services.AddScoped<ColorRepository>();
builder.Services.AddScoped<SizeRepository>();
builder.Services.AddScoped<RamRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CommentRepository>();
builder.Services.AddScoped<ReviewRepository>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
               {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
               });
});

builder.Services.Configure<AppSetttings>(builder.Configuration.GetSection("AppSettings"));
var secretKey = builder.Configuration["AppSettings:SecretKey"];
var secretKeyByte = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
    {
        // tự cấp token nên không cần
        ValidateIssuer = false,
        ValidateAudience = false,

        //ký vào token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),
        ClockSkew = TimeSpan.Zero
    });


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMvc(routeBuilder =>
//{
//    routeBuilder.EnableDependencyInjection();
//    routeBuilder.Select().OrderBy().Filter();
//});

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();

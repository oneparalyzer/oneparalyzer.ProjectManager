using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using oneparalyzer.ProjectManager.Auth.Api.Entities;
using oneparalyzer.ProjectManager.Auth.Api.Helpers.Implementations;
using oneparalyzer.ProjectManager.Auth.Api.Helpers.Interfaces;
using oneparalyzer.ProjectManager.Auth.Api.Options;
using oneparalyzer.ProjectManager.Auth.Api.Persistance;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));
    builder.Services.AddIdentity<User, Role>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    var jwtOptions = new JwtTokenOptions();
    builder.Configuration.Bind(JwtTokenOptions.SectionName, jwtOptions);
    builder.Services.AddSingleton(Options.Create(jwtOptions));
    builder.Services.AddScoped<IJwtTokenHelper, JwtTokenHelper>();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
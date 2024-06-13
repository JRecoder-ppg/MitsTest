using Microsoft.EntityFrameworkCore;
using MitsTest.Data;
using MitsTest.Models;
using MitsTest.Services;

var builder = WebApplication.CreateBuilder(args);

var test = Environment.GetEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("PsqlConnection");

SecretManagerService secretManager = new SecretManagerService();
CredentialsRDS credentials = await secretManager.GetSecret<CredentialsRDS>("rds!cluster-19c13237-fe19-495e-8b9e-cd4c81d1034e", "us-east-2");
connectionString = connectionString!.Replace("@username@", credentials.username).Replace("@password@", credentials.password);
builder.Services.AddDbContext<DataContext>(db=>db.UseNpgsql(connectionString, mig => mig.MigrationsHistoryTable("__EFMigrationsHistory","tests")));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

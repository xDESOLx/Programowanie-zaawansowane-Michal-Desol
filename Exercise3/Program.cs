using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Exercise3.Data;
using System.Text.Json.Serialization;
using NuGet.Packaging;
using Exercise3.Converters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Exercise3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Exercise3Context") ?? throw new InvalidOperationException("Connection string 'Exercise3Context' not found.")));

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

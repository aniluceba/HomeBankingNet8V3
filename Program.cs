using HomeBankingNet8V3.Models;
using HomeBankingNet8V3.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers().AddJsonOptions(x =>
x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

// Add DbContext to the container

builder.Services.AddDbContext<HomeBankingContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("HbAppDbConnection")));

builder.Services.AddScoped<IClientRepository, ClientRepository>();

var app = builder.Build();


// Create a scope to get the DbContext instance

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<HomeBankingContext>();
    DbInitializer.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
    }
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapRazorPages();

app.Run();

app.UseDefaultFiles();
app.UseStaticFiles();


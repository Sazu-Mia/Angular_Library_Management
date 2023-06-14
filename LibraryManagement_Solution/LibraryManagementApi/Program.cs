using LibraryManagementApi.HostedServices;
using LibraryManagementApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LMS")));
builder.Services.AddScoped<LibraryDbInitializer>();
builder.Services.AddHostedService<LibraryDbSeederHostedService>();
builder.Services.AddCors(op =>
{
    op.AddPolicy(name:"EnableCors", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        

    });
});
builder.Services.AddControllers().AddNewtonsoftJson(op =>
{
    op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
    op.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
});
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseCors("EnableCors");
app.MapDefaultControllerRoute();

app.Run();

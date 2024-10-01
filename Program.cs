using Azure.Storage.Files.Shares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ST10310998_CLDV6212_POE__Part_1.Data;
using ST10310998_CLDV6212_POE__Part_1.Services;
using Azure.Storage.Blobs;
using Azure.Storage.Queues;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureSqlDatabase")));

// Register Azure Blob Service and Queue Service
builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("AzureStorage:BlobConnectionString")));
builder.Services.AddSingleton(x => new QueueServiceClient(builder.Configuration.GetConnectionString("AzureStorage:QueueConnectionString")));
builder.Services.AddSingleton(x => new ShareServiceClient(builder.Configuration.GetConnectionString("AzureStorage:FileConnectionString")));

// Register custom services
builder.Services.AddTransient<BlobStorageService>();
builder.Services.AddTransient<QueueStorageService>();
builder.Services.AddTransient<FileService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

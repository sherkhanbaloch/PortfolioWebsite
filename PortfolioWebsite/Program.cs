using Microsoft.EntityFrameworkCore;
using PortfolioWebsite.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();

var con = builder.Configuration.GetConnectionString("dbcs");
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(con));
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

app.Run();

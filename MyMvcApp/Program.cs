using MyMvcApp.Models;
using MyMvcApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddSingleton<User>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
} else {
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=AccountInfo}/{id?}");

app.Run();

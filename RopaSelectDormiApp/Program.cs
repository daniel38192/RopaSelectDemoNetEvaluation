using Microsoft.AspNetCore.Authorization;
using RopaSelectDormiApp.Dao.Clothe;
using RopaSelectDormiApp.Dao.ClotheList;
using RopaSelectDormiApp.Dao.ClotheListElement;
using RopaSelectDormiApp.Service.Clothe;
using RopaSelectDormiApp.Service.ClotheList;
using RopaSelectDormiApp.Service.ClotheListElement;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using RopaSelectDormiApp.Data;
using RopaSelectDormiApp.Entities.Role;
using RopaSelectDormiApp.Entities.User;
using RopaSelectDormiApp.Service.User;

var builder = WebApplication.CreateBuilder(args);

const string connectionString = "Host=localhost;Port=6432;Username=postgres;Password=postgres;Database=ropaDormiSelectDB"; 
var dataSource = NpgsqlDataSource.Create(connectionString);
builder.Services.AddSingleton(dataSource);

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(dataSource)
);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IClothesDao, ClothesDaoImpl>();
builder.Services.AddSingleton<IClothesService, ClothesServiceImpl>();

builder.Services.AddSingleton<IClothesListDao, ClothesListDaoImpl>();
builder.Services.AddSingleton<IClothesListService, ClothesListServiceImpl>();

builder.Services.AddSingleton<IClothesListElementDao, ClothesListElementDaoImpl>();
builder.Services.AddSingleton<IClothesListElementService, ClothesListElementServiceImpl>();

builder.Services.AddScoped<IUserService, UserServiceImpl>();

builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = "/Account";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorizationBuilder()
    .SetFallbackPolicy(new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
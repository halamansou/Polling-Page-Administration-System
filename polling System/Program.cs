using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using polling_System.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<polling_SystemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PollingSystem"));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<polling_SystemContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireRole("Admin");
    });

    options.AddPolicy("User", policy =>
    {
        policy.RequireRole("User");
    });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//    if (context.User.Identity.IsAuthenticated)
//    {
//        var userManager = context.RequestServices.GetRequiredService<UserManager<IdentityUser>>();
//        var user = await userManager.GetUserAsync(context.User);
//        var roles = await userManager.GetRolesAsync(user);

//        var requestPath = context.Request.Path.ToString().ToLower();

//        if (roles.Contains("Admin") && !requestPath.StartsWith("/polls"))
//        {
//            context.Response.Redirect("/Polls/Index");
//            return;
//        }
//        else if (roles.Contains("User") && !requestPath.StartsWith("/client/lastpoll"))
//        {
//            context.Response.Redirect("/Client/LastPoll");
//            return;
//        }
//    }

//    await next();
//});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

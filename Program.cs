using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SymphonyExpressApi.Data;
using SymphonyExpressApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables(prefix: "SYMPHONYEXPRESS_");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// TODO: Configure the Email Sender
// builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
// builder.Services.Configure<MailKitEmailSenderOptions>(options =>
// {
//     options.Host_Address = builder.Configuration["Authentication:SMTP:HostAddress"];
//     options.Host_Port = 587;
//     options.Host_Username = builder.Configuration["Authentication:SMTP:HostUserName"];
//     options.Host_Password = builder.Configuration["Authentication:SMTP:HostPassword"];
//     options.Sender_EMail = "admin@theblink.network";
//     options.Sender_Name = "admin@theblink.network";
// });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
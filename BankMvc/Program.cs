using AppDomainAppService.Card;
using AppDomainAppService.Transaction;
using AppDomainAppService.User;
using AppDomainCore.Contract.Card;
using AppDomainCore.Contract.Transaction;
using AppDomainCore.Contract.User;
using AppDomainService.Card;
using AppDomainService.Transaction;
using AppDomainService.User;
using Configuration.BankDb;
using Microsoft.EntityFrameworkCore;
using Repository.Card;
using Repository.Transaction;
using Repository.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddDbContext<BankDbContext>(option =>
//    option.UseSqlServer(
//        @"Server=DESKTOP-78B19T2\SQLEXPRESS; Initial Catalog=cw-18; User Id=sa; Password=13771377; TrustServerCertificate=True;"));

builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IUsreRepository, UserRepository >();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICardAppSerevice, CardAppService>();
builder.Services.AddScoped<ITransactionAppService, TransactionAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddDbContext<BankDbContext>();


var app = builder.Build();







// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

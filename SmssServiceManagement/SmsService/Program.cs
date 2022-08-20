using Microsoft.EntityFrameworkCore;
using SmsService.Data;
using SmsService.Midlewares;
using SmsService.Serivces.Provider;
using SmsService.Serivces.SMS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var smsServiceConnectionString = builder.Configuration.GetConnectionString("SmsServiceConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(smsServiceConnectionString));

builder.Services.AddScoped<ISMSService, SMSService>();
builder.Services.AddScoped<ProviderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.ConfigureCustomExceptionHandler();

app.MapControllers();

app.Run();

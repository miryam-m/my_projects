using BL;
using DAL;
using DAL.models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//כאן אני מחזיקה את כל התלויות שבפרויקט שלי
//התלות הכללית שכל המחלקות של דל יצטרכו אותה
builder.Services.AddDbContext<flyForYouContext>();

builder.Services.AddScoped<Booking_DAL>();
builder.Services.AddScoped<Booking_BL>();

builder.Services.AddScoped<BookingDetails_DAL>();
builder.Services.AddScoped<BookingDetails_BL>();

builder.Services.AddScoped<Flight_DAL>();
builder.Services.AddScoped<FlightBl>();

builder.Services.AddScoped<FlightDetails_DAL>();
builder.Services.AddScoped<FlightDetails_BL>();

builder.Services.AddScoped<Passengers_DAL>();
builder.Services.AddScoped<Passengers_BL>();

builder.Services.AddScoped<Payment_DAL>();
builder.Services.AddScoped<Payment_BL>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using PillPalAPI.Model;
using PillPalAPI.Repositories;
using PillPalLib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IDataStore, DataStore>();
builder.Services.AddScoped<IItemStore<Medicine>, MedicineRepository>();
builder.Services.AddScoped<IItemStore<Reminder>, ReminderRepository>();
builder.Services.AddScoped<IItemStore<User>, UserRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

using ComercioInteriorV1.Data.Helper;
using ComercioInteriorV1.Data.Implementations;
using ComercioInteriorV1.Data.Interfaces;
using ComercioInteriorV1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DataHelper>(sp =>
new DataHelper(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IArticulosRepository, ArticulosRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturasRepository>();
builder.Services.AddScoped<ArticuloService>();
builder.Services.AddScoped<FacturaService>();
builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

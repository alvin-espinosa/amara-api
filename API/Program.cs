using Amara.Microservice.Configuration;
using Amara.Microservice.Shared;
using Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMicroServiceConfiguration(builder.Configuration);
builder.Services.AddDatabaseConfiguration<RentingContext>(builder.Configuration);
builder.Services.AddEmailServices(builder.Configuration);
builder.Services.AddCacheServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAmaraConfiguration(builder.Configuration);
app.UseDatabaseConfiguration<RentingContext>();

app.MapControllers()
   .RequireAuthorization();

app.Run();

using JukeBoxPartyAPI.Data;
using JukeBoxPartyAPI.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container. Comment before deploy
builder.Services.AddCors(o => o.AddPolicy(name: MyAllowSpecificOrigins, policy => {
    policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
    .WithOrigins("http://145.44.235.126/",
    "https://145.44.235.126");
}));
builder.Services.AddTransient<IQueueElementsRepository, QueueElementRepository>();
builder.Services.AddScoped<QueueElementRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<MyDbContext>(x => x.UseLazyLoadingProxies().UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);
app.Run();

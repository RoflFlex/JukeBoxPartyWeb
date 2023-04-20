using JukeBoxPartyAPI.Data;
using JukeBoxPartyAPI.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add services to the container.
builder.Services.AddCors(o => o.AddPolicy(name: MyAllowSpecificOrigins, policy => {
    policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
    .WithOrigins("http://localhost:7188");
}));


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
    
}
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);
app.Run();

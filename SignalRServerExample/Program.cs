using SignalRServerExample.Business;
using SignalRServerExample.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
					policy.AllowAnyMethod()
						  .AllowAnyHeader()
						  .AllowCredentials()
						  .SetIsOriginAllowed(origin => true)
));

builder.Services.AddTransient<MyBusiness>();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

// https://localhost:7122/myHub => MyHub �ma bu �ekilde ba�lanabilirm.
app.MapHub<MyHub>("/myHub");
app.MapHub<MessageHub>("/messageHub");

app.Run();

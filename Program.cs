using PuntoDeVenta_API.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInjections();

var app = builder.Build();

//Enable CORS

app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

using PuntoDeVenta_API.DependencyInjections;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container by going to AddInjections method.

builder.Services.AddInjections();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.UseSwagger()
        .UseSwaggerUI()
        .UseHttpsRedirection()
        .UseAuthentication()
        .UseAuthorization()
        .UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

app.MapControllers();

app.Run();

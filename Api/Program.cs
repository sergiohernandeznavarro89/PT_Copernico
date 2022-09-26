using AutoMapper;
using Database.BD.Context;
using Microsoft.EntityFrameworkCore;
using Service.Helpers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

// Auto Mapper Configurations
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapperProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//configure context
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Añade los servicios y los repositorios empleando Scrutor
builder.Services.Scan(scan => scan
    .FromAssemblies(Assembly.Load("Service"), Assembly.Load("Database"))
    .AddClasses(c => c.Where(d =>
        d.Name.EndsWith("Service")
        || d.Name.EndsWith("Repository")
        || d.Name.EndsWith("Factory")))
    .AsImplementedInterfaces()
    .WithScopedLifetime());

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MyContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

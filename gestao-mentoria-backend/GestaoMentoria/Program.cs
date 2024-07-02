using GestaoMentoria.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using GestaoMentoria.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>((Action<DbContextOptionsBuilder>?)(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        Console.WriteLine("Using MySQL");
        UseMySQL(options);
    }
    else
    {
        Console.WriteLine("Using PostgreSQL");
        UsePostgresql(options);
    }
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


// Use a política de CORS
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void UseMySQL(DbContextOptionsBuilder options)
{
    // mysql
    var strConnection = "server=localhost;database=gestaodementoriadb;user=root;password=root;port=3306;";
    options.UseMySQL(strConnection) 
           .LogTo( Console.WriteLine, new[] {
    RelationalEventId.CommandExecuted, CoreEventId.ContextInitialized
    },
    LogLevel.Information,
    DbContextLoggerOptions.LocalTime | DbContextLoggerOptions.SingleLine
    );
}

static void UsePostgresql(DbContextOptionsBuilder options)
{
    // postgresql
    var strConnectionRenderDb = "Host=dpg-cps4hoij1k6c738herc0-a.virginia-postgres.render.com;Username=gtmuser;Password=zZQpokGPhZS3o8DBjSCf7yAESOOtokIb;Database=gestaodementoriapostgresql;Port=5432";
    options.UseNpgsql(strConnectionRenderDb); 
}
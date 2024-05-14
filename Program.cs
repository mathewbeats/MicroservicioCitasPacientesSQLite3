using CitasMedicas.Repository;
using CitasMedicas.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//builder.Services.AddScoped<ISqlRepository, SqlRepository>();

// Configurar la cadena de conexión
var connectionString = builder.Configuration.GetConnectionString("SqliteConnectionString");

// Registrar tu repositorio
builder.Services.AddScoped<ISqlRepository, SqlRepository>(serviceProvider =>
    new SqlRepository(connectionString));


// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://example.com")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Usar CORS
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();

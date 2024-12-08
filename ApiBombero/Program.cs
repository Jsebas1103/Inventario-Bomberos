using Entities;
using repositories;

var builder = WebApplication.CreateBuilder(args);
// Añadir políticas CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", policy =>
    {
        policy.WithOrigins("https://localhost:7265")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddTransient<IBomberoRepository,BomberoRepository>();
builder.Services.AddTransient<IPrestamoRepository,PrestamoRepository>();
builder.Services.AddTransient<IElementoRepository,ElementoRepository>();
builder.Services.AddTransient<ICategoriaRepository,CategoriaRepository>();
builder.Services.AddTransient<IDetallePrestamoRepository,DetallePrestamoRepository>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Usar CORS
app.UseCors("AllowBlazorWasm");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapControllers();

app.Run();

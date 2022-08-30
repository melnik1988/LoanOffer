
using LoanOffer.Interfaces;
using LoanOffer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Services
builder.Services.AddTransient<ILoadLogic, LoanLogicService>();
builder.Services.AddTransient<IHelp, HelpService>();
builder.Services.AddSingleton<IFile, FileService>();

// Add Service CORS
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Read from configuration file
IConfiguration configuration = app.Configuration;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
        options => options.WithOrigins(configuration.GetSection("AppSettings:Origin").Value).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
    );

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();

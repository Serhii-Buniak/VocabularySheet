using VocabularySheet.Application;
using VocabularySheet.CambridgeDictionary;
using VocabularySheet.Infrastructure;
using VocabularySheet.ReversoContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCambridgeDictionary();
builder.Services.AddReversoContext();
// builder.Services.AddInfrastructureServices(new()
// {
//     DataDirectory = @"C:\Users\Administrator\AppData\Local\Packages\58081b13-9b5a-4853-a7d3-0eb7306c2f3f_75cr2b68sm664\LocalState"
// });
// builder.Services.AddApplicationServices();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
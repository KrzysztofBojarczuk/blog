using blogWebApi.Data;
using blogWebApi.InterfaceReposiotry;
using blogWebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins",
        builder =>
        {
            builder
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod();

        });
}
);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CORSPolicy",
//        builder =>
//        {
//            builder
//            .WithOrigins("http://localhost:3000")
//            .AllowAnyOrigin()
//            .AllowAnyMethod()
//            .AllowAnyHeader();

//        });
//}
//);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<IPostRepository, PostRepository>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();

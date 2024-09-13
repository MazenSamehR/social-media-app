using Microsoft.OpenApi.Models;
using SocailMediaApp.Models;
using SocailMediaApp.Repositories;
using SocailMediaApp.Services;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<List<User>>();
builder.Services.AddSingleton<List<Post>>();
builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<PostRepository>();
builder.Services.AddSingleton<FollowingManagementService>();
builder.Services.AddSingleton<PostService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.ExampleFilters(); 
}); 
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();


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

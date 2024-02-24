using Api.Endpoints;
using Application.Articles.CreateArticle;
using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(CreateArticleCommand).Assembly,
    typeof(ApplicationDbContext).Assembly));

builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

builder.Services.AddDbContext<ApplicationDbContext>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapArticleEndpoints();

app.UseHttpsRedirection();

app.Run();
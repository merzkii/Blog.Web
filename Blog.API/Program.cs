using Blog.API.Filters;
using Blog.Application.Common.Interfaces;
using Blog.Application.Features.BlogPosts.Commands;
using Blog.Application.Features.BlogPosts.Validators.Behaviour.Blog.Application.Common.Behaviors;
using Blog.Application.Features.BlogPosts.Validators.Blog;
using Blog.Domain.Interfaces;
using Blog.Infrastucture.Data;
using Blog.Infrastucture.Data.Repositories;
using Blog.Infrastucture.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
  
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBlogRepository, BlogPostRepository>();
builder.Services.AddScoped<IDateService, DateTimeService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BlogDb"));
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePost).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(CreatePostValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("DefaultScheme")
    .AddCookie("DefaultScheme", options =>
    {
        options.LoginPath = "/api/auth/login";
        options.AccessDeniedPath = "/api/auth/denied";
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        };
    });
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Seeds.Seed(context);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

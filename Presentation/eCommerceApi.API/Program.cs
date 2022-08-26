using eCommerceApi.Application.Repositories.Validators.Products;
using eCommerceApi.Infrastructure.Filters;
using eCommerceApi.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

//Add Services IoC
builder.Services.AddPersistenceServices();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy=>
{
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
}));

builder.Services.AddControllers(option => option.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration =>
        configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options=> options.SuppressModelStateInvalidFilter = true);
    
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();


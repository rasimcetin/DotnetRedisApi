using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);
{

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var redisConnectionString = builder.Configuration.GetValue<string>("Redis:ConnectionString");
    builder.Services.AddSingleton(new RedisCacheService(redisConnectionString));
    builder.Services.AddSingleton(new ProductRepository());
}

var app = builder.Build();
{
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
}

app.Run();


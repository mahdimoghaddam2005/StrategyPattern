using StrategyPattern.Domain;
using StrategyPattern.Infrastructure;
using StrategyPattern.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Use Controllers
builder.Services.AddControllers();

// SwaggerGen
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Define Dbcontext For Services
builder.Services.AddDbContext<SmsContext>();

// Add Services and Strategy
builder.Services.AddScoped<ISmsSender,KavehnegarSender>();
builder.Services.AddScoped<ISmsSender,TabanSender>();
builder.Services.AddScoped<List<ISmsSender>>(serviceProvider => 
{
    List<ISmsSender> senders=new List<ISmsSender>();
    foreach (var sender in serviceProvider.GetServices<ISmsSender>())
    {
        senders.Add(sender);
    }
    return senders;
});
builder.Services.AddScoped<SenderStrategy,SenderStrategy>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Https
app.UseHsts();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

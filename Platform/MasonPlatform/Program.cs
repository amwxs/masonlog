using MasonPlatform;
using MasonPlatform.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers(o =>
{
    o.Filters.Add<MasonExceptionFilter>();//全局异常处理
}).AddFluentValidation();//模型参数验证

//注册
builder.Services.AddScoped<ILogEntryService, LogEntryService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

using Tethr.Demo.Uploader;
using Tethr.Sdk;
using Tethr.Sdk.Heartbeat;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole();

builder.Services.AddTethr();
builder.Services.AddTethrHeartBeatService();
builder.Services.AddHostedService<UploadCallWorker>();

var host = builder.Build();

host.Run();
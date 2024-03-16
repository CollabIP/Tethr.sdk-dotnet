using Tethr.Demo.FolderUploader;
using Tethr.Sdk;
using Tethr.Sdk.Heartbeat;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTethr();
builder.Services.AddTethrHeartBeatService();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
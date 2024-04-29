using Amazon.SQS;
using Customers.Consumer;
using MediatR;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services.Configure<QueueSettings>(builder.Configuration.GetSection(QueueSettings.Key));

builder.Services.AddSingleton<IAmazonSQS>(_ => {
    return new AmazonSQSClient(config.GetValue<string>("AWSCredentials:AccessKey"),
        config.GetValue<string>("AWSCredentials:SecritKey"));
});
builder.Services.AddHostedService<QueueConsumerService>();
builder.Services.AddMediatR(typeof(Program));

var app = builder.Build();


app.Run();

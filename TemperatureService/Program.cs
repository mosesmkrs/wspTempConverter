using SoapCore;

var builder = WebApplication.CreateBuilder(args);

// Register the SOAP service
builder.Services.AddSoapCore();
builder.Services.AddSingleton<ITemperatureService, TemperatureService>();

var app = builder.Build();

// Enable routing and SOAP endpoint
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<ITemperatureService>(
        path: "/TemperatureService.asmx",
        encoder: new SoapEncoderOptions(),
        serializer: SoapSerializer.XmlSerializer
    );
});

app.Run();
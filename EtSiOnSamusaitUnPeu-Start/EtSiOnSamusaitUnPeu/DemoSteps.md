
# Ajouter OpenTelemetry 

## Ajouter les packages nuget 

```cmd
dotnet add package OpenTelemetry.Extensions.Hosting
dotnet add package OpenTelemetry.Instrumentation.AspNetCore
dotnet add package OpenTelemetry.Exporter.Console
```


## modifier le code 
```csharp
//DEBUT \\\///
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
//FIN ///\\\


var builder = WebApplication.CreateBuilder(args);

//DEBUT \\\///
const string serviceName = "casino-demo";

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddConsoleExporter();
});
builder.Services.AddOpenTelemetry()
      .ConfigureResource(resource => resource.AddService(serviceName))
      .WithTracing(tracing => tracing
          .AddAspNetCoreInstrumentation()
          .AddConsoleExporter()
          )
      .WithMetrics(metrics => metrics
          .AddAspNetCoreInstrumentation()
          .AddConsoleExporter()
          );

//FIN ///\\\
var app = builder.Build();


```

# Utilisation du DashBoard OTEL Aspire

## Ajouter le package
```cmd
dotnet add package OpenTelemetry.Exporter.OpenTelemetryProtocol
```

Modifier le code pour ajouter :

```csharp
//DEBUT \\\///

    builder.Services.AddOpenTelemetry().UseOtlpExporter();

//FIN ///\\\
var app = builder.Build();
    
```



## Définir les var d'environnement 

```cmd
SET OTEL_EXPORTER_OTLP_PROTOCOL=grpc
SET OTEL_EXPORTER_OTLP_ENDPOINT=http://localhost:4317
```
 ou les ajouter dans Properties/launchSettings.json
 ```json
 { 
    "...",
    "https": {
      "...",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development",
        "OTEL_EXPORTER_OTLP_PROTOCOL" : "grpc",
        "OTEL_EXPORTER_OTLP_ENDPOINT" : "http://localhost:4317"
      }
      "...",
    }
    "...",
 }
 ```

 ## démarrer le projet
 ```cmd
 dotnet run
 ```

 # Avec Azure

 ## Modifier le projet pour cibler Azure 
 ```cmd
 dotnet add package Azure.Monitor.OpenTelemetry.AspNetCore
 ```

 Remplacer _builder.Services.AddOpenTelemetry().UseOtlpExporter();_ par 
 ```csharp
 builder.Services.AddOpenTelemetry().UseAzureMonitor();
 ```

 Définissez la variable d'environnement 
 ```cmd
 SET APPLICATIONINSIGHTS_CONNECTION_STRING=LA_CNX_STRING_AZURE_APP_INSIGHT
 ```
 ou la définir dans Properties/launchSettings.json
 ```json
 { 
    "...",
    "https": {
      "...",
      "environmentVariables": {
        "...",
        "APPLICATIONINSIGHTS_CONNECTION_STRING" : "LA_CNX_STRING_AZURE_APP_INSIGHT"
      }
      "...",
    }
    "...",
 }
 ```

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.OTelAvecAspire>("otelavecaspire");

builder.Build().Run();

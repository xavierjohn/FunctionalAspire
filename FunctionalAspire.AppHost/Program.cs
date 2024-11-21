var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.FunctionalAspire_ApiService>("apiservice");

builder.AddProject<Projects.FunctionalAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();

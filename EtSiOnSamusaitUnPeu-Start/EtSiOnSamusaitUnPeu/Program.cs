using EtSiOnSamusaitUnPeu;


var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", () => Results.Extensions.Html("""<html><head><title>Le casino</title></head><body><h1>Plus Petit ou plus Grand ?? : <A href="/jouons">Jouons !</a></h1></body></html> """));

app.MapGet("/jouons" , (ILogger<Program> logger) => {
    var newValue = Random.Shared.Next(100);
    logger.LogWarning($"newvalue= {newValue}");
    return Results.Extensions.Html($"""<html><head><title>R&eacute;sultat</title></head><body><h1 style="font-size:500px;">{newValue}</h1></body></html>""");
});


app.Run();

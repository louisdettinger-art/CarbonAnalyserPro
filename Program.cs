var builder = WebApplication.CreateBuilder(args);

// Ajouter les services
builder.Services.AddControllers();

var app = builder.Build();

// --- Configuration des fichiers statiques ---
app.UseDefaultFiles(); // Cherche index.html
app.UseStaticFiles();  // Autorise l'envoi des fichiers du dossier wwwroot

app.UseAuthorization();

app.MapControllers();

// SECURITÉ : Si aucune page n'est trouvée, force l'affichage de index.html
// C'est souvent ce qui manque pour que ça marche du premier coup
app.MapFallbackToFile("index.html");

// Démarrage dynamique pour Render
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
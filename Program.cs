var builder = WebApplication.CreateBuilder(args);

// Ajouter les services (les contrôleurs pour le calcul)
builder.Services.AddControllers();

var app = builder.Build();

// --- C'est ici que ça se joue ---
app.UseDefaultFiles(); // 1. Dire que "index.html" est la page d'accueil par défaut
app.UseStaticFiles();  // 2. Autoriser l'envoi des fichiers HTML/CSS/JS (ceux dans wwwroot)
// -------------------------------

app.UseAuthorization();

app.MapControllers(); // Active les calculs API

// Démarrer l'application (écoute sur le port défini par Render)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
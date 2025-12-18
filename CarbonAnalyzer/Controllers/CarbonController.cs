using Microsoft.AspNetCore.Mvc;

namespace CarbonIndexPro.Controllers
{
    public class CarbonController : Controller
    {
        // 1. LA NOUVELLE PAGE D'ACCUEIL (Le choix)
        public IActionResult Index()
        {
            return View(); // Affiche la page de choix (Particulier ou Entreprise)
        }

        // 2. LE COULOIR "PARTICULIER"
        public IActionResult Particulier()
        {
            return View(); // Affiche le formulaire pour les gens
        }

        // 3. LE COULOIR "ENTREPRISE"
        public IActionResult Entreprise()
        {
            return View(); // Affiche le formulaire pour les pros
        }

        // ... Ici on mettra ensuite les méthodes de calcul (CalculerParticulier et CalculerEntreprise)
    }
}
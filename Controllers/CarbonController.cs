using Microsoft.AspNetCore.Mvc;

namespace CarbonAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarbonController : ControllerBase
    {
        public class CarbonRequest
        {
            public string Secteur { get; set; }
            public int NombreEmployes { get; set; }
            public string CodePostal { get; set; }
            public string TypeChauffage { get; set; }
            public string IntensiteTransport { get; set; }
            public string IntensiteAchats { get; set; }
        }

        public class CarbonResponse
        {
            public double TotalCo2 { get; set; }
            public double PartTransport { get; set; }
            public double PartEnergie { get; set; }
            public double PartAchats { get; set; }
            public string ConseilAction { get; set; }
            public string ExplicationCalcul { get; set; }

            // NOUVEAU : La partie Monétisation
            public string TitreBouton { get; set; } // Ex: "Comparer les pompes à chaleur"
            public string LienPartenaire { get; set; } // Le lien d'affiliation
        }

        [HttpPost("calculer")]
        public ActionResult<CarbonResponse> Calculer([FromBody] CarbonRequest request)
        {
            if (request.NombreEmployes <= 0) return BadRequest("Effectif invalide");

            // --- 1. CALCULS (Identique à avant) ---
            double emissionParEmploye = 2.0;
            double p_ener = 30, p_trans = 30, p_achats = 40;

            switch (request.Secteur)
            {
                case "bureau": emissionParEmploye = 1.2; p_ener = 40; p_trans = 25; p_achats = 35; break;
                case "tech": emissionParEmploye = 1.8; p_ener = 50; p_trans = 10; p_achats = 40; break;
                case "commerce": emissionParEmploye = 3.5; p_ener = 30; p_trans = 15; p_achats = 55; break;
                case "btp": emissionParEmploye = 8.0; p_ener = 15; p_trans = 35; p_achats = 50; break;
                default: emissionParEmploye = 4.0; break;
            }

            double totalBase = request.NombreEmployes * emissionParEmploye;
            double t_energie = totalBase * (p_ener / 100);
            double t_transport = totalBase * (p_trans / 100);
            double t_achats = totalBase * (p_achats / 100);

            if (request.TypeChauffage == "fioul") t_energie *= 1.5;
            else if (request.TypeChauffage == "electricite") t_energie *= 0.8;
            else if (request.TypeChauffage == "autre") t_energie *= 0.9;

            if (request.IntensiteTransport == "faible") t_transport *= 0.5;
            if (request.IntensiteTransport == "fort") t_transport *= 1.8;

            if (request.IntensiteAchats == "faible") t_achats *= 0.7;
            if (request.IntensiteAchats == "fort") t_achats *= 1.5;

            double totalFinal = t_energie + t_transport + t_achats;

            // --- 2. INTELLIGENCE COMMERCIALE ---

            string conseil = "";
            string btnText = "";
            string btnLink = "";

            double maxVal = Math.Max(t_energie, Math.Max(t_transport, t_achats));

            // SCÉNARIO 1 : Le problème est l'ENERGIE
            if (maxVal == t_energie)
            {
                if (request.TypeChauffage == "fioul" || request.TypeChauffage == "gaz")
                {
                    conseil = "🚨 URGENCE CHAUFFAGE : Vous perdez de l'argent avec les énergies fossiles.";
                    btnText = "🔥 Obtenir 3 devis Pompe à Chaleur (Gratuit)";
                    btnLink = "https://www.quelleenergie.fr/pompe-a-chaleur"; // Exemple lien affiliation
                }
                else
                {
                    // Électricité
                    conseil = "⚡ OPTIMISATION : Vos factures d'électricité plombent votre bilan.";
                    btnText = "💡 Comparer les offres d'électricité verte (-10%)";
                    btnLink = "https://www.hellowatt.fr"; // Exemple lien affiliation
                }
            }
            // SCÉNARIO 2 : Le problème est le TRANSPORT
            else if (maxVal == t_transport)
            {
                conseil = "🚗 FLOTTE AUTOMOBILE : Vos véhicules thermiques vous coûtent cher en TVS et carburant.";
                btnText = "🔋 Simuler le passage à une flotte électrique";
                btnLink = "https://www.arval.fr"; // Exemple leasing
            }
            // SCÉNARIO 3 : Le problème sont les ACHATS
            else
            {
                conseil = "💻 MATÉRIEL & IT : L'achat de matériel neuf alourdit votre empreinte.";
                btnText = "♻️ Voir les offres de matériel reconditionné Pro";
                btnLink = "https://www.backmarket.fr/fr-fr/professional"; // Exemple
            }

            return Ok(new CarbonResponse
            {
                TotalCo2 = Math.Round(totalFinal, 1),
                PartEnergie = Math.Round(t_energie, 1),
                PartTransport = Math.Round(t_transport, 1),
                PartAchats = Math.Round(t_achats, 1),
                ExplicationCalcul = $"Calcul basé sur {request.NombreEmployes} ETP (Secteur : {request.Secteur}).",
                ConseilAction = conseil,
                TitreBouton = btnText,
                LienPartenaire = btnLink
            });
        }
    }
}
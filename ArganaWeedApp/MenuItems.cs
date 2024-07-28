using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArganaWeedApp.Views;

namespace ArganaWeedApp
{
    public static class MenuItems
    {
        public static Dictionary<string, List<Tuple<string, string, string, string, Type>>> GetMenuItems()
        {
            return new Dictionary<string, List<Tuple<string, string, string, string, Type>>>
            {
                {
                    "Viewer", new List<Tuple<string, string, string, string, Type>>
                    {
                        new Tuple<string, string, string, string, Type>("1. Dashboard", "Consultation", "Tableau de bord", "dashboard.png", typeof(PlantulesPage))
                    }
                },
                {
                    "Agent", new List<Tuple<string, string, string, string, Type>>
                    {
                        new Tuple<string, string, string, string, Type>("2.1 Mon inventaire", "Exploitation", "Gérer votre inventaire", "inventaire.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.2 Scanner QR", "Exploitation", "Scanner les codes QR", "qrscan.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.3 Consulter plantule", "Exploitation", "Consulter les plantules", "weed.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.4 Ajouter plantule", "Exploitation", "Ajouter de nouvelles plantules", "weed.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.5 Generer plantule QR", "Exploitation", "Générer des codes QR pour plantules", "weed.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.6 Editer plantule", "Exploitation", "Éditer les plantules", "weed.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.7 Emplacements", "Exploitation", "Gérer les emplacements", "weed.png", typeof(EmplacementsPage)),
                        new Tuple<string, string, string, string, Type>("2.8 Varietes", "Exploitation", "Gérer les variétés", "weed.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.9 Provenances", "Exploitation", "Gérer les provenances", "weed.png", typeof(PlantulesPage))
                    }
                },
                {
                    "Owner", new List<Tuple<string, string, string, string, Type>>
                    {
                        new Tuple<string, string, string, string, Type>("3.1 Infos laboratoire", "Paramétrages", "Informations sur le laboratoire", "weed.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("3.2 ImporterPlantules", "Paramétrages", "Importer des plantules", "weed.png", typeof(PlantulesPage))
                    }
                },
                {
                    "Administrator", new List<Tuple<string, string, string, string, Type>>
                    {
                        new Tuple<string, string, string, string, Type>("4.1 Consulter utilisateurs", "Administration", "Consulter les utilisateurs", "weed.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("4.2 Ajouter utilisateur", "Administration", "Ajouter de nouveaux utilisateurs", "weed.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("4.3 Modifier utilisateur", "Administration", "Éditer les utilisateurs", "weed.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("4.4 Autres settings", "Administration", "Autres réglages", "weed.png", typeof(PlantulesPage))
                    }
                }
            };
        }
    }
}

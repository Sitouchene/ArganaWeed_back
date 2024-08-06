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
                        new Tuple<string, string, string, string, Type>("2.1 Mes plantules", "Exploitation", "Gérer mes plantules", "inventaire.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.2 Scanner plantule QR", "Exploitation", "Consulter une plantule par scan QR", "qrscan.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.3 Consulter plantule", "Exploitation", "Consulter une plantule par code", "weedview.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.4 Ajouter plantule", "Exploitation", "Ajouter une nouvelle plantule", "weedadd.png", typeof(PlantuleNewPage)),
                        new Tuple<string, string, string, string, Type>("2.5 Générer QR plantule", "Exploitation", "Générer code QR pour plantule et envoyer en pdf", "qradd.png", typeof(QrTest)),
                        new Tuple<string, string, string, string, Type>("2.6 Editer plantule", "Exploitation", "Modifier, mettre ", "weededit.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("2.7 Emplacements", "Exploitation", "Gérer les emplacements", "emplacement.png", typeof(EmplacementsPage)),
                        new Tuple<string, string, string, string, Type>("2.8 Varietes", "Exploitation", "Gérer les variétés", "varietes.png", typeof(VarietesPage)),
                        new Tuple<string, string, string, string, Type>("2.9 Provenances", "Exploitation", "Gérer les provenances", "provenances.png", typeof(ProvenancesPage))
                    }

                },
                {
                    "Owner", new List<Tuple<string, string, string, string, Type>>
                    {
                        new Tuple<string, string, string, string, Type>("3.1 Infos laboratoire", "Paramétrages", "Informations sur le laboratoire", "laboinfos.png", typeof(LaboInfoDetailPage)),
                        new Tuple<string, string, string, string, Type>("3.2 Importer Plantules", "Paramétrages", "Importer des plantules depuis fichiers plats", "weedimport.png", typeof(ImportPlantulesPage))
                    }
                },
                {
                    "Administrator", new List<Tuple<string, string, string, string, Type>>
                    {
                        new Tuple<string, string, string, string, Type>("4.1 Consulter utilisateurs", "Administration", "Consulter les utilisateurs", "users.png", typeof(UsersPage)),
                        new Tuple<string, string, string, string, Type>("4.2 Ajouter utilisateur", "Administration", "Ajouter de nouveaux utilisateurs", "useradd.png", typeof(UserNewPage)),
                        new Tuple<string, string, string, string, Type>("4.3 Modifier utilisateur", "Administration", "Éditer les utilisateurs", "useredit.png", typeof(PlantulesPage)),
                        new Tuple<string, string, string, string, Type>("4.4 Autres réglages", "Administration", "Autres réglages", "othersettings.png", typeof(PlantulesPage))
                    }
                }
            };
        }
    }
}

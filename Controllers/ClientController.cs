using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using MyApp.Data;

namespace canalsat.Controllers;

public class ClientController : Controller
{
    private readonly MyAppContext _dbContext;
    private readonly DbAccessor _DbAccessor;

    MyUtil util = new MyUtil();

    public ClientController(MyAppContext dbContext, DbAccessor dbAccessor)
    {
        _dbContext = dbContext;
        _DbAccessor = dbAccessor;
    }

    public IActionResult Modif_remise() {
        List<Abonnement> allAB = new Abonnement().getAbonnementsLastRemise();
        ViewData["allAB"] = allAB;
        return View();
    }

    /*public IActionResult listeBouquet() {
        List<Abonnement> allAB = new Abonnement().getAllAbonnements();
        ViewData["bouquet"] = allAB;
        return View();
    }*/

    public IActionResult Valide_ab_perso() {
        string[] chaines = Request.Form["chaines"];
        string idclient = Request.Form["idclient"];
        string descri_ab_perso = Request.Form["descri_ab_perso"];
        List<Chaine> listeChaine = util.toListeChaines(chaines);
        Client client = new Client().getClientById(idclient);
        ViewData["client"] = client;
        ViewData["chaines"] = listeChaine;
        return View();
    }

    public IActionResult Abonnement_perso(string idclient) {
        List<Chaine> chaines = new Chaine().getAllChaines();
        Client client = new Client().getClientById(idclient);
        List<Chaine> chainesDispo = client.GetChainesDispo(chaines);
        Abonnement abEnCours = client.GetAbonnementEncours();
        ViewData["abEnCours"] = abEnCours;
        ViewData["allChaines"] = chaines;
        ViewData["chainesDispo"] = chainesDispo;
        ViewData["client"] = client;
        return View();
    }

    public IActionResult Abonnement(string idclient, string idab)
    {
        Client client = new Client().getClientById(idclient);
        Abonnement ab = new Abonnement().getAbonnementById(idab);
        
        //traiter le nouvel abonnement
        AbonnementClient newAB = util.GetNewAbClient(client, ab);

        //inserer nouvel abonnement
        newAB.Insert();

        //RETOURNE LA VIEW DETAIL
        Abonnement abEnCours = client.GetAbonnementEncours();
        int id = int.Parse(idclient);
        var details = _DbAccessor.GetClientDetails(id);
        client.id = id;
        client = client.getClientById();
        ViewData["details"] = details;
        ViewData["client"] = client;
        var abs = _DbAccessor.AbonnementDispos(id);
        if(abEnCours == null) {
            ViewData["abs"] = abs;
        }
        else {
            List<AbonnementDispo> absDispo = new List<AbonnementDispo>();
            foreach (AbonnementDispo abd in abs)
            {
                if(abd.abonnement.getPrixRemise() >= abEnCours.getPrixRemise()) {
                    absDispo.Add(abd);
                }
            }
            ViewData["abs"] = absDispo;
        }
        ViewData["abEnCours"] = abEnCours;
        return View("Detail");
        //return View(clients);
    }

    /*public IActionResult reAB()
    {
        var abs = _dbContext.Abonnements.ToList();
        ViewData["abs"] = abs;
        return View();
        //return View(clients);
    }*/

    public IActionResult Detail(int id)
    {
        //var clients = _dbContext.Clients.ToList();
        var details = _DbAccessor.GetClientDetails(id);
        var client = new Client();
        client.id = id;
        client = client.getClientById();
        ViewData["details"] = details;
        ViewData["client"] = client;
        Abonnement abEnCours = client.GetAbonnementEncours();
        var abs = _DbAccessor.AbonnementDispos(id);
        if(abEnCours == null) {
            ViewData["abs"] = abs;
        }
        else {
            List<AbonnementDispo> absDispo = new List<AbonnementDispo>();
            foreach (AbonnementDispo ab in abs)
            {
                if(ab.abonnement.getPrixRemise() >= abEnCours.getPrixRemise()) {
                    absDispo.Add(ab);
                }
            }
            ViewData["abs"] = absDispo;
        }
        ViewData["abEnCours"] = abEnCours;
        return View();
        //return View(clients);
    }

    public IActionResult Index()
    {
        //var clients = _dbContext.Clients.ToList();
        var ClientListes = new Client().getAllClients();
        ViewData["ClientListes"] = ClientListes;
        return View();
        //return View(clients);
    }

    public IActionResult contact(){
        return View();
    }
}


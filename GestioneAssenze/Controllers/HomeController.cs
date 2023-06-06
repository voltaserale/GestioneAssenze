using GestioneAssenze.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GestioneAssenze.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("matricola") ==null)       //se non c'è loggato nessuno mostro la pagina di login
                return RedirectToAction("Login");
            else
            {
                ViewData["nomestudente"] = HttpContext.Session.GetString("nomestudente");
                return View();
            }
                
        }


        [HttpGet]
        public IActionResult Login()     //pagina /Home/Login       (richiesta pagina di login)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Studente studente)     //pagina /Home/Login (richiesta post quando l'utente clicca sul bottone login. In user ci sarà la username e la password)
        {
            using (ScuolaContext dbContext = new ScuolaContext())
            {
                Studente? loggedUser = dbContext.Studentes
                    .Where(u => 
                        u.Username==studente.Username && 
                        u.Password==studente.Password)      //cerco nel db un utente con username e password specificati
                    .FirstOrDefault();
                if (loggedUser == null)
                    return NotFound("Nome utente o password errata");
                else  //utente trovato=>lo memorizzo nella sessione
                {
                    //memorizzo il nome dell'utente all'interno della sessione
                    HttpContext.Session.SetString("matricola",loggedUser.Matricola);
                    HttpContext.Session.SetString("nomestudente", loggedUser.Nome + " "+loggedUser.Cognome);
                    
                    return RedirectToAction("Index"); //vado sulla pagina principale
                }

            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("matricola");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
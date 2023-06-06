using GestioneAssenze.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestioneAssenze.Controllers
{
    public class AssenzeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("matricola") == null)       //se non c'è loggato nessuno mostro la pagina di login
                return RedirectToAction("Login","Home");
            else
                using (ScuolaContext context = new ScuolaContext())
                {
                    DateTime inizioAnno = new DateTime(DateTime.Now.Year, 1, 1);
                    List<Assenza> elencoAssenze =
                        context.Assenzas.Where(a => a.Data>= inizioAnno &&
                        a.Matrstudente== HttpContext.Session.GetString("matricola")
                                     ).ToList();
                    ViewData["nomestudente"] = HttpContext.Session.GetString("nomestudente");
                    return View(elencoAssenze);
                }
           
        }
    }
}

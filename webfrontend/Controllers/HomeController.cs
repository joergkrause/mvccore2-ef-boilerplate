using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SodgeIt.WebFrontEnd.ServiceProxy;
using SodgeIt.Workshop.WebFrontEnd.Models;
using webfrontend.Models;

namespace webfrontend.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWorkshopAPI client;

        public HomeController(IWorkshopAPI client)
        {
            this.client = client;
        }

        public async Task<IActionResult> Index()
        {
            var model = await client.ApiCompanyUserGetAsync();
            // TODO: Benutze den Automapper!
            var viewmodel = new List<EmployeeViewModel>();
            foreach(var item in model){
                viewmodel.Add(new EmployeeViewModel{
                    Id = item.Id,
                    Name = item.N,
                    PhoneNumber = item.Pn
                });
            }
            return View(viewmodel);
        }

        public async Task<IActionResult> About(int id)
        {
            var item = await client.ApiCompanyUserByIdGetAsync(id);
            var viewmodel = new EmployeeViewModel{
                    Id = item.Id,
                    Name = item.N,
                    PhoneNumber = item.Pn
                };
            return View(viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> AboutSave(EmployeeViewModel model)
        {
            if (ModelState.IsValid){
                // TODO: Speichern
            }
            return View(model);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

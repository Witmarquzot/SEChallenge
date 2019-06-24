using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Http.Cors;
namespace HeathCarePayStubs.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

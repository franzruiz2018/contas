using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CONTAS.Models;

namespace CONTAS.Controllers
{
    
    public class AsistenciaController : Controller
    {

        CONTASEntities db = new CONTASEntities();
        // GET: /Asistencia/

        public ActionResult Index()
        {
            var list = db.sp_personas_que_marcaron_hoy();
            ViewBag.sp_personas_que_marcaron_hoy = list.ToList()    ;
            return View();
        }

    }
}

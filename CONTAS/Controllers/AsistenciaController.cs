using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CONTAS.Models;
using CONTAS.ClasesGlobales;

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

        public ActionResult Registrar()
        {
           
            return View();
        }

        [HttpPost]
        public JsonResult RegistrarMarcacion(int codigo)
        {
            //var duplicado = codigo * 2;
            //return Json(duplicado);

          
            tb_asistencia_p asistencia = new tb_asistencia_p();
            BaseRespuesta resultado = new BaseRespuesta();
           
            //Verificar si existe el codigo
            var result = db.tb_usuarios_colegio_m.Where(x => x.id_usuarios_colegio == codigo).Count();
            if (result > 0) { 
                try
                {
                    asistencia.id_usuarios_colegio = codigo;
                    asistencia.fecha_registro = DateTime.Now;

                    db.tb_asistencia_p.Add(asistencia);
                    db.SaveChanges();
                    resultado.Mensaje = "Se guardo Correctamente";
                    resultado.Ok = true;
                
                }
                catch (Exception ex)
                {                   
                    resultado.Mensaje = "Ocurrio un Error," + ex.Message;
                    resultado.Ok = false;
                }
            }
            else
            {
                resultado.Mensaje = "El usuario no se encuentra registrado";
                resultado.Ok = false; 
            }
            return Json(resultado);

        }

        public ActionResult Marcaciones()
        {

            return View();
        }

        public PartialViewResult ListaDeMarcaciones(tb_usuarios_colegio_m u)
        {
            var marcaciones = db.tb_asistencia_p.Where(x => x.id_usuarios_colegio == u.id_usuarios_colegio).ToList();
            return PartialView("_marcaciones",marcaciones);
        }


    }
}

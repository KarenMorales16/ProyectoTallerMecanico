using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LosInges.Models;
using Syncfusion.EJ2.Navigations;
using Newtonsoft.Json;
namespace LosInges.Controllers
{
    public class EmpleadosController : Controller
    {
        // GET: Empleados
        //
        private LosIngesEntities db = new LosIngesEntities();
        public ActionResult Index()
        {
            return View();
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Mostrar()
        {
            ViewBag.ListEmp = (from LE in db.Empleado
                               join d in db.Departamento on LE.IdDepartamento equals d.IdDepartamento
                               join P in db.Puesto on LE.IdPuesto equals P.IdPuesto
                               select new
                               {
                                   IdEmp = LE.IdEmpleado,
                                   NomEmp = LE.Nombre,
                                   ApPat = LE.ApPat,
                                   ApMat = LE.ApMat,
                                   DepaEmp = LE.IdDepartamento,
                                   PuestoEmp = LE.IdPuesto,
                                   DepaEmpNom = d.Descripcion,
                                   PuestoEmpNom = P.Descripcion
                               }).ToList();
            return View("Empleado");
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {

            ViewBag.ListPues = (from LP in db.Puesto
                                select new
                                {
                                    IdPues = LP.IdPuesto,
                                    DesPues = LP.Descripcion

                                }).ToList();

            ViewBag.ListDepa = (from LD in db.Departamento
                                select new
                                {
                                    IdDepa = LD.IdDepartamento,
                                    DesDepa = LD.Descripcion

                                }).ToList();
            return View("AltaEmpleado");
        }

        // POST: Empleados/Create
        [HttpPost]
        public ActionResult Create(Empleado model)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View("Create");
                }
                else
                {
                    //ObjectParameter OutPut = new ObjectParameter("Correcto", typeof(bool));
                    db.Alta_Empleado(model.Nombre, model.ApMat, model.ApPat, model.IdDepartamento, model.IdPuesto);
                    return RedirectToAction("Mostrar", "Empleados");
                }

            }
            catch
            {
                return View("Create");
            }

        }

        // GET: Empleados/Edit/5
        public ActionResult Update(int IdEmpleado)
        {
            ViewBag.ListPues = (from LP in db.Puesto
                                select new
                                {
                                    IdPues = LP.IdPuesto,
                                    DesPues = LP.Descripcion

                                }).ToList();

            ViewBag.ListDepa = (from LD in db.Departamento
                                select new
                                {
                                    IdDepa = LD.IdDepartamento,
                                    DesDepa = LD.Descripcion

                                }).ToList();
            Empleado empleado = db.Empleado.Where(x => x.IdEmpleado == IdEmpleado).FirstOrDefault<Empleado>();
            ViewBag.TipoCliente = empleado.IdPuesto;

            return View("EmpUpdate", empleado);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Update(Empleado Emp)
        {
            try
            {
                // TODO: Add insert logic here
                if (!ModelState.IsValid)
                {
                    return View("EmpUpdate");
                }
                else
                {

                    db.SP_Empleado_Update(Emp.IdEmpleado, Emp.Nombre, Emp.ApPat, Emp.ApMat, Emp.IdDepartamento, Emp.IdPuesto);
                    return RedirectToAction("Mostrar", "Empleados");
                }

            }
            catch
            {
                return View("EmpUpdate");
            }
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int IdEmpleado)
        {
            Empleado emp = db.Empleado.Where(x => x.IdEmpleado == IdEmpleado).FirstOrDefault<Empleado>();
            db.Empleado.Remove(emp);
            db.SaveChanges();
            return Json(new { mensaje = 1 }, JsonRequestBehavior.AllowGet);
        }



    }
}

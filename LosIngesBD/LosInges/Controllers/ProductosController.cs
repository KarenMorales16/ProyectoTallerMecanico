using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Se agrega el modelo
using LosInges.Models;
namespace LosInges.Controllers
{
    public class ProductosController : Controller
    {
        //
        private LosIngesEntities db = new LosIngesEntities();
        // GET: Productos
        public ActionResult Index()
        {
            return View();
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.ListProducto = (from LP in db.Producto
                                    select new
                                    {
                                        IdProducto = LP.IdProducto,
                                        Descripcion = LP.Descripcion,
                                        NoParte = LP.NoParte
                                    }).ToList();
            return View("Productos");
        }

        // POST: Productos/Create
        [HttpPost]
        public ActionResult Create(Producto Produc)
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

                    db.Alta_Producto(Produc.Descripcion, Produc.NoParte);
                    return RedirectToAction("Create", "Productos");
                }

            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int IdProducto)
        {
            Producto prod = db.Producto.Where(x => x.IdProducto == IdProducto).FirstOrDefault<Producto>();
            db.Producto.Remove(prod);
            db.SaveChanges();
            return Json(new { regreso = 1 }, JsonRequestBehavior.AllowGet);
        }

    }
}

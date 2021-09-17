using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Taller1_DSE_MM160953.Models;

namespace Taller1_DSE_MM160953.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: Products
        public ActionResult Index(string descripcion = "", int existencia = 0, double precio = 0)
        {
            var products = db.Products.Include(p => p.Category);
            if (!string.IsNullOrEmpty(descripcion))
                products = products.Where(p => p.Descripcion.ToUpper().Contains(descripcion.ToUpper()));
            if (existencia > 0)
                products = products.Where(p => p.Stock == existencia);
            if (precio > 0)
                products = products.Where(p => p.Price == precio);

            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (db.Categories.Count() == 0)
                return RedirectToAction("Create", "Categories");

            ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "Descripcion");
            return View();
        }

        // POST: Products/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduct,Descripcion,IdCategory,Cost,Price,Stock,NumberOfOrders")] Product product)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "Descripcion", product.IdCategory);
                return View(product);              
            }

            if (product.Stock < product.NumberOfOrders)
            {
                ModelState.AddModelError("", "No se puede agregar una cantidad de pedidos mayor que el stock");
                ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "Descripcion", product.IdCategory);
                return View(product);
            }

            product.Descripcion = product.Descripcion.ToUpper();
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "Descripcion", product.IdCategory);
            return View(product);
        }

        // POST: Products/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduct,Descripcion,IdCategory,Cost,Price,Stock,NumberOfOrders")] Product product)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "Descripcion", product.IdCategory);
                return View(product);            
            }

            if(product.Stock < product.NumberOfOrders)
            {
                ModelState.AddModelError("", "No se puede agregar una cantidad de pedidos mayor que el stock");
                ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "Descripcion", product.IdCategory);
                return View(product);
            }

            product.Descripcion = product.Descripcion.ToUpper();
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

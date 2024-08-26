using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp003_CodeFirst.DAL;
using WebApp003_CodeFirst.Models;

namespace WebApp003_CodeFirst.Controllers
{
    public class SalaController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Sala
        public ActionResult Index(string filter)
        {
            var sala = from s in db.Salas
                       select s;
            if (!string.IsNullOrEmpty(filter))
            {
                sala = db.Salas.Where(s => s.Nombre.Contains(filter));
            }

            return View(sala.ToList());
        }

        // GET: Sala/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = db.Salas.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // GET: Sala/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sala/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre,Capacidad,Recursos,Comentarios")] Sala sala)
        {
            try
            {
                var salaToFind = db.Salas.Any(s => s.Nombre == sala.Nombre);
                if (salaToFind)
                {
                    ModelState.AddModelError("", "Ya existe una sala con ese nombre");
                }
                if (ModelState.IsValid)
                {
                    db.Salas.Add(sala);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No se pudo crear correctamente, intentelo nuevamente");
            }

            return View(sala);
        }

        // GET: Sala/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = db.Salas.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // POST: Sala/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id is null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var salaToUpdate = db.Salas.Find(id);
            
            if (TryUpdateModel(salaToUpdate, "", 
                new string[] { "Nombre", "Capacidad", "Recursos", "Comentarios" }))
            {
                try
                {
                    var salaToFind = db.Salas.Any(s => s.Nombre == salaToUpdate.Nombre);
                    if (salaToFind)
                    {
                        ModelState.AddModelError("", "Ya existe una sala con ese nombre");
                        return View(salaToUpdate);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "No se pudo actualizar correctamente, vuelva a intentarlo");
                }
            }

            return View(salaToUpdate);
        }

        // GET: Sala/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sala sala = db.Salas.Find(id);
            if (sala == null)
            {
                return HttpNotFound();
            }
            return View(sala);
        }

        // POST: Sala/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sala sala = db.Salas.Find(id);
            db.Salas.Remove(sala);
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

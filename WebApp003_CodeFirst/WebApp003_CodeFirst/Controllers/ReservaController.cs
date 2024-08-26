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
    public class ReservaController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Reserva
        public ActionResult Index()
        {
            var reservas = db.Reservas.Include(r => r.Sala);
            return View(reservas.ToList());
        }

        // GET: Reserva/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Include(r => r.Sala).SingleOrDefault(r => r.ReservaId == id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reserva/Create
        public ActionResult Create()
        {
            ViewBag.SalaId = new SelectList(db.Salas, "SalaId", "Nombre");
            return View();
        }

        // POST: Reserva/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaId,FechaInicio,FechaFinal,Usuario")] Reserva reserva)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.Reservas.Any(r => r.SalaId == reserva.SalaId &&
                                             r.FechaInicio < reserva.FechaFinal &&
                                             reserva.FechaInicio < r.FechaFinal))
                    {
                        ModelState.AddModelError("", "La sala ya esta reservada en ese horario");
                        ViewBag.SalaId = new SelectList(db.Salas, "SalaId", "Nombre", reserva.SalaId);
                        return View(reserva);
                    }
                    db.Reservas.Add(reserva);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "No se pudo crear correctamente, intentelo nuevamente");
            }

            ViewBag.SalaId = new SelectList(db.Salas, "SalaId", "Nombre", reserva.SalaId);
            return View(reserva);
        }

        // GET: Reserva/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            ViewBag.SalaId = new SelectList(db.Salas, "SalaId", "Nombre", reserva.SalaId);
            return View(reserva);
        }

        // POST: Reserva/Edit/5
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
            var reservaToUpdate = db.Reservas.Find(id);
            if (TryUpdateModel(reservaToUpdate, "", 
                new string[] { "SalaId", "Sala", "FechaInicio", "FechaFinal", "Usuario" }))
            {
                try
                {
                    if (db.Reservas.Any(r => r.SalaId == reservaToUpdate.SalaId &&
                                             r.FechaInicio < reservaToUpdate.FechaFinal &&
                                             reservaToUpdate.FechaInicio < r.FechaFinal &&
                                             r.ReservaId != reservaToUpdate.ReservaId))
                    {
                        ModelState.AddModelError("", "La sala ya esta reservada en ese horario");
                        ViewBag.SalaId = new SelectList(db.Salas, "SalaId", "Nombre", reservaToUpdate.SalaId);
                        return View(reservaToUpdate);
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "No se pudo actualizar correctamente, intentelo nuevamente");
                }
            }

            ViewBag.SalaId = new SelectList(db.Salas, "SalaId", "Nombre", reservaToUpdate.SalaId);
            return View(reservaToUpdate);
        }

        // GET: Reserva/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reserva reserva = db.Reservas.Find(id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reserva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reserva reserva = db.Reservas.Find(id);
            db.Reservas.Remove(reserva);
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

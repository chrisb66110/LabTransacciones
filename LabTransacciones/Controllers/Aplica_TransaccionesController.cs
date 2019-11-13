using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LabTransacciones.Models;
using System.Data.SqlClient;

namespace LabTransacciones.Controllers
{
    public class Aplica_TransaccionesController : Controller
    {
        private DataBaseEntities db = new DataBaseEntities();

        // GET: Aplica_Transacciones
        public ActionResult Index()
        {
            var aplica_Transacciones = db.Aplica_Transacciones.Include(a => a.Profesor_Transacciones).Include(a => a.Examen_Transacciones);
            return View(aplica_Transacciones.ToList());
        }

        // GET: Aplica_Transacciones/Details/5
        public ActionResult Details(string cedula, string siglaExam)
        {
            if (cedula == null || siglaExam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplica_Transacciones aplica_Transacciones = db.Aplica_Transacciones.Find(cedula, siglaExam);
            if (aplica_Transacciones == null)
            {
                return HttpNotFound();
            }
            return View(aplica_Transacciones);
        }

        // GET: Aplica_Transacciones/Create
        public ActionResult Create()
        {
            ViewBag.Cedula = new SelectList(db.Profesor_Transacciones, "Cedula", "Cedula");
            ViewBag.SiglaExam = new SelectList(db.Examen_Transacciones, "SiglaExam", "SiglaExam");
            return View();
        }

        // POST: Aplica_Transacciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( [Bind(Include = "SiglaExam,Nombre,Cedula,Email,NombreP,Apellido1,Apellido2,Descripcion")] ModelCreate modelCreate)
        {
            if (ModelState.IsValid)
            {
                var CedulaP = new SqlParameter("@CedulaP", modelCreate.Cedula);
                CedulaP.SqlDbType = SqlDbType.Char;
                CedulaP.Size = 9;

                var Email = new SqlParameter("@Email", string.IsNullOrEmpty(modelCreate.Email)? DBNull.Value: (object)modelCreate.Email);
                Email.SqlDbType = SqlDbType.VarChar;
                Email.Size = 50;
                Email.IsNullable = true;

                var NombreP = new SqlParameter("@NombreP", string.IsNullOrEmpty(modelCreate.NombreP) ? DBNull.Value : (object)modelCreate.NombreP);
                NombreP.SqlDbType = SqlDbType.VarChar;
                NombreP.Size = 15;
                NombreP.IsNullable = true;

                var Apellido1 = new SqlParameter("@Apellido1", string.IsNullOrEmpty(modelCreate.Apellido1) ? DBNull.Value : (object)modelCreate.Apellido1);
                Apellido1.SqlDbType = SqlDbType.VarChar;
                Apellido1.Size = 15;
                Apellido1.IsNullable = true;

                var Apellido2 = new SqlParameter("@Apellido2", string.IsNullOrEmpty(modelCreate.Apellido2) ? DBNull.Value : (object)modelCreate.Apellido2);
                Apellido2.SqlDbType = SqlDbType.VarChar;
                Apellido2.Size = 15;
                Apellido2.IsNullable = true;

                var SiglaExam = new SqlParameter("@SiglaExam", modelCreate.SiglaExam);
                SiglaExam.SqlDbType = SqlDbType.Char;
                SiglaExam.Size = 5;

                var NombreE = new SqlParameter("@NombreE", string.IsNullOrEmpty(modelCreate.Nombre) ? DBNull.Value : (object)modelCreate.Nombre);
                NombreE.SqlDbType = SqlDbType.VarChar;
                NombreE.Size = 15;
                NombreE.IsNullable = true;

                var Descripcion = new SqlParameter("@Descripcion", string.IsNullOrEmpty(modelCreate.Descripcion) ? DBNull.Value : (object)modelCreate.Descripcion);
                Descripcion.SqlDbType = SqlDbType.VarChar;
                Descripcion.Size = 50;
                Descripcion.IsNullable = true;

                var NumeroError = new SqlParameter("@NumeroError", 0);
                NumeroError.Direction = ParameterDirection.Output;
                NumeroError.SqlDbType = SqlDbType.Int;

                db.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, 
                    "EXEC Aplicar_Examen_Sin_Transaccion @CedulaP, @Email, @NombreP, @Apellido1, @Apellido2, @SiglaExam, @NombreE, @Descripcion, @NumeroError OUTPUT", CedulaP, Email, NombreP, Apellido1, Apellido2, SiglaExam, NombreE, Descripcion, NumeroError);

                TempData["Error"] = Convert.ToInt32(NumeroError.Value);
                return RedirectToAction("Index");
            }

            ViewBag.Cedula = new SelectList(db.Profesor_Transacciones, "Cedula", "Cedula", modelCreate.Cedula);
            ViewBag.SiglaExam = new SelectList(db.Examen_Transacciones, "SiglaExam", "SiglaExam", modelCreate.SiglaExam);
            return View(modelCreate);
        }

        // GET: Aplica_Transacciones/Edit/5
        public ActionResult Edit(string cedula, string siglaExam)
        {
            if (cedula == null || siglaExam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplica_Transacciones aplica_Transacciones = db.Aplica_Transacciones.Find(cedula, siglaExam);
            if (aplica_Transacciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cedula = new SelectList(db.Profesor_Transacciones, "Cedula", "Cedula", aplica_Transacciones.Cedula);
            ViewBag.SiglaExam = new SelectList(db.Examen_Transacciones, "SiglaExam", "SiglaExam", aplica_Transacciones.SiglaExam);
            return View(aplica_Transacciones);
        }

        // POST: Aplica_Transacciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cedula,SiglaExam,Descripcion")] Aplica_Transacciones aplica_Transacciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aplica_Transacciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cedula = new SelectList(db.Profesor_Transacciones, "Cedula", "Cedula", aplica_Transacciones.Cedula);
            ViewBag.SiglaExam = new SelectList(db.Examen_Transacciones, "SiglaExam", "SiglaExam", aplica_Transacciones.SiglaExam);
            return View(aplica_Transacciones);
        }

        // GET: Aplica_Transacciones/Delete/5
        public ActionResult Delete(string cedula, string siglaExam)
        {
            if (cedula == null || siglaExam == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aplica_Transacciones aplica_Transacciones = db.Aplica_Transacciones.Find(cedula,siglaExam);
            if (aplica_Transacciones == null)
            {
                return HttpNotFound();
            }
            return View(aplica_Transacciones);
        }

        // POST: Aplica_Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string cedula, string siglaExam)
        {
            Aplica_Transacciones aplica_Transacciones = db.Aplica_Transacciones.Find(cedula,siglaExam);
            db.Aplica_Transacciones.Remove(aplica_Transacciones);
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

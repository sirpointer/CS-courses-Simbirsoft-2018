using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Home5.Models;

namespace Home5.Controllers
{
    public class GameResultsController : Controller
    {
        private CrossZeroDataBaseModel db = new CrossZeroDataBaseModel();

        // GET: GameResults
        public ActionResult Index()
        {
            return View(db.GameResults.ToList());
        }

        // GET: GameResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameResult gameResult = db.GameResults.Find(id);
            if (gameResult == null)
            {
                return HttpNotFound();
            }
            return View(gameResult);
        }

        // GET: GameResults/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Result,FirstPlayer,SecondPlayer,StepsCount,GameTime")] GameResult gameResult)
        {
            if (ModelState.IsValid)
            {
                db.GameResults.Add(gameResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gameResult);
        }

        // GET: GameResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameResult gameResult = db.GameResults.Find(id);
            if (gameResult == null)
            {
                return HttpNotFound();
            }
            return View(gameResult);
        }

        // POST: GameResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Result,FirstPlayer,SecondPlayer,StepsCount,GameTime")] GameResult gameResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gameResult);
        }

        // GET: GameResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameResult gameResult = db.GameResults.Find(id);
            if (gameResult == null)
            {
                return HttpNotFound();
            }
            return View(gameResult);
        }

        // POST: GameResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameResult gameResult = db.GameResults.Find(id);
            db.GameResults.Remove(gameResult);
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

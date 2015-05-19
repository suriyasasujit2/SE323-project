using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatDuangDee.Domain.Entities;
using WatDuangDee.Domain.Concrete;
using System.IO;

namespace WatDuangDee.UI.Controllers
{
    public class LessonController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: /Lesson/
        public async Task<ActionResult> Index()
        {
            return View(await db.Lesson.ToListAsync());
        }

        // GET: /Lesson/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = await db.Lesson.FindAsync(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // GET: /Lesson/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Lesson/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LessonId,Topic,date,description,ImageData,ImageMimeType")] Lesson lesson, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image == null)
                {
                    db.Lesson.Add(lesson);
                    await db.SaveChangesAsync();
                    TempData["message"] = string.Format("Dhamma Lesson Information has been saved");
                    return RedirectToAction("Index");
                }
                else
                {
                    lesson.ImageMimeType = image.ContentType;
                    lesson.ImageData = new byte[image.ContentLength];
                    BinaryReader reader = new BinaryReader(image.InputStream);
                    lesson.ImageData = reader.ReadBytes(image.ContentLength);

                    db.Lesson.Add(lesson);
                    await db.SaveChangesAsync();
                    TempData["message"] = string.Format("Dhamma Lesson Information has been saved");
                    return RedirectToAction("Index");
                }
            }

            return View(lesson);
        }

        // GET: /Lesson/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = await db.Lesson.FindAsync(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            TempData["Lesson"] = lesson;
            return View(lesson);
        }

        // POST: /Lesson/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LessonId,Topic,date,description,ImageData,ImageMimeType")] Lesson lesson, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {

                    lesson.ImageMimeType = image.ContentType;
                    lesson.ImageData = new byte[image.ContentLength];
                    BinaryReader reader = new BinaryReader(image.InputStream);
                    lesson.ImageData = reader.ReadBytes(image.ContentLength);

                    TempData["message"] = string.Format("Dhamma Lesson Information has been saved");
                    db.Entry(lesson).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    Lesson temp = (Lesson)TempData["Lesson"];
                    lesson.ImageData = temp.ImageData;
                    lesson.ImageMimeType = temp.ImageMimeType;

                    TempData["message"] = string.Format("Dhamma Lesson Information has been saved");
                    db.Entry(lesson).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(lesson);
        }

        // GET: /Lesson/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson lesson = await db.Lesson.FindAsync(id);
            if (lesson == null)
            {
                return HttpNotFound();
            }
            return View(lesson);
        }

        // POST: /Lesson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Lesson lesson = await db.Lesson.FindAsync(id);
            db.Lesson.Remove(lesson);
            await db.SaveChangesAsync();
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
        public FileContentResult GetImage(int id)
        {

            Lesson act = db.Lesson.FirstOrDefault(p => p.LessonId == id);
            if (act != null)
            {



                return new FileContentResult(act.ImageData, act.ImageMimeType);

            }
            else
            {
                return null;
            }
        }
    }
}

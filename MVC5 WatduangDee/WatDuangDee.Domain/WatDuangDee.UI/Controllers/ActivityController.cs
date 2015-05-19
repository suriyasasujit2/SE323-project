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
    public class ActivityController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: /Activity/
        public async Task<ActionResult> Index()
        {
            return View(await db.Activity.ToListAsync());
        }

        // GET: /Activity/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activity.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: /Activity/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Activity/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ActivityId,Topic,date,description,ImageData,ImageMimeType,Type")] Activity activity, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {

                if (image == null)
                {
                    db.Activity.Add(activity);
                    await db.SaveChangesAsync();
                    TempData["message"] = string.Format("Activity Information has been saved");
                    return RedirectToAction("Index");
                }
                else
                {
                    activity.ImageMimeType = image.ContentType;
                    activity.ImageData = new byte[image.ContentLength];
                    BinaryReader reader = new BinaryReader(image.InputStream);
                    activity.ImageData = reader.ReadBytes(image.ContentLength);

                    db.Activity.Add(activity);
                    await db.SaveChangesAsync();
                    TempData["message"] = string.Format("Activity Information has been saved");
                    return RedirectToAction("Index");
                }
             
            }

            return View(activity);
        }

        // GET: /Activity/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activity.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            TempData["Activity"] = activity;
            return View(activity);
        }

        // POST: /Activity/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ActivityId,Topic,date,description,ImageData,ImageMimeType,Type")] Activity activity, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {

                    activity.ImageMimeType = image.ContentType;
                    activity.ImageData = new byte[image.ContentLength];
                    BinaryReader reader = new BinaryReader(image.InputStream);
                    activity.ImageData = reader.ReadBytes(image.ContentLength);

                    TempData["message"] = string.Format("Activity Information has been saved");
                    db.Entry(activity).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    Activity temp = (Activity)TempData["Activity"];
                    activity.ImageData = temp.ImageData;
                    activity.ImageMimeType = temp.ImageMimeType;

                    TempData["message"] = string.Format("Activity Information has been saved");
                    db.Entry(activity).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return View(activity);
        }

        // GET: /Activity/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await db.Activity.FindAsync(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: /Activity/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Activity activity = await db.Activity.FindAsync(id);
            db.Activity.Remove(activity);
            await db.SaveChangesAsync();
            TempData["message"] = string.Format("Activity Information has been saved");
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

            Activity act = db.Activity.FirstOrDefault(p => p.ActivityId == id);
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

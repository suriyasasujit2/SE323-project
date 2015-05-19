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
    public class NewsController : Controller
    {
        private EFDbContext db = new EFDbContext();

        // GET: /News/
        public async Task<ActionResult> Index()
        {
            return View(await db.News.ToListAsync());
        }

        // GET: /News/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: /News/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "NewsId,Topic,Date,Description,ImageData,ImageMimeType")] News news, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image == null)
                {
                    db.News.Add(news);
                    await db.SaveChangesAsync();
                    TempData["message"] = string.Format("News Information has been saved");
                    return RedirectToAction("Index");
                }
                else
                {
                    news.ImageMimeType = image.ContentType;
                    news.ImageData = new byte[image.ContentLength];
                    BinaryReader reader = new BinaryReader(image.InputStream);
                    news.ImageData = reader.ReadBytes(image.ContentLength);

                    db.News.Add(news);
                    await db.SaveChangesAsync();
                    TempData["message"] = string.Format("News Information has been saved");
                    return RedirectToAction("Index");
                }
               


                
            }

            return View(news);
        }

        // GET: /News/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            TempData["News"] = news;
            return View(news);
        }

        // POST: /News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "NewsId,Topic,Date,Description,ImageData,ImageMimeType")] News news, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {

                    news.ImageMimeType = image.ContentType;
                    news.ImageData = new byte[image.ContentLength];
                    BinaryReader reader = new BinaryReader(image.InputStream);
                    news.ImageData = reader.ReadBytes(image.ContentLength);
                 
                    TempData["message"] = string.Format("News Information has been saved");
                    db.Entry(news).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    News temp = (News)TempData["News"];
                    news.ImageData = temp.ImageData;
                    news.ImageMimeType = temp.ImageMimeType;

                    TempData["message"] = string.Format("News Information has been saved");
                    db.Entry(news).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
               
            }
            return View(news);
        }

        // GET: /News/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: /News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            News news = await db.News.FindAsync(id);
            db.News.Remove(news);
            await db.SaveChangesAsync();
            TempData["message"] = string.Format("News Information has been saved");
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

            News news = db.News.FirstOrDefault(p => p.NewsId == id);
            if (news != null)
            {



                return new FileContentResult(news.ImageData, news.ImageMimeType);

            }
            else
            {
                return null;
            }
        }
    }
}

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
using WatDuangDee.Domain.Abstract;
using System.IO;

namespace WatDuangDee.UI.Controllers
{
    public class HistoryController : Controller
    {
        private EFDbContext db = new EFDbContext();
        private IRepository repository = new EFRepository();
        // GET: /History/
        public async Task<ActionResult> Index()
        {
            return View(await db.History.ToListAsync());
        }

        // GET: /History/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = await db.History.FindAsync(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

  
        // GET: /History/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = await db.History.FindAsync(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            TempData["history"] = history;
            return View(history);
        }

        // POST: /History/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(History history, HttpPostedFileBase image)
        {
         

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    
                    history.ImageMimeType = image.ContentType;
                    history.ImageData = new byte[image.ContentLength];
                    BinaryReader reader = new BinaryReader(image.InputStream);
                    history.ImageData = reader.ReadBytes(image.ContentLength);
                    this.repository.SaveHistory(history);
                    TempData["message"] = string.Format("History Information has been saved");
                    return RedirectToAction("Index");
                }
                else
                {
                    History temp = (History)TempData["history"];
                    history.ImageData = temp.ImageData;
                    history.ImageMimeType = temp.ImageMimeType;
                    this.repository.SaveHistory(history);
                    TempData["message"] = string.Format("History Information has been saved");
                    return RedirectToAction("Index");
                }

         
            }
            return View(history);
        }
        
        public FileContentResult GetImage(int id)
        {
             
            History history = repository.History.FirstOrDefault(p => p.HistoryId == id);
            if (history != null)
            {

            

                return new FileContentResult(history.ImageData, history.ImageMimeType);
              
            }
            else
            {
                return null;
            }
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

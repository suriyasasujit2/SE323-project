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
using Microsoft.AspNet.Identity;
using WatDuangDee.UI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;

namespace WatDuangDee.UI.Controllers
{
    public class QuestionandAnswerController : Controller
    {
        private EFDbContext db = new EFDbContext();


       public QuestionandAnswerController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
          
        }

       public QuestionandAnswerController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        // GET: /QuestionandAnswer/
        public ActionResult Index()
        {

           List<QAViewModel> qa = new List<QAViewModel>();
            foreach (var item in  db.QuestionAnswerDhamma.AsEnumerable())
            {
                QAViewModel a =new QAViewModel();
                ApplicationUser user = UserManager.FindById(item.UserId);
                if (user != null)
                {
                    if (item.UserId.Equals(user.Id))
                    {
                        a.ApplicationUser = user;
                        a.QuestionAnswerDhamma = item;
                        qa.Add(a);
                    }
                }
                
               
              
               
            }
            return View(qa);
        }

        // GET: /QuestionandAnswer/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionAnswerDhamma questionanswerdhamma = await db.QuestionAnswerDhamma.FindAsync(id);
            ApplicationUser user = UserManager.FindById(questionanswerdhamma.UserId);
            QAViewModel qa = new QAViewModel();
            qa.QuestionAnswerDhamma = questionanswerdhamma;
            qa.ApplicationUser = user;
            if (questionanswerdhamma == null)
            {
                return HttpNotFound();
            }
            return View(qa);
        }



        // GET: /QuestionandAnswer/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionAnswerDhamma questionanswerdhamma = await db.QuestionAnswerDhamma.FindAsync(id);
            ApplicationUser user = UserManager.FindById(questionanswerdhamma.UserId);
            QAViewModel qa = new QAViewModel();
            qa.QuestionAnswerDhamma = questionanswerdhamma;
            qa.ApplicationUser = user;
            if (questionanswerdhamma == null)
            {
                return HttpNotFound();
            }
            TempData["QA"] = questionanswerdhamma;
            return View(qa);
        }

        // POST: /QuestionandAnswer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(QuestionAnswerDhamma questionanswerdhamma)
        {
            if (ModelState.IsValid)
            {
                QuestionAnswerDhamma qa = questionanswerdhamma;


                if (qa != null)
                {
                    TempData["message"] = string.Format("Question & Answer Dhamma Information has been saved");
                    db.Entry(qa).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
                   
                   
                
               
            }
            return View(questionanswerdhamma);
        }

        // GET: /QuestionandAnswer/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionAnswerDhamma questionanswerdhamma = await db.QuestionAnswerDhamma.FindAsync(id);
            ApplicationUser user = UserManager.FindById(questionanswerdhamma.UserId);
            QAViewModel qa = new QAViewModel();
            qa.QuestionAnswerDhamma = questionanswerdhamma;
            qa.ApplicationUser = user;
            if (questionanswerdhamma == null)
            {
                return HttpNotFound();
            }
            return View(qa);
        }

        // POST: /QuestionandAnswer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            QuestionAnswerDhamma questionanswerdhamma = await db.QuestionAnswerDhamma.FindAsync(id);
            db.QuestionAnswerDhamma.Remove(questionanswerdhamma);
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

            QuestionAnswerDhamma qa = db.QuestionAnswerDhamma.FirstOrDefault(p => p.QuestionId == id);
            if (qa != null)
            {



                return new FileContentResult(qa.ImageData, qa.ImageMimeType);

            }
            else
            {
                return null;
            }
        }
    }
}

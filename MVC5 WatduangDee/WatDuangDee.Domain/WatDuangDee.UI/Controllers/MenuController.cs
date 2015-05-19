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
using WatDuangDee.Domain.Abstract;
using System.Web.Security;
namespace WatDuangDee.UI.Controllers
{
    public class MenuController : Controller
    {
       private IRepository repository;
       private EFDbContext db = new EFDbContext();

       private ApplicationDbContext app = new ApplicationDbContext();

       public MenuController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
          
        }

        public MenuController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

       public MenuController(IRepository productRepository)
        {
            this.repository = productRepository;
        }
        public ActionResult Index()
        {
            IndexViewModel index = new IndexViewModel();
            index.Activity = this.repository.Activity;
            index.News = this.repository.News;
            return View(index);
        }


        public ActionResult Contact()
        {

            LeaveForComment lev = new LeaveForComment();
            return View(lev);
        }
        public ViewResult History()
        {
            History history = repository.History.First();
            return View(history);
        }
        public ViewResult News()
        {
            return View(this.repository.News.OrderBy(o => o.Date));
        }
        public ViewResult Activity()
        {
            List<Activity> act = db.Activity.ToList();
            return View(act);
        }
        public ViewResult QA()
        {
           
             List<QAViewModel> qa = new List<QAViewModel>();
            
            foreach (var item in  db.QuestionAnswerDhamma.AsEnumerable())
            {
                 
               
                QAViewModel a =new QAViewModel();
                ApplicationUser user = app.Users.Find(item.UserId);
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
        [HttpPost]
        public ActionResult AskQA()
        {
            QuestionAnswerDhamma Ask = new QuestionAnswerDhamma();
            return View(Ask);
        }
         [HttpPost]
        public async Task<ActionResult> AskedQA(QuestionAnswerDhamma asked, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                asked.UserId = User.Identity.GetUserId();
                asked.Status = "No";
                if (image == null)
                {
                    db.QuestionAnswerDhamma.Add(asked);
                    await db.SaveChangesAsync();
                    TempData["message"] = string.Format("Dhamma Question Information has been saved to the System");
                    return RedirectToAction("QA");
                }
                else
                {
                    asked.ImageMimeType = image.ContentType;
                    asked.ImageData = new byte[image.ContentLength];
                    BinaryReader reader = new BinaryReader(image.InputStream);
                    asked.ImageData = reader.ReadBytes(image.ContentLength);

                    db.QuestionAnswerDhamma.Add(asked);
                    await db.SaveChangesAsync();
                    TempData["message"] = string.Format("Dhamma Question Information has been saved to the System");
                    return RedirectToAction("QA");
                }
            }
            
            return View(asked);
        }
         public ActionResult QASelectedView(int id)
         {

             QAViewModel a = new QAViewModel();
             QuestionAnswerDhamma qa = db.QuestionAnswerDhamma.Find(id);
             if (qa != null)
             {
                 ApplicationUser user = app.Users.Find(qa.UserId);
                 a.QuestionAnswerDhamma = qa;
                 a.ApplicationUser = user;
             }

            

             return View(a);
         }

         public ActionResult NewsDetail(int id)
         {
             News news = db.News.Find(id);
            
              


             return View(news);
         }

         public ActionResult ActDetail(int id)
         {
            Activity act = db.Activity.Find(id);




             return View(act);
         }

        public ViewResult Dhamma()
        {
             
            return View(this.repository.Lesson.OrderBy(m=>m.date));
        }


        public ViewResult DhammaLessonView(int id)
        {
            Lesson dhamma = db.Lesson.Find(id);
            return View(dhamma);
        }
        public ViewResult Photo()
        {
            
            return View(this.repository.Gallery.OrderBy(m => m.GalleryId));
        }
        public ViewResult Video()
        {
            return View(this.repository.Video.OrderBy(m => m.VideoId));
        }

        public ApplicationUserRole roles { get; set; }
        public ActionResult EmailSend(LeaveForComment lev)
        {
            EmailProcess email = new EmailProcess();
            email.emailSetting.Body = lev.Message;
            email.emailSetting.Subject = lev.Topic;
           // ApplicationDbContext db = new ApplicationDbContext();
           // var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            
          //   roles = db.ApplicationUserRoles.

   
            email.EmailSendToAdmin();


            return RedirectToAction("Contact");
        }

        public FileContentResult GetImage(int id,int type)
        {

           
            if(type ==1){
               var image = repository.History.FirstOrDefault(p => p.HistoryId == id);
               if (image != null)
               {
                   return new FileContentResult(image.ImageData, image.ImageMimeType);
               }
               else
               {
                   return null;
               }
            }
            else if (type == 2)
            {
                var image = repository.Activity.FirstOrDefault(p => p.ActivityId == id);
                if (image != null)
                {
                    return new FileContentResult(image.ImageData, image.ImageMimeType);
                }
                else
                {
                    return null;
                }
            }
            else if (type == 3)
            {
                var image = repository.News.FirstOrDefault(p => p.NewsId == id);
                if (image != null)
                {
                    return new FileContentResult(image.ImageData, image.ImageMimeType);
                }
                else
                {
                    return null;
                }
            }
            else if (type == 4)
            {
                var image = repository.QuestionAnswerDhamma.FirstOrDefault(p => p.QuestionId == id);
                if (image != null)
                {
                    return new FileContentResult(image.ImageData, image.ImageMimeType);
                }
                else
                {
                    return null;
                }
            }
            else if (type == 5)
            {
                var image = repository.Lesson.FirstOrDefault(p => p.LessonId == id);
                if (image != null)
                {
                    return new FileContentResult(image.ImageData, image.ImageMimeType);
                }
                else
                {
                    return null;
                }
            }
            return null;
           
          
        }
     
	}
}
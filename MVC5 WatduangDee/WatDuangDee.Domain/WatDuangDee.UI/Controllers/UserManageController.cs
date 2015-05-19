using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WatDuangDee.UI.Models;

namespace WatDuangDee.UI.Controllers
{
        
    public class UserManageController : Controller
    {


        private ApplicationDbContext db;
        public UserManageController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            db = new ApplicationDbContext();
        }
       
  
   
    public UserManageController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
    
        //
        // GET: /UserManage/
        public ActionResult Index()
        {
           
           var user = db.Users.ToList();
       
            return View(user);
        }

        //
        // GET: /UserManage/Details/5
    //[HttpPost]
        public async Task<ActionResult> Details(string id)
        {
            ApplicationUser user = await UserManager.FindByIdAsync(id);
            //var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
           var roles = db.ApplicationUserRoles.ToList();
            if (user != null)
            {
               
               
                 
                return View(user);
                
                
                
               
            }
            else
            {
                user.Name = "null";
                return View(user);
            }

           
        }

        //
        // GET: /UserManage/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserManage/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserManage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /UserManage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /UserManage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /UserManage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

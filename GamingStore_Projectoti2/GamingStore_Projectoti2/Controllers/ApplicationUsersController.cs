﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GamingStore_Projectoti2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GamingStore_Projectoti2.Controllers
{
    [Authorize(Roles = "Admin")] // apenas o Admin tem acesso a esta página
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUsersController()
        {
            var store = new UserStore<ApplicationUser>(db);
            _userManager = new UserManager<ApplicationUser>(store);
        }

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }


       

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> DeleteConfirmedAsync(string id)
        {


            ApplicationUser applicationUser = db.Users.Find(id);
            // primeiro tem que se remover a role do cliente
            //De seguida o Cliente é removido
            IList<string> userRoles = await _userManager.GetRolesAsync(applicationUser.Id);

            var roleUser = applicationUser.Roles.Select(r => r).SingleOrDefault();
            var roleDb = db.Roles.Where(r => r.Id == roleUser.RoleId).Select(r => r).SingleOrDefault();

            string roleName = roleDb.Name;
            var roleToRemove = userRoles.FirstOrDefault(role => role.Equals(roleName, StringComparison.InvariantCultureIgnoreCase));
            // remove a role do cliente
            var result = await _userManager.RemoveFromRoleAsync(applicationUser.Id, roleToRemove);

                db.Users.Remove(applicationUser);
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

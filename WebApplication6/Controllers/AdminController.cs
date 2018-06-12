using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication6.Controllers
{
    public class AdminController : Controller
    {
        database_Access_Layer.db dblayer = new database_Access_Layer.db();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(FormCollection fc)
        {
            int res = dblayer.Admin_Login(fc["adminid"], fc["password"]);
            if (res == 1)
            {
                // TempData["msg"] = "login successfully";
                return RedirectToAction("FileUpload", "Account");
            }
            else
            {
                TempData["msg"] = "Admin id or password is wrong";
            }
            return View();
        }
    }
}
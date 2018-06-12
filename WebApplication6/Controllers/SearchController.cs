using WebApplication6.Models;
using WebApplication6.Dapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace WebApplication6.Controllers
{
    public class SearchController : Controller
    {
        private SqlConnection con;
        private void Dbconnection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);
        }
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult FileDetails()
        {
            List<FileDetailsModel> DetList = GetFileList();
            return PartialView("FileDetails", DetList);
        }

        private List<FileDetailsModel> GetFileList()
        {
            List<FileDetailsModel> DetList = new List<FileDetailsModel>();

            Dbconnection();
            con.Open();
            DetList = SqlMapper.Query<FileDetailsModel>
            (con, "GetFileDetails", commandType: CommandType.StoredProcedure).ToList();
            con.Close();
            return DetList;
        }

        public JsonResult GetSearchingData(string SearchBy, string SearchValue)
        {
            List<FileDetailsModel> detlist = new List<FileDetailsModel>();
            if (SearchBy == "ID")
            {
                try
                {
                    int Id = Convert.ToInt32(SearchValue);
                    detlist = detlist.Where(x => x.Id == Id || SearchValue == null).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} Is Not A ID ", SearchValue);
                }
                return Json(detlist, JsonRequestBehavior.AllowGet);
            }
            else
            {
                detlist = detlist.Where(x => x.EbookName.StartsWith(SearchValue) || SearchValue == null).ToList();
                return Json(detlist, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
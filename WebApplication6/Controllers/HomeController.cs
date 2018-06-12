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
    public class HomeController : Controller
    {      
   
        private SqlConnection con;
        private void Dbconnection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);
        }
      
        // Download File
        // GET: Home

        [HttpGet]
        public FileResult DownloadFile(int id)
        {
            List<FileDetailsModel> ObjFiles = GetFileList();
            var FileById = (from FC in ObjFiles
                            where FC.Id.Equals(id)
                            select new { FC.FileName, FC.FileContent }).ToList().FirstOrDefault();

            return File(FileById.FileContent, "application/pdf", FileById.FileName);
        }

        //VIew Uploaded Files
        [HttpGet]
        public PartialViewResult FileDetails()
        {
            List<FileDetailsModel> DetList = GetFileList();
            return PartialView("FileDetails", DetList);
        }

        [HttpGet]
        public PartialViewResult Search(string searching)
        {
            List<FileDetailsModel> DetList = GetFileList();
            if (!String.IsNullOrEmpty(searching))
            {
                DetList = DetList.Where(x => x.EbookName.Contains(searching) || searching == null).ToList();
            }
            return PartialView("Search", DetList);
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

    }
   
}
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
using System.Net;

namespace WebApplication6.Controllers
{
    public class AccountController : Controller
    {
  
        //Dapper CRUD
        private IFileDashBoard _dashboard = new FileDashBoard();

        private SqlConnection con;
        private void Dbconnection()
        {
            string constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
            con = new SqlConnection(constr);
        }


        // Upload Download File
        // GET: Home

        public ActionResult FileUpload()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase files,  HttpPostedFileBase imagefiles)
        {

            String FileExt = Path.GetExtension(files.FileName).ToUpper();

            if (FileExt == ".PDF")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);

                //Stream str1 = imagefiles.InputStream;
                //BinaryReader Br1 = new BinaryReader(str1);
                //Byte[] FileDet1 = Br1.ReadBytes((Int32)str1.Length);              

                 FileDetailsModel Fd = new WebApplication6.Models.FileDetailsModel();
                 Fd.EbookName = Request.Form["EbookName"];
                 Fd.FileName = files.FileName;
                 Fd.FileContent = FileDet;
                 Fd.Image = COnvertToBytes(imagefiles);
                 SaveFileDetails(Fd);
                 return RedirectToAction("FileUpload");
                 }
                 else
                 {
                    ViewBag.FileStatus = "Invalid file format";
                    return View();
                 }
             }

        public byte[] COnvertToBytes(HttpPostedFileBase imagefiles)            
          {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(imagefiles.InputStream);
            imageBytes = reader.ReadBytes((int)imagefiles.ContentLength);
            return imageBytes;
           }   


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

        // Database related operations
        private void SaveFileDetails(FileDetailsModel objDet)

        {
            DynamicParameters Parm = new DynamicParameters();
            Parm.Add("@FileName", objDet.FileName);
            Parm.Add("@FileContent", objDet.FileContent);
            Parm.Add("@EbookName", objDet.EbookName);
            Parm.Add("@Image", objDet.Image);

            Dbconnection();
            con.Open();
            con.Execute("AddFileDetails", Parm, commandType: System.Data.CommandType.StoredProcedure);
            con.Close();
        }

        //private SqlConnection con;
        //private string constr;
        //private void DbConnection()
        //{
        //    constr = ConfigurationManager.ConnectionStrings["dbcon"].ToString();
        //    con = new SqlConnection(constr);
        //}

        //Dapper CRUD 

        //GET: /Files/

        //
        //GET: /File/Delete/5



        public ActionResult Delete(int id)
        {
            return View(_dashboard.Find(id));
        }

        //
        // POST: /File/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _dashboard.Remove(id);
                return RedirectToAction("FileUpload");
            }
            catch
            {
                return View();
            }
        }

    }
}
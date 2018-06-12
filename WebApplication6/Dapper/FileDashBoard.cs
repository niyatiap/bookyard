using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;

namespace WebApplication6.Dapper
{
    public class FileDashBoard : IFileDashBoard
    {
        // private IDbConnection _db = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;attachdbfilename=|DataDirectory|\\bookyard.mdf; Integrated Security=True;");
         private IDbConnection _db = new SqlConnection("Data Source=SQL5039.site4now.net;Initial Catalog=DB_A3583B_bookyard;User Id=DB_A3583B_bookyard_admin;Password=justit123; Integrated Security=False;");

        public List<FileEbook> GetAll()
        {
            List<FileEbook> FileList = this._db.Query<FileEbook>("SELECT * FROM FileDetails").ToList();
            return FileList;
        }

        public FileEbook Find(int? id)
        {
            string query = "SELECT * FROM FileDetails WHERE Id = " + id + "";
            return this._db.Query<FileEbook>(query).SingleOrDefault();
        }

        public FileEbook Add(FileEbook file)
        {
            var sqlQuery = "INSERT INTO FileDetails (FileName, FileContent) VALUES(@FileName, @FileContent); " + "SELECT CAST(SCOPE_IDENTITY() as int)";
            var FileId = this._db.Query<int>(sqlQuery, file).Single();
            file.Id = FileId;
            return file;
        }

        public FileEbook Update(FileEbook file)
        {
            var sqlQuery =
            "UPDATE FileDetails " +
            "SET FileName = @FileName, " +
            " FileContent = @FileContent " +           
            "WHERE Id = @Id";
            this._db.Execute(sqlQuery, file);
            return file;
        }

        public void Remove(int id)
        {
            var sqlQuery = ("Delete From FileDetails Where Id = " + id + "");
            this._db.Execute(sqlQuery);
        }
    }
}
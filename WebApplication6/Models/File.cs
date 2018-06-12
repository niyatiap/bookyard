using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace WebApplication6.Dapper
{
    public class FileEbook
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public byte[] FileContent { get; set; }

    }
}
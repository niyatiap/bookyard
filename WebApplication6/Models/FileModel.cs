using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Dapper;

namespace WebApplication6.Models
{
    public class FileModel
    {

        public string EbookName { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Select File")]
        public HttpPostedFileBase files { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Select Image")]
        public HttpPostedFileBase imagefiles { get; set; }
    }

    public class FileDetailsModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Uploaded File")]
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string EbookName { get; set; }
        public byte[] Image { get; set; }

    }


}
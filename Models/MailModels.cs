using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace LOWE.Models
{
    public class MailModels
    {


        public string MailAddress { set; get; } = "";
        public string Subject { set; get; } = "";
        public string UserName { set; get; } = "";
        public string Message { set; get; } = "";
        [NotMapped]
        public HttpPostedFileBase Image { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationTest.Models
{
    public class tbl_dept
    {
        [Key]
        public int D_Id { get; set; }
        public string DName { get; set; }

    }
}
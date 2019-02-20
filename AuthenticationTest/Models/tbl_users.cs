using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AuthenticationTest.Models
{
    public class tbl_users
    {
        
        [Key]
        public int U_Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string signature { get; set; }
        [ForeignKey("tbl_roles")]
        public int? Role_id { get; set; }
        public virtual tbl_roles tbl_roles { get; set; }
    }
}
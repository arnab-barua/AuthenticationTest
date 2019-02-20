using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationTest.Models
{
    public class tbl_role_actions
    {
        public tbl_role_actions()
        {
            tbl_roles = new HashSet<tbl_roles>();
        }
        [Key]
        public int Action_Id { get; set; }
        public string Action_Name { get; set; }
        public virtual ICollection<tbl_roles> tbl_roles { get; set; }
    }   

}
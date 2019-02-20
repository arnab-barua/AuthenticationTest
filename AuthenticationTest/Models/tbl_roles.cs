using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationTest.Models
{
    public class tbl_roles
    {
        public tbl_roles()
        {
            tbl_users = new List<tbl_users>();
            tbl_role_actions = new HashSet<tbl_role_actions>();
        }
        [Key]
        public int Role_Id { get; set; }

        [Display(Name = "Role Name")]
        public string role_name { get; set; }


        public virtual ICollection<tbl_users> tbl_users { get; set; }
		
        public virtual ICollection<tbl_role_actions> tbl_role_actions { get; set; }

    }
}
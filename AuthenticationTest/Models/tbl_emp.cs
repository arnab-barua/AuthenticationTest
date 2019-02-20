using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AuthenticationTest.Models
{
    public class tbl_emp
    {
        [Key]
        public int e_Id { get; set; }
        public string e_Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public int Nid { get; set; }
    }
    public class AUTH_TEST_DB:DbContext
    {
        public AUTH_TEST_DB()
        {

        }
        public DbSet<tbl_emp> employee { get; set; }
        public DbSet<tbl_dept> departments { get; set; }
        public DbSet<tbl_users> users { get; set; }
        public DbSet<tbl_roles> roles { get; set; }
        public DbSet<tbl_role_actions> role_actions { get; set; }
    }


}
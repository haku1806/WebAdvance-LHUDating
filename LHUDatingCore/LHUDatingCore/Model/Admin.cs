using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LhuDating.Models
{
    [Table("Admin")]
    public class Admin
    {


        public string Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool Active { get; set; } //[Active] [bit] NOT NULL,
    }
}
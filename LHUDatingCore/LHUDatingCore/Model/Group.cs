﻿using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System;
using System.Collections.Generic;

namespace LhuDating.Models
{
    
        [Table("Group")]
        public class Group
        {
            [Key]
            public string Code { get; set; }
            public string Type { get; set; }
            public string Avatar { get; set; }
            public string Name { get; set; }
            public DateTime Created { get; set; }
            public string CreatedBy { get; set; }
            public DateTime LastActive { get; set; }

            public virtual ICollection<GroupUser> GroupUsers { get; set; }
        }
    }
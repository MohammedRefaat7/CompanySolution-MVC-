﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Department : BaseEntity
    {
        [Required]
        [MaxLength(55)]
        public string Name { get; set; }
        
        [Required]
        public string Code { get; set; }
        
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}

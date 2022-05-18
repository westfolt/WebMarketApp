using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Entities
{
    public class Person:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        [EmailAddress] 
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticeWeb.Models
{
    public class Employee
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Department { get; set; }
        public bool isDeleted   { get; set; }
    }
}
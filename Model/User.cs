using ForeignLanguagesSchool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JMBG { get; set; }
        public Gender Gender{ get; set; }
        public Address Address{ get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType{ get; set; }
    }
}

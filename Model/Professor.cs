using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Model
{
    public class Professor : User
    {
        public School School { get; set; }
        public List<string> Languages { get; set; }
        public List<Class> Classes{ get; set; }
    }
}

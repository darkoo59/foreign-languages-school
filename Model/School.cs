using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Model
{
    public class School
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public Address Address{ get; set; }
        public List<string> Languages { get; set; }
    }
}

﻿using ForeignLanguagesSchool.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForeignLanguagesSchool.Model
{
    public class Class
    {
        public int Id { get; set; }
        public int ProfessorId{ get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int Duration { get; set; }
        public ClassStatus ClassStatus { get; set; }
        public int StudentId{ get; set; }
        public int Deleted { get; set; }
    }
}

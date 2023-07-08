using System;
using System.Collections.Generic;

#nullable disable

namespace Group01_PRJ.Models
{
    public partial class UserClass
    {
        public int Classid { get; set; }
        public int Userid { get; set; }
        public int Courseid { get; set; }

        public virtual Class Class { get; set; }
        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}

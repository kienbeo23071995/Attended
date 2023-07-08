using System;
using System.Collections.Generic;

#nullable disable

namespace Group01_PRJ.Models
{
    public partial class Attended
    {
        public int Roomid { get; set; }
        public int Slotid { get; set; }
        public DateTime Date { get; set; }
        public int Userid { get; set; }
        public bool Status { get; set; }

        public virtual Session Session { get; set; }
        public virtual User User { get; set; }
    }
}

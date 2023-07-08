using System;
using System.Collections.Generic;

#nullable disable

namespace Group01_PRJ.Models
{
    public partial class UserGroup
    {
        public int Groupid { get; set; }
        public int Userid { get; set; }

        public virtual Group Group { get; set; }
        public virtual User User { get; set; }
    }
}

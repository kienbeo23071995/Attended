using System;
using System.Collections.Generic;

#nullable disable

namespace Group01_PRJ.Models
{
    public partial class Group
    {
        public Group()
        {
            UserGroups = new HashSet<UserGroup>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}

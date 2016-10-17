using System;
using System.Collections.Generic;

namespace EKIFVK.Todo.API.Models
{
    public partial class SystemUsergroup
    {
        public SystemUsergroup()
        {
            SystemUser = new HashSet<SystemUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Permission { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public string Tag { get; set; }

        public virtual ICollection<SystemUser> SystemUser { get; set; }
    }
}

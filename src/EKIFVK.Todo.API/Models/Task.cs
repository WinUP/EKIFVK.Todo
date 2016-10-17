using System;
using System.Collections.Generic;

namespace EKIFVK.Todo.API.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Owner { get; set; }
        public DateTime? Deadline { get; set; }

        public virtual SystemUser OwnerNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class Gender
    {
        public Gender()
        {
            Books = new HashSet<Book>();
        }

        public Guid GenderId { get; set; }
        public string GenderName { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}

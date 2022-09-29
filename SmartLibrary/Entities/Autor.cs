using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class Autor
    {
        public Autor()
        {
            Books = new HashSet<Book>();
        }

        public Guid AutorId { get; set; }
        public Guid? CountryId { get; set; }
        public Guid? PersonId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? IsActive { get; set; }

        public virtual Country? Country { get; set; }
        public virtual Person? Person { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}

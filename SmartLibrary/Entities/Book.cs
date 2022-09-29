using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class Book
    {
        public Book()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public Guid BookId { get; set; }
        public Guid? GenderId { get; set; }
        public Guid? AutorId { get; set; }
        public Guid? EditorialId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Autor? Autor { get; set; }
        public virtual Editorial? Editorial { get; set; }
        public virtual Gender? Gender { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}

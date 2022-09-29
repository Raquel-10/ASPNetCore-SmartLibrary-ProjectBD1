using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class Factura
    {
        public Factura()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public Guid FacturaId { get; set; }
        public Guid? ClienteId { get; set; }
        public Guid? TipoDocumentId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public bool? IsActive { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual TipoDocumento? TipoDocument { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}

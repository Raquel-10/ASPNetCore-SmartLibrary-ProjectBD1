using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class InvoiceDetail
    {
        public Guid InvoiceDetailsId { get; set; }
        public Guid? BookId { get; set; }
        public Guid? FacturaId { get; set; }
        public decimal Price { get; set; }
        public int Cuantity { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }

        public virtual Book? Book { get; set; }
        public virtual Factura? Factura { get; set; }
    }
}

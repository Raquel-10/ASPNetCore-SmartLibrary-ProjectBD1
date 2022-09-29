using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class TipoDocumento
    {
        public Guid TipoDocumentId { get; set; }
        public string DocumentName { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual Factura? Factura { get; set; }
    }
}

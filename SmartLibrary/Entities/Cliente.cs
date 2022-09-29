using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            Facturas = new HashSet<Factura>();
        }

        public Guid ClienteId { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? CountryId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Country? Country { get; set; }
        public virtual Person? Person { get; set; }
        public virtual ICollection<Factura> Facturas { get; set; }
    }
}

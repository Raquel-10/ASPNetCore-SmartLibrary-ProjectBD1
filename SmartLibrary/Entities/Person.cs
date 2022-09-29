using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class Person
    {
        public Person()
        {
            Clientes = new HashSet<Cliente>();
        }

        public Guid PersonId { get; set; }
        public string FirstName1 { get; set; } = null!;
        public string FirstName2 { get; set; } = null!;
        public string LastName1 { get; set; } = null!;
        public string LastName2 { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual Autor? Autor { get; set; }
        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}

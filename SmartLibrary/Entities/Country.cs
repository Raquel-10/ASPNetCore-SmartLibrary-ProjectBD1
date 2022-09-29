using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class Country
    {
        public Country()
        {
            Autors = new HashSet<Autor>();
        }

        public Guid CountryId { get; set; }
        public string CountryName { get; set; } = null!;
        public string Iso { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Editorial? Editorial { get; set; }
        public virtual ICollection<Autor> Autors { get; set; }
    }
}

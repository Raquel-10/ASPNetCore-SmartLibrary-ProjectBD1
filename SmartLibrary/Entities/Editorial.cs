using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class Editorial
    {
        public Guid EditorialId { get; set; }
        public string EditorialName { get; set; } = null!;
        public Guid? CountryId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Country? Country { get; set; }
        public virtual Book? Book { get; set; }
    }
}

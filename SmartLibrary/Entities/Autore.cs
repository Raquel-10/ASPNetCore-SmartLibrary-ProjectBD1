using System;
using System.Collections.Generic;

namespace SmartLibrary.Entities
{
    public partial class Autore
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Lastname2 { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}

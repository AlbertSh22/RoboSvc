using System;
using System.Collections.Generic;

namespace EntityDal.Models
{
    public partial class Language
    {
        public short Id { get; set; }
        public string Name { get; set; } = null!;
        public string Alpha2 { get; set; } = null!;
        public string? Notes { get; set; }
        public string? DigitalCode { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace EntityDal.Models
{
    /// <summary>
    /// Classifier Languages
    /// </summary>
    public partial class Language
    {
        /// <summary>
        /// ID
        /// </summary>
        public short Id { get; set; }
        /// <summary>
        /// ISO language name
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Codes for the representation of names of languages—Part 1: Alpha-2 code
        /// </summary>
        public string Alpha2 { get; set; } = null!;
        /// <summary>
        /// Language notes
        /// </summary>
        public string? Notes { get; set; }
        /// <summary>
        /// The digital code consisting of 3 Arabic numerals and assigned to languages arranged in the order of Russian names.
        /// </summary>
        public string? DigitalCode { get; set; }
    }
}

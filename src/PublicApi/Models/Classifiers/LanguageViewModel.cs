using System.ComponentModel.DataAnnotations;

using EntityDal.Interfaces;

namespace PublicApi.Models.Classifiers
{
    /// <summary>
    ///     Represents the data of the Language view model classifier.
    /// </summary>
    public class LanguageViewModel : IEntity<short>
    {
        /// <summary>
        ///     ID.
        /// </summary>
        public short Id { get; set; }

        /// <summary>
        ///     ISO language name.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        ///     ISO 639-1 code.
        /// </summary>
        [Required]
        [RegularExpression(@"^[a-z]{2}$", 
            ErrorMessage = "This value must contains only 2 lower case English letters.")]
        public string Alpha2 { get; set; } = null!;

        /// <summary>
        ///     Language notes.
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        ///    The digital code consisting of 3 Arabic numerals and 
        ///    assigned to languages arranged in the order of Russian 
        ///    names. 
        /// </summary>
        [RegularExpression(@"^$|^[\d]{3}$",
            ErrorMessage = "This value must be zero or contains only 3 Arabic numerals.")]
        public string? DigitalCode { get; set; }
    }
}

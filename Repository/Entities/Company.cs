using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;


namespace Repository.Entities
{
    [Index(nameof(Isin), IsUnique = true)]
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [RegularExpression(@"([A-Za-z]{2})\w+")]
        public string Isin { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Exchange { get; set; }

        [Required]
        public string Ticker { get; set; }

        public string? Website { get; set; }
    }
}

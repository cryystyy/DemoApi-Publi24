using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.Company
{
    public class CreateCompanyDto
    {

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

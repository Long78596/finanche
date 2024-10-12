using APISYMBOL.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APISYMBOL.Dto.StockDto
{
    public class UpdateDtorequest
    {
        [Required]
        [MinLength(1, ErrorMessage = "Symbol must be 1 characters")]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 character")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Company must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Company cannot be over 280 character")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1, 100000000)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.001, 100)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Industry must be 5 characters")]
        [MaxLength(280, ErrorMessage = "Industry cannot be over 280 character")]
        public string Industry { get; set; } = string.Empty;
        [Range(1, 500000)]
        public long MarketCap { get; set; }
    }
}

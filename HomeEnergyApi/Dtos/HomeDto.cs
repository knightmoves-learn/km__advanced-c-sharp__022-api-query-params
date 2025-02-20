using System.ComponentModel.DataAnnotations;

namespace HomeEnergyApi.Dtos
{
    public class HomeDto
    {
        [Required]
        public string? OwnerLastName { get; set; }
        [StringLength(40)]
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public int? MonthlyElectricUsage { get; set; }
        public ICollection<int>? UtilityProviderIds { get; set; }
    }
}

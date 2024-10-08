using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Domain
{
    [Table("Region")]
    public sealed class ILibeyRegion
    {
        [Key]
        [Column("RegionCode")]
        public string? RegionCode { get; private set; }

        [Column("RegionDescription")]
        public string? RegionDescription { get; private set; }

        public ILibeyRegion() { }

        public ILibeyRegion(string code, string name)
        {
            RegionCode = code;
            RegionDescription = name;
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Domain
{
    [Table("Province")]
    public sealed class ILibeyProvince
    {
        [Key]
        [Column("ProvinceCode")]
        public string? ProvinceCode { get; private set; }

        [Column("RegionCode")]
        public string? RegionCode { get; private set; }

        [Column("ProvinceDescription")]
        public string? ProvinceDescription { get; private set; }

        public ILibeyProvince() { }

        public ILibeyProvince(string provinceCode, string regionCode, string provinceDescription)
        {
            ProvinceCode = provinceCode;
            RegionCode = regionCode;
            ProvinceDescription = provinceDescription;
        }

    }
}

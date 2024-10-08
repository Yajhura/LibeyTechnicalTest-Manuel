using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibeyTechnicalTestDomain.LibeyUbigeoAggregate.Domain
{
    [Table("Ubigeo")]
    public sealed class ILibeyUbigeo
    {
        [Key]
        [Column("UbigeoCode")]
        public string? UbigeoCode { get; private set; }


        [Column("ProvinceCode")]
        public string? ProvinceCode { get; private set; }

        [Column("RegionCode")]

        public string? RegionCode { get; private set; }

        [Column("UbigeoDescription")]
        public string? UbigeoDescription { get; private set; }

        public ILibeyUbigeo() { }

        public ILibeyUbigeo(string code, string provinceCode, string regionCode, string name)
        {
            UbigeoCode = code;
            ProvinceCode = provinceCode;
            RegionCode = regionCode;
            UbigeoDescription = name;
        }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagerUI.Data;

[Table("PRODUCT", Schema = "v2")]
public class Product
{
    [Key]
    public int LDB_ID { get; set; }

    [Key]
    public int PRODUCT_ID { get; set; }

    [MaxLength(200)]
    public string? EXTERNAL_NAME { get; set; }

    [Required]
    [MaxLength(200)]
    public string INTERNAL_NAME { get; set; } = string.Empty;

    public string? ORDER_FORM_SCHEMA { get; set; }

    public string? AUTO_PROD_MAPPING_SCHEMA { get; set; }

    [MaxLength(100)]
    public string? ENDPOINT_PATH { get; set; }

    [MaxLength(50)]
    public string? PROCESS_TYPE { get; set; }

    [MaxLength(50)]
    public string? STATE { get; set; }

    public virtual ICollection<ProductConfig> Configs { get; set; } = new List<ProductConfig>();
    public virtual ICollection<ProductMetadata> Metadata { get; set; } = new List<ProductMetadata>();
    public virtual ICollection<ProductInterestTier> InterestTiers { get; set; } = new List<ProductInterestTier>();
}

[Table("PRODUCT_CONFIG", Schema = "v2")]
public class ProductConfig
{
    [Key]
    public int LDB_ID { get; set; }

    [Key]
    public int CONFIG_ID { get; set; }

    public int PRODUCT_ID { get; set; }

    [Required]
    [MaxLength(100)]
    public string KEY_TYPE { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string KEY { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? VALUE { get; set; }

    [ForeignKey("LDB_ID, PRODUCT_ID")]
    public virtual Product? Product { get; set; }
}

[Table("PRODUCT_METADATA", Schema = "v2")]
public class ProductMetadata
{
    [Key]
    public int LDB_ID { get; set; }

    [Key]
    public int PRODUCT_ID { get; set; }

    [Key]
    [MaxLength(10)]
    public string LANGUAGE_CODE { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? MARKETING_PRODUCT_TYPE { get; set; }

    [MaxLength(200)]
    public string? MARKETING_NAME { get; set; }

    public string? MARKETING_DESCRIPTION { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MIN_TOTAL_DEPOSIT { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? MAX_TOTAL_DEPOSIT { get; set; }

    public string? BINDING { get; set; }

    public string? NOTICE_PERIOD { get; set; }

    public string? ADVANTAGES { get; set; }

    public string? WORTH_KNOWING { get; set; }

    public string? TERM_OF_NOTICE_TITLE { get; set; }

    public string? TERM_OF_NOTICE_DESCRIPTION { get; set; }

    public string? IMPORTANT_INFO_TITLE { get; set; }

    public string? IMPORTANT_INFO_DESCRIPTION { get; set; }

    [MaxLength(500)]
    public string? PRODUCT_URL { get; set; }

    [ForeignKey("LDB_ID, PRODUCT_ID")]
    public virtual Product? Product { get; set; }
}

[Table("PRODUCT_INTEREST_TIERS", Schema = "v2")]
public class ProductInterestTier
{
    [Key]
    public int LDB_ID { get; set; }

    [Key]
    public int PRODUCT_ID { get; set; }

    [Key]
    [MaxLength(50)]
    public string INTEREST_TYPE { get; set; } = string.Empty;

    [Key]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal LIMIT { get; set; }

    [Column(TypeName = "decimal(6, 4)")]
    public decimal INTEREST_RATE { get; set; }

    [ForeignKey("LDB_ID, PRODUCT_ID")]
    public virtual Product? Product { get; set; }
}


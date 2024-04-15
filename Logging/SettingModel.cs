using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logging
{
    [Table("tbl_setting")]
    [Index("Key", IsUnique = true)]
    public class SettingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Key { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Value { get; set; }
    }
}

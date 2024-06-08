namespace QuanLySinhVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("lophoc")]
    public partial class lophoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maLop { get; set; }

        [Required]
        [StringLength(80)]
        public string tenLop { get; set; }

        public int maGV { get; set; }

        public virtual giangvien giangvien { get; set; }
    }
}

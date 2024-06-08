namespace QuanLySinhVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("giangvien")]
    public partial class giangvien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public giangvien()
        {
            lophocs = new HashSet<lophoc>();
        }

        [Key]
        public int maGV { get; set; }

        [Required]
        [StringLength(20)]
        public string tenDangNhap { get; set; }

        [Required]
        [StringLength(80)]
        public string hoTen { get; set; }

        [Required]
        [StringLength(11)]
        public string sdt { get; set; }

        [Required]
        [StringLength(50)]
        public string diachi { get; set; }

        [Required]
        [StringLength(50)]
        public string hinhAnh { get; set; }

        public virtual taikhoan taikhoan { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lophoc> lophocs { get; set; }
    }
}

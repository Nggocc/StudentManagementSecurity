namespace QuanLySinhVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sinhvien")]
    public partial class sinhvien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int maSV { get; set; }

        [Required]
        [StringLength(20)]
        public string tenDangNhap { get; set; }

        public int maLop { get; set; }

        [Required(ErrorMessage ="Họ tên không được để trống!")]
        [StringLength(80)]
        public string hoTen { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống!")]
        [StringLength(80)]
        public string diachi { get; set; }

        [Required(ErrorMessage = "SĐT không được để trống!")]
        [StringLength(11)]
        public string sdt { get; set; }

        [Required(ErrorMessage = "Email không được để trống!")]
        [StringLength(30)]
        public string email { get; set; }
        [Range(0, 10, ErrorMessage = "Điểm trong khoảng từ 0 đến 10!")]
        public double? diemTX1 { get; set; }
        [Range(0, 10, ErrorMessage = "Điểm trong khoảng từ 0 đến 10!")]
        public double? diemXT2 { get; set; }
        [Range(0, 10, ErrorMessage = "Điểm trong khoảng từ 0 đến 10!")]
        public double? diemThi { get; set; }

        [Required(ErrorMessage = "Hình ảnh không được để trống!")]
        [StringLength(80)]
        public string hinhAnh { get; set; }

        public virtual taikhoan taikhoan { get; set; }
    }
}

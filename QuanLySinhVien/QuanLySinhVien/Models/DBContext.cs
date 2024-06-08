using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLySinhVien.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<giangvien> giangviens { get; set; }
        public virtual DbSet<lophoc> lophocs { get; set; }
        public virtual DbSet<sinhvien> sinhviens { get; set; }
        public virtual DbSet<taikhoan> taikhoans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<giangvien>()
                .Property(e => e.tenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<giangvien>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<giangvien>()
                .Property(e => e.hinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<giangvien>()
                .HasMany(e => e.lophocs)
                .WithRequired(e => e.giangvien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<sinhvien>()
                .Property(e => e.tenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<sinhvien>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<sinhvien>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<sinhvien>()
                .Property(e => e.hinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<taikhoan>()
                .Property(e => e.tenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<taikhoan>()
                .Property(e => e.matkhau)
                .IsUnicode(false);

            modelBuilder.Entity<taikhoan>()
                .Property(e => e.hash)
                .IsUnicode(false);

            modelBuilder.Entity<taikhoan>()
                .HasMany(e => e.giangviens)
                .WithRequired(e => e.taikhoan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<taikhoan>()
                .HasMany(e => e.sinhviens)
                .WithRequired(e => e.taikhoan)
                .WillCascadeOnDelete(false);
        }
    }
}

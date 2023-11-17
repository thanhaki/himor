using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pos.API.Domain.Entities;
using System.Data;
using System.Reflection.Emit;

namespace Pos.API.Infrastructure.Persistence
{
    public class DBPosContext : DbContext
    {
        public DBPosContext(DbContextOptions<DBPosContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<M_ThucDon_MatHang>().HasKey(table => new
            {
                table.Ma_TD, table.Ma_MH
            });
            builder.Entity<T_DonHangChiTiet>().HasKey(table => new
            {
                table.SoDonHang,
                table.Ma_MH
            });

            builder.Entity<M_NhomQuyen_ChucNang>().HasKey(table => new
            {
                table.Ma_ChucNang,
                table.Ma_NhomQuyen
            });

            builder.Entity<T_VerifyCode>().HasKey(table => new
            {
                table.DonVi,
                table.CodeVerify
            });
            
            builder.Entity<M_DonVi>().HasKey(table => new
            {
                table.DonVi,
            });

            builder.Entity<M_KhuyenMai_ApDung>().HasKey(table => new
            {
                table.SoKhuyenMaiApDung,
                table.SoKhuyenMai
            });

            builder.Entity<M_KhuyenMai_DoiTuong>().HasKey(table => new
            {
                table.SoKhuyenMaiDoiTuong,
                table.SoKhuyenMai
            });
            builder.Entity<M_KhuyenMai_KhoangThoiGian>().HasKey(table => new
            {
                table.SoKhuyenMaiKhoangThoiGian,
                table.SoKhuyenMai
            });
            builder.Entity<T_PhieuNhapXuat_ChiTiet>().HasKey(table => new
            {
                table.Ma_PNX,
                table.Ma_MH
            });
            builder.Entity<T_LichSuTichDiem_KhachHang>().HasKey(table => new
            {
                table.MaLichSuTichDiem,
                table.Ma_KH,
                table.SoDonHang,
            });
            builder.Entity<T_LichCongTac_NhanVien>().HasKey(table => new
            {
                table.So_NV,
                table.Month,
                table.Year,
            });
        }

        public DbSet<M_DonVi> M_DonVi { get; set; }
        public DbSet<M_User> M_User { get; set; }
        public DbSet<M_Data> M_Data { get; set; }
        public DbSet<T_TokenInfo> T_TokenInfo { get; set; }
        public DbSet<M_NhomQuyen> M_NhomQuyen { get; set; }
        public DbSet<M_NhomQuyen_ChucNang> M_NhomQuyen_ChucNang { get; set; }
        public DbSet<M_Saler> M_Saler { get; set; }
    }
}

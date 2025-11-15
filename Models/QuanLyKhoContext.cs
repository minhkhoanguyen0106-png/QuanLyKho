using Microsoft.EntityFrameworkCore;

namespace QuanLyKho.Models
{
    public class QuanLyKhoContext : DbContext
    {
        public QuanLyKhoContext(DbContextOptions<QuanLyKhoContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<NCC> NCCs { get; set; }
        public DbSet<GiaoDich> GiaoDiches { get; set; }
        public DbSet<BaoCao> BaoCaos { get; set; }
        public DbSet<KhoHang> KhoHangs { get; set; }
        public DbSet<KiemKho> KiemKhos { get; set; }
        public DbSet<NhapHang> NhapHangs { get; set; }
        public DbSet<XuatHang> XuatHangs { get; set; }
        public DbSet<TaoBaoCao> TaoBaoCaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fix cảnh báo decimal DonGia
            modelBuilder.Entity<HangHoa>()
                .Property(p => p.DonGia)
                .HasPrecision(18, 2);

            modelBuilder.Entity<KhoHang>()
                .Property(p => p.DonGia)
                .HasPrecision(18, 2);

            modelBuilder.Entity<NhapHang>()
                .Property(p => p.DonGia)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}

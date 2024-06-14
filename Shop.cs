using practise_app_first_code.models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace practise_app_first_code
{
    public partial class ShopDbContext : DbContext
    {
        public ShopDbContext()
            : base("name=Shop")
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // ������������� ��������� "���� �� ������" ����� �������� � ����������
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("OrderProducts");
                    m.MapLeftKey("OrderId");
                    m.MapRightKey("ProductId");
                });

            // ������������� ����� "���� � ������" ����� �������� � ���������
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Client)
                .WithMany()
                .HasForeignKey(o => o.ClientId);
        }
    }
}

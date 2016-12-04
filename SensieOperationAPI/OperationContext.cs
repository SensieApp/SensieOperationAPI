namespace SensieOperationAPI
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OperationContext : DbContext
    {
        public OperationContext()
            : base("name=OperationContext")
        {
        }

        public virtual DbSet<ActivationCode> ActivationCodes { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivationCode>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<ActivationCode>()
                .Property(e => e.Tag)
                .IsUnicode(false);

            modelBuilder.Entity<ActivationCode>()
                .HasMany(e => e.Modules)
                .WithMany(e => e.ActivationCodes)
                .Map(m => m.ToTable("ModulesActivationCodes").MapLeftKey("ActivationCodeId").MapRightKey("ModuloId"));

            modelBuilder.Entity<ActivationCode>()
                .HasMany(e => e.Users)
                .WithMany(e => e.ActivationCodes)
                .Map(m => m.ToTable("UsersActivationCodes").MapLeftKey("ActivationCodeId").MapRightKey("UserId"));

            modelBuilder.Entity<Module>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.Environment)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Comments)
                .IsUnicode(false);
        }
    }
}

using System.Data.Entity;

namespace SensieOperationAPI.Models
{
    public partial class OperationContext : DbContext
    {
        public OperationContext()
            : base("name=OperationContext")
        {
        }

        public virtual DbSet<ActivationCode> ActivationCodes { get; set; }
        public virtual DbSet<Activation> Activations { get; set; }
        public virtual DbSet<Duration> Durations { get; set; }
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
                .HasMany(e => e.Activations)
                .WithRequired(e => e.ActivationCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ActivationCode>()
                .HasMany(e => e.Modules)
                .WithMany(e => e.ActivationCodes)
                .Map(m => m.ToTable("ModulesActivationCodes").MapLeftKey("ActivationCodeId").MapRightKey("ModuloId"));

            modelBuilder.Entity<Activation>()
                .Property(e => e.Referencias)
                .IsUnicode(false);

            modelBuilder.Entity<Duration>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.ModuloKey)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .Property(e => e.Name)
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

            modelBuilder.Entity<User>()
                .HasMany(e => e.Activations)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}

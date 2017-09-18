using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PragmaticTalks.Data
{
    public class PragmaticContext : IdentityDbContext<Speaker>
    {
        public PragmaticContext(DbContextOptions<PragmaticContext> options) : base(options)
        {
        }

        public DbSet<Talk> Talks { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TalkTag> TalkTags { get; set; }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Speaker>().ToTable("Speakers");

            modelBuilder.Entity<Event>().HasMany(e => e.Talks).WithOne(t => t.Event).HasForeignKey(t => t.EventId);

            modelBuilder.Entity<Talk>().Property(t => t.Title).HasMaxLength(37);

            modelBuilder.Entity<TalkTag>().HasKey(tt => new { tt.TalkId, tt.TagId });

            modelBuilder.Entity<TalkTag>()
                .HasOne(tt => tt.Talk)
                .WithMany(b => b.Tags)
                .HasForeignKey(tt => tt.TalkId);

            modelBuilder.Entity<TalkTag>()
                .HasOne(tt => tt.Tag)
                .WithMany()
                .HasForeignKey(tt => tt.TagId);
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace PragmaticTalks.Model
{
    public class PragmaticContext : DbContext
    {
        public PragmaticContext(DbContextOptions<PragmaticContext> options) : base(options)
        {
        }

        public DbSet<Talk> Talks { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<TalkTag> TalkTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

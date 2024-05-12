using CalendarDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.Linq.Expressions;

namespace CalendarDbContext.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Calendar> Calendars { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Calendar>()
                .HasKey(x=>x.Name);

            modelBuilder.Entity<Calendar>()
                .Property(x => x.Weekend)
                .HasConversion(new JsonAffectedBYDateConvertor<List<DayOfWeek>>());
                
            modelBuilder.Entity<Calendar>()
                .HasMany(x => x.Events)
                .WithOne()
                .HasForeignKey("calendarName")
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DateEvent>()
                .HasKey(x => new { x.Date, x.Description });

            modelBuilder.Entity<DateEvent>()
                .Property(x => x.Date)
                .HasColumnType("date");

            modelBuilder.Entity<DateEvent>()
                .Property<string>("calendarName")
                .IsRequired();
            
            modelBuilder.Entity<DateEvent>();
        }
    }
}

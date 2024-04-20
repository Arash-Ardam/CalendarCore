using CalendarDomain;
using DataStructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using System.Text.Json;

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
                //.HasConversion(new AffectedBYDateConvertor<List<DayOfWeek>>());
                .HasConversion(new JsonAffectedBYDateConvertor<List<DayOfWeek>>());


            modelBuilder.Entity<DateEvent>()
                .HasKey(x => new { x.Date, x.Description });

            modelBuilder.Entity<DateEvent>()
                .Property(x => x.Date)
                .HasColumnType("date");
        }
    }


    public class AffectedBYDateConvertor<T> : ValueConverter<AffectedByDateCollection<T>, List<KeyValuePair<DateTime, T>>>
    {
        public AffectedBYDateConvertor() 
            : base(abd => abd.Values, lkvp => AffectedByDateCollection<T>.CreateByList(lkvp), null)
        {
        }
    }

    public class JsonAffectedBYDateConvertor<T> : ValueConverter<AffectedByDateCollection<T>, string>
    {
        public JsonAffectedBYDateConvertor()
            : base(v => Serialize(v), v => Deserialize(v), null)
        {
        }

        private static AffectedByDateCollection<T> Deserialize(string str)
        {
            var obj = JsonSerializer.Deserialize<AffectedByDateCollection<T>>(str, new JsonSerializerOptions());

            return obj;
        }

        private static string Serialize(AffectedByDateCollection<T> v)
        {
            var str = JsonSerializer.Serialize(v, new JsonSerializerOptions());

            return str;
        }
    }
}

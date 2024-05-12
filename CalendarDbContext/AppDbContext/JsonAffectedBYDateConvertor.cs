using DataStructures;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace CalendarDbContext.AppDbContext
{
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

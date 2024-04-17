using System.Data;

namespace DataStructures
{
    public interface IAffectedByDateCollection<T>
    {
        void Add(DateTime dateTime, T value);
        T Get(DateTime dateTime);
        void Modify(DateTime dateTime, T v);
        void Remove(DateTime dateTime);
    }

    public class AffectedByDateCollection<T> : IAffectedByDateCollection<T>
    {
        public List<KeyValuePair<DateTime, T>> Values { get; set; } = new List<KeyValuePair<DateTime, T>>();


        //ctors
        public AffectedByDateCollection()
        {
        }

        public static AffectedByDateCollection<T> CreateByList(List<KeyValuePair<DateTime, T>> values)
        {
            var result = new AffectedByDateCollection<T>();

            foreach (var kvp in values)
            {
                result.Add(kvp.Key, kvp.Value);
            }

            return result;
        }

        // null pattern
        private static AffectedByDateCollection<T> empty = new AffectedByDateCollection<T>();
        public static AffectedByDateCollection<T> _Empty { get { return empty; } }

        //Logics

        public void Add(DateTime dateTime, T value)
        {
            var isAnyGraterKey = Values.Any(x => x.Key > dateTime);
            var isDuplicatedKey = Values.Any(x => x.Key == dateTime);

            if (isDuplicatedKey)
            {
                throw new DuplicateNameException(nameof(dateTime));
            }

            if (isAnyGraterKey)
            {
                throw new ArgumentOutOfRangeException(nameof(isAnyGraterKey));
            }

            var dateDataPair = new KeyValuePair<DateTime, T>(dateTime, value);

            this.Values.Add(dateDataPair);
        }



        public T Get(DateTime dateTime)
        {
            T result = Values.FindLast(x => dateTime >= x.Key ).Value;

            if (result?.Equals(default(T)) ?? true)
            {
                throw new ArgumentOutOfRangeException(nameof(result));  
            }

            return result;
        }

        
        public void Modify(DateTime dateTime, T v)
        {
            Remove(dateTime);
            Add(dateTime, v);
        }

        public void Remove(DateTime dateTime)
        {
            Values.Remove(Values.FirstOrDefault(x => dateTime == x.Key));
        }
    }
}

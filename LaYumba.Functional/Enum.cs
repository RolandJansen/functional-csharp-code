namespace LaYumba.Functional
{
    using System.Collections.Generic;
    using System.Linq;
    using static F;

    public static class Enum
    {
        public static bool TryParse<T>(this string s, out T t)
        {
            IEnumerable<T> all = System.Enum.GetValues(typeof(T)).Cast<T>();
            Dictionary<string, T> sensitiveNames = all.ToDictionary(k => System.Enum.GetName(typeof(T), k));

            return sensitiveNames.TryGetValue(s, out t);
        }

        public static Option<T> Parse<T>(this string s) where T : struct
           => Enum.TryParse(s, out T t) ? Some(t) : None;
    }
}

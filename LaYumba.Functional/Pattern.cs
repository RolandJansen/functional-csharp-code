using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Reflection;

namespace LaYumba.Functional
{
   using static F;

   public class NonExhaustivePattern : Exception { }

   // changed type dynamic -> object
   public class Pattern : Pattern<object> { }

   public class Pattern<R> : IEnumerable
   {
      IList<(Type, Func<object, R>)> funcs = new List<(Type, Func<object, R>)>();

      IEnumerator IEnumerable.GetEnumerator() => funcs.GetEnumerator();

      public void Add<T>(Func<T, R> func) 
         => funcs.Add((typeof(T), o => func((T)o)));

      public Pattern<R> Default(Func<R> func)
      {
         Add((object _) => func());
         return this;
      }

      public Pattern<R> Default(R val)
      {
         Add((object _) => val);
         return this;
      }

      public R Match(object value)
      {
         Func<object, R> matchingDel = null;
         try
         {
            matchingDel = funcs.First(InputArgMatchesTypeOf(value)).Item2;
         }
         catch(InvalidOperationException)
         {
            throw new NonExhaustivePattern();
         }

         return matchingDel(value);
      }

      // changed tup.Item1.GetTypeInfo() to ... GetType()
      static Func<(Type, Func<object, R>), bool> InputArgMatchesTypeOf(object value)
         => tup => tup.Item1.GetType().IsAssignableFrom(value.GetType());
   }
}

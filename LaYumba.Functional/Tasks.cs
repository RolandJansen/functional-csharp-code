using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LaYumba.Functional
{
    /// <summary>
    /// This adds functionality that was introduced
    /// to the Task class in .NET Framework 4.5.
    /// It's not very elegant because the code that is using it
    /// must be changed (a bit). Unfortunately there seems to be no other solution
    /// for this:
    /// https://stackoverflow.com/questions/49281745/what-is-the-equivalent-of-task-fromresult-from-net-4-5-in-net-3-5
    /// </summary>
    public class Tasks
    {
        public static Task<TResult> FromResult<TResult>(TResult result)
        {
            var tcs = new TaskCompletionSource<TResult>();
            tcs.SetResult(result);
            return tcs.Task;
        }

        //public static Task WhenAll(IEnumerable<Task> tasks)
        //{
        //    return Task.Factory.ContinueWhenAll(tasks, (t) => t);
        //}

        public static Task WhenAll(params Task[] tasks)
        {
            return Task.Factory.ContinueWhenAll(tasks, (t) => t);
        }

        public static Task Delay(int millisecondsDelay)
        {
            var tcs = new TaskCompletionSource<object>();
            new Timer(_ => tcs.SetResult(null)).Change(millisecondsDelay, -1);
            return tcs.Task;
        }
    }
}

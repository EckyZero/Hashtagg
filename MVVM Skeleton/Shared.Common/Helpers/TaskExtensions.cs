using System;
using System.Threading.Tasks;

namespace Shared.Common
{
	public static class TaskExtensions
	{
		public static async Task<T> TimeoutAfter<T>(this Task<T> task, int millisecondsTimeout)
		{
			if (task == await Task.WhenAny (task, Task.Delay (millisecondsTimeout))) {
				return await task;
			} else {
				return default(T);
			}
		}
	}
}


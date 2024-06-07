using Hydro;
using Microsoft.Extensions.Caching.Memory;

namespace HydroPrototype.Pages.Components;

public class Polling(IMemoryCache memoryCache) : HydroComponent
{
	private static string CacheKey => "default";

	public int NotificationsCount { get; set; }

	public override void Mount()
	{
		NotificationsCount = memoryCache.GetOrCreate($"polling-{CacheKey}", _ => 0);
	}

	[Poll(Interval = 10_000)]
	public async Task Refresh()
	{
		var random = new Random();
		
		NotificationsCount += await Task.Run(() => random.Next(0, 10));

		memoryCache.Set($"polling-{CacheKey}", NotificationsCount);
	}
}
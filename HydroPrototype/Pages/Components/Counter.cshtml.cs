using Hydro;
using Microsoft.Extensions.Caching.Memory;

namespace HydroPrototype.Pages.Components;

public class Counter(IMemoryCache memoryCache) : HydroComponent
{
	public string CacheKey { get; set; } = "default";
	
	public int Count { get; set; }

	public override void Mount()
	{
		Count = memoryCache.Get<int>($"count-{CacheKey}");
	}

	public void Add()
	{
		Count++;
		memoryCache.Set($"count-{CacheKey}", Count);
		DispatchGlobal(new CountChangedEvent(CacheKey, Count));
	}
}
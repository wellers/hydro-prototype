using Hydro;
using Microsoft.Extensions.Caching.Memory;

namespace HydroPrototype.Pages.Components;

public class ContactsList : HydroComponent
{
	private readonly IMemoryCache _memoryCache;
	
	public string CacheKey { get; init; } = "default";

	public ContactsList(IMemoryCache memoryCache)
	{
		_memoryCache = memoryCache;
		Subscribe<ContactAddedEvent>(Handle);
	}
	
	public List<Contact>? Contacts = [];

	public override async Task MountAsync()
	{
		// simulate async call to retrieve data
		var contacts = await Task.Run<List<Contact>>(() => [new Contact("Mr","Testy", "McTest")]);
		
		Contacts = _memoryCache.GetOrCreate<List<Contact>>($"contacts-{CacheKey}", _ => contacts);
	}
	
	public void Handle(ContactAddedEvent contactAddedEvent)
	{
		Contacts = _memoryCache.Get<List<Contact>>($"contacts-{CacheKey}");
		
		Contacts?.Add(contactAddedEvent.Contact);
		_memoryCache.Set($"contacts-{CacheKey}", Contacts);
	}
}

public record Contact(string Title, string FirstName, string LastName);

public record ContactAddedEvent(Contact Contact);
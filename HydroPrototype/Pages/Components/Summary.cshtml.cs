using Hydro;

namespace HydroPrototype.Pages.Components;

public class Summary : HydroComponent
{
	public Summary()
	{
		Subscribe<CountChangedEvent>(Handle);
	}
	
	public string Text { get; set; } 

	public void Handle(CountChangedEvent changedEvent)
	{
		Text = $"{changedEvent.Key} button clicked for the {changedEvent.Count} time!";
	}
}

public record CountChangedEvent(string Key, int Count);
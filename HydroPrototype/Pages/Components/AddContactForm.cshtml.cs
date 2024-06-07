using Hydro;
using System.ComponentModel.DataAnnotations;

namespace HydroPrototype.Pages.Components;

public class AddContactForm : HydroComponent
{
	[Required]
	public string Title { get; set; } = "";
	[Required]
	public string FirstName { get; set; } = "";
	[Required]
	public string LastName { get; set; } = "";

	public void Submit()
	{
		if (!Validate())
			return;
		
		DispatchGlobal(new ContactAddedEvent(new Contact(Title, FirstName, LastName)));
		Title = "";
		FirstName = "";
		LastName = "";
	}
}
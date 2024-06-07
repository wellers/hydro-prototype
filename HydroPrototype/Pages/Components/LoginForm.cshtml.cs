using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Principal;
using Hydro;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace HydroPrototype.Pages.Components;

public class LoginForm : HydroComponent
{
	public bool Authenticated { get; set; }
	
	[Required]
	public string Username { get; set; } = "";
	[Required]
	public string Password { get; set; } = "";

	public override void Mount()
	{
		Authenticated = HttpContext.User.Identity?.IsAuthenticated ?? false;
		if (Authenticated)
			Redirect(Url.Page("/Login"));
	}

	public async Task Submit()
	{
		if (!Validate())
		{
			Username = "";
			Password = "";
			return;
		}
		
		var claims = new List<Claim>
		{
			new (ClaimTypes.Name, Username),
			new (ClaimTypes.Role, "Administrator")
		};

		var claimsIdentity = new ClaimsIdentity(
			claims, CookieAuthenticationDefaults.AuthenticationScheme);

		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(claimsIdentity),
			new AuthenticationProperties
			{
				IsPersistent = true
			});
		
		Redirect(Url.Page("/Login"));
	}
}
using Hydro;

namespace HydroPrototype.Attributes;

public class ComponentAuthorizeAttribute : Attribute, IHydroAuthorizationFilter
{
	public Task<bool> AuthorizeAsync(HttpContext httpContext, object component)
	{
		var isAuthorised = httpContext.User.Identity?.IsAuthenticated ?? false;
		return Task.FromResult(isAuthorised);
	}
}
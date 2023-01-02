using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace IdentityServerSample.IdentityApp.Filters
{
  public class ValidateAntiforgeryTokenAuthorizationFilter : IAsyncAuthorizationFilter, IAntiforgeryPolicy
  {
    private readonly IAntiforgery _antiforgery;

    public ValidateAntiforgeryTokenAuthorizationFilter(IAntiforgery antiforgery)
    {
      _antiforgery = antiforgery ?? throw new ArgumentNullException(nameof(antiforgery));
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
      if (HttpMethods.IsPost(context.HttpContext.Request.Method) ||
          HttpMethods.IsPut(context.HttpContext.Request.Method) ||
          HttpMethods.IsDelete(context.HttpContext.Request.Method))
      {
        try
        {
          await _antiforgery.ValidateRequestAsync(context.HttpContext);
        }
        catch (AntiforgeryValidationException)
        {
          context.Result = new AntiforgeryValidationFailedResult();
        }
      }
    }
  }
}

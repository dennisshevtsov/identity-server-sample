using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityServerSample.IdentityApp.Filters
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
  public sealed class ValidateAntiForgeryTokenAttribute : Attribute, IOrderedFilter, IFilterFactory
  {
    public int Order => 1000;

    public bool IsReusable => true;

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
      => serviceProvider.GetRequiredService<ValidateAntiforgeryTokenAuthorizationFilter>();
  }
}

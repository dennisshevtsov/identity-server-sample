// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace IdentityServerSample.IdentityApp.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  /// <summary>Provides a simple API to handle HTTP requests.</summary>
  [ApiController]
  [Route("api/error")]
  [Produces("application/json")]
  public sealed class ErrorController : ControllerBase
  {
  }
}

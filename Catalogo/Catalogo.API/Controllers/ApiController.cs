﻿using Microsoft.AspNetCore.Mvc;

namespace Catalogo.API.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiController: ControllerBase
{
}

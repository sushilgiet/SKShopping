using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebView.Controllers
{
    public class BaseController : Controller
    {
      public IConfiguration Configuration { get; set; }
      public BaseController(IConfiguration confugration)
      {
            Configuration = confugration;
      }
    }
}

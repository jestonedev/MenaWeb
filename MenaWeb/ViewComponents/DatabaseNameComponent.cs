using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenaWeb.ViewComponents
{
    public class DatabaseNameComponent : ViewComponent
    {
        private IConfiguration config;

        public DatabaseNameComponent(IConfiguration config)
        {
            this.config = config;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Database = config.GetValue<string>("Database");
            return View("DatabaseName");
        }
    }
}

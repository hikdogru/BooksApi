﻿using Microsoft.AspNetCore.Mvc;
using System;
//using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.Api.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/RedirectToUpdateMethod")]
        public IActionResult RedirectToUpdateMethod(string name)
        {
            return RedirectToAction("Update", "Book");
        }
    }
}

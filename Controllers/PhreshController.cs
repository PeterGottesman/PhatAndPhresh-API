using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhatAndPhresh.Models;

namespace PhatAndPhresh.Controllers
{
    [Route("api/[controller]")]
    public class PhreshController : Controller
    {
        private readonly IRapGenerator m_RapGenerator;

        public PhreshController(IRapGenerator rapGenerator)
		{
			m_RapGenerator = rapGenerator;
		}

        // GET api/phresh
        [HttpGet]
        public Rap Get()
        {
            string versesString = HttpContext.Request.Query["verses"];
            int verses = 1;

            if (!String.IsNullOrEmpty(versesString))
            {
                verses = int.Parse(versesString);
            }

            return m_RapGenerator.Generate(verses);
        }
    }
}

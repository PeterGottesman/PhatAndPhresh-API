using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public string Get()
        {
            return m_RapGenerator.Generate(2);
        }
    }
}

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

        private const int MaxVerses = 10;

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

            if (verses > MaxVerses)
            {
                string errorString = $"ERROR: You cannot request more than {MaxVerses} verses.";

                return new Rap
                {
                    Verses = new List<string>(new string[]{errorString}),
                    Rhymes = new List<string>()
                };
            }

            return m_RapGenerator.Generate(verses);
        }
    }
}

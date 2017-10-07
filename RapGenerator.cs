using System;
using System.Collections.Generic;
using System.Linq;

namespace PhatAndPhresh
{
    public class RapGenerator : IRapGenerator
    {
        private readonly IRhymeGenerator m_RhymeGenerator;

        private List<string> m_templates;

		public RapGenerator(IRhymeGenerator rhymeGenerator)
		{
            m_RhymeGenerator = rhymeGenerator;

			string templates = System.IO.File.ReadAllText("templates.txt");
			m_templates = templates.Split('\n').ToList();
		}

        public string Generate()
        {
            string rhyme = m_RhymeGenerator.GetRhyme("Gun", WordType.Any);
            string rap = $"Yo, my gun is {rhyme}, yo";

            Random rand = new Random();

            string verse = m_templates.ElementAt(rand.Next(m_templates.Count));

            verse.IndexOf('<');
            verse.IndexOf('>');

            return verse;
        }
    }
}

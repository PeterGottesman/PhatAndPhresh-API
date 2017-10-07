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

			string templates = System.IO.File.ReadAllText("./wwwroot/templates.txt");
			m_templates = templates.Split('\n').ToList();
		}

        public string Generate()
        {
            Random rand = new Random();
            string verse = m_templates.ElementAt(rand.Next(m_templates.Count));

			int tag_count = verse.Count(x => x == '<');
            int start_token = 0;
            int end_token = 0;

            while (tag_count > 0)
            {
                start_token = verse.IndexOf('<', start_token);
                end_token = verse.IndexOf('>', start_token);
				end_token -= start_token;
                string token_tag = verse.Substring(start_token + 1, end_token - 1);
				string rhyme = m_RhymeGenerator.GetRhyme("cunt", WordType.Any);
                verse = verse.Remove(start_token, end_token + 1).Insert(start_token, rhyme);
                tag_count--;
			}

            return verse;
        }
    }
}

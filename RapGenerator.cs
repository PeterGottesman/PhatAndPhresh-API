using System;
using System.Collections.Generic;
using System.Linq;

namespace PhatAndPhresh
{
    public class RapGenerator : IRapGenerator
    {
        private readonly IRhymeGenerator m_RhymeGenerator;

        private List<string> m_templates;
		private List<string> m_nouns;
		private List<string> m_adjectives;
		private List<string> m_verbs;


		public RapGenerator(IRhymeGenerator rhymeGenerator)
		{
            m_RhymeGenerator = rhymeGenerator;

			string templates = System.IO.File.ReadAllText("./wwwroot/templates.txt");
			m_templates = templates.Split('\n').ToList();
			string nouns = System.IO.File.ReadAllText("./wwwroot/nouns.txt");
            m_nouns = nouns.Split('\n').ToList();
			string adjectives = System.IO.File.ReadAllText("./wwwroot/adjectives.txt");
            m_adjectives = adjectives.Split('\n').ToList();
			string verbs = System.IO.File.ReadAllText("./wwwroot/verbs.txt");
            m_verbs = verbs.Split('\n').ToList();

		}

        public string Generate()
        {
            Random rand = new Random();
            string verse = m_templates.ElementAt(rand.Next(m_templates.Count));
			string base_word = m_nouns.ElementAt(rand.Next(m_nouns.Count));

			int tag_count = verse.Count(x => x == '<');
            int start_token = 0;
            int end_token = 0;

            while (tag_count > 0)
            {
                start_token = verse.IndexOf('<', start_token);
                end_token = verse.IndexOf('>', start_token);
				end_token -= start_token;
                string token_tag = verse.Substring(start_token + 1, end_token - 1);
                WordType pos;
                switch (token_tag)
                {
                    case "noun":
                        pos = WordType.Noun;
                        break;
                    case "adjective":
                        pos = WordType.Adjective;
                        break;
                    case "verb":
                        pos = WordType.Verb;
                        break;
                    default:
                        pos = WordType.Any;
                        break;
                }

                string rhyme = m_RhymeGenerator.GetRhyme(base_word, pos);
                verse = verse.Remove(start_token, end_token + 1).Insert(start_token, rhyme);
                tag_count--;
			}

            return verse;
        }
    }
}

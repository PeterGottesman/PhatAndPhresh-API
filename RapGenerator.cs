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

            List<string> verse_list = verse.Split(' ').ToList();
            bool base_hit = false;
            string base_word = "";

			for (int i = 0; i < verse_list.Count(); ++i)
            {
                string word = verse_list[i];

                if (!base_hit && word.ElementAt(0) == '<')
                {
                    int end_index = word.IndexOf('>');
                    string base_type = word.Substring(1, end_index - 1);
					base_word = GetBaseWord(base_type);
                    verse_list[i] = base_word;
                    base_hit = true;
					if (word.Contains(',')) { verse_list[i] += ','; }
				}
                else if(base_hit && word.ElementAt(0) == '<')
                {
					int end_index = word.IndexOf('>');
                    string base_type = word.Substring(1, end_index - 1);
					WordType pos;
                    switch (base_type)
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
                    verse_list[i] = rhyme;
                    base_word = rhyme;
					if (word.Contains(',')) { verse_list[i] += ','; }
				}


            }

            verse = verse_list.Aggregate((a, b) => a + ' ' + b);

            // Make the entire thing lowercase
            verse = verse.ToLower();

            // Make the first letter uppercase
            verse = char.ToUpper(verse[0]) + verse.Substring(1);

            return verse;
        }

        /// <summary>
        /// Gets the base word based on the base_type (Part of Speech) passed in.
        /// </summary>
        /// <returns>The base word.</returns>
        /// <param name="base_type">Part of speech.</param>
        private string GetBaseWord(string base_type)
        {
            
			Random rand = new Random();
			string base_word;
			switch (base_type)
			    {
			        case "noun":
                        base_word = m_nouns.ElementAt(rand.Next(m_nouns.Count));
			            break;
			        case "adjective":
                        base_word = m_adjectives.ElementAt(rand.Next(m_adjectives.Count));
    			        break;
			        case "verb":
					    base_word = m_verbs.ElementAt(rand.Next(m_verbs.Count));
					    break;
			        default:
                        base_word = "cunt";
			            break;
			    }

            return base_word;
		}
    }
}

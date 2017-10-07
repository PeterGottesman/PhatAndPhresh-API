using System;
using System.Collections.Generic;
using System.Linq;
using PhatAndPhresh.Models;

namespace PhatAndPhresh
{
    public class RapGenerator : IRapGenerator
    {
        readonly IRhymeGenerator m_RhymeGenerator;

        // Templates used to generated verses
        public List<string> Templates { get; private set; }

        // Word lists used for base words
        public List<string> Nouns { get; private set; }
        public List<string> Adjectives { get; private set; }
        public List<string> Verbs { get; private set; }

        const string TemplatesPath = "./wwwroot/templates.txt";
        const string NounsPath = "./wwwroot/nouns.txt";
        const string AdjectivesPath = "./wwwroot/adjectives.txt";
        const string VerbsPath = "./wwwroot/verbs.txt";

        // Verse generation parameters
        const int MinRhymes = 2;
        const int MaxRhymes = 5;
        const string DefaultBaseWord = "Bitch";

		public RapGenerator(IRhymeGenerator rhymeGenerator)
		{
            m_RhymeGenerator = rhymeGenerator;

            // Load required data from files
            Templates = System.IO.File.ReadAllText(TemplatesPath).Split('\n').ToList();
            Nouns = System.IO.File.ReadAllText(NounsPath).Split('\n').ToList();
            Adjectives = System.IO.File.ReadAllText(AdjectivesPath).Split('\n').ToList();
            Verbs = System.IO.File.ReadAllText(VerbsPath).Split('\n').ToList();
		}

        public Rap Generate(int verseCount)
        {
            Random rand = new Random();
			int rhymeCount = rand.Next(MinRhymes, MaxRhymes);

            // Generates a list of random verses to the specified verse_count
            List<string> verses = new List<string>(verseCount);
            for (int i = 0; i < verseCount; i++)
            {
                verses.Add(Templates.ElementAt(rand.Next(Templates.Count)));
			}

            List<string> rhymes = new List<string>();

            bool baseHit = false;
            string baseWord = null;
            string baseType = null;
            for (int i = 0; i < verseCount; ++i)
            {
                string verse = verses.ElementAt(i);
                List<string> verseWords = verse.Split(' ').ToList();

                for (int j = 0; j < verseWords.Count(); ++j)
				{
                    string word = verseWords.ElementAt(j);

                    if (!baseHit && (word.ElementAt(0) == '<'))
					{
                        int endIndex = word.IndexOf('>');

						baseType = word.Substring(1, endIndex - 1);
						baseWord = GetBaseWord(baseType);
                        rhymes.Add(baseWord.ToLower());

						verseWords[j] = baseWord;
                            
                        // Preserve comma
						if (word.Contains(','))
                        {
                            verseWords[j] += ',';
                        }

                        //rhymeCount = rand.Next(MinRhymes, MaxRhymes);

                        baseHit = true;
					}
                    else if (baseHit && (word.ElementAt(0) == '<'))
					{
                        int endIndex = word.IndexOf('>');
						WordType wordType;

						baseType = word.Substring(1, endIndex - 1);
						switch (baseType)
						{
							case "noun":
								wordType = WordType.Noun;
								break;
							case "adjective":
								wordType = WordType.Adjective;
								break;
							case "verb":
								wordType = WordType.Verb;
								break;
							default:
								wordType = WordType.Any;
								break;
						}

						string rhyme = null;

						rhyme = m_RhymeGenerator.GetRhyme(baseWord, wordType);
						rhymes.Add(rhyme.ToLower());
						rhymeCount--;

						verseWords[j] = rhyme;

						// Preserve comma
						if (word.Contains(','))
                        {
                            verseWords[j] += ',';
                        }
					}

                    //if (rhymeCount < 2)
                    //{
                    //    rhymeCount = rand.Next(MinRhymes, MaxRhymes);
                    //    baseHit = false;
                    //}
				}

                // Join the words back into a verse
                verse = verseWords.Aggregate((a, b) => (a + ' ' + b));

				// Make the entire thing lowercase
				verse = verse.ToLower();

				// Make the first letter uppercase
				verse = char.ToUpper(verse[0]) + verse.Substring(1);

                // Replace the original verse with the phire one
                verses[i] = verse;
			}

            Rap rap = new Rap
            {
                Verses = verses,
                Rhymes = rhymes
            };

            return rap;
        }

        /// <summary>
        /// Gets the base word based on the type (Part of Speech) passed in.
        /// </summary>
        /// <returns>The base word.</returns>
        /// <param name="baseType">Part of speech.</param>
        private string GetBaseWord(string baseType)
        {
			Random rand = new Random();

            string baseWord = null;
			switch (baseType)
		    {
		        case "noun":
                    baseWord = Nouns.ElementAt(rand.Next(Nouns.Count));
		            break;
		        case "adjective":
                    baseWord = Adjectives.ElementAt(rand.Next(Adjectives.Count));
			        break;
		        case "verb":
				    baseWord = Verbs.ElementAt(rand.Next(Verbs.Count));
				    break;
		        default:
                    baseWord = DefaultBaseWord;
		            break;
		    }

            return baseWord;
		}
    }
}

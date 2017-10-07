using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using PhatAndPhresh.Models;

namespace PhatAndPhresh
{
    public class RhymeGenerator : IRhymeGenerator
    {
		public string GetRhyme(string input, WordType type)
        {
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://api.datamuse.com/words");
                HttpResponseMessage webResponse = client.GetAsync($"words?rel_rhy={input}&md=p").Result;
				webResponse.EnsureSuccessStatusCode();

				string resultString = webResponse.Content.ReadAsStringAsync().Result;
                List<WordResponse> json = JsonConvert.DeserializeObject<List<WordResponse>>(resultString);

				// If there were no rhymes, just get a related word.
				if (json.Count == 0)
				{
                    return GetRelatedWord(input, type);
				}

				Random rand = new Random();
				WordResponse rhyme = null;

                if (type == WordType.Noun)
                {
                    var nouns = json.Where(r => ((r.tags != null) && r.tags.Contains("n"))).ToList();
                    if (nouns.Count() != 0) { rhyme = nouns.ElementAt(rand.Next(nouns.Count())); }
                }
                else if (type == WordType.Verb)
				{
					var verbs = json.Where(r => ((r.tags != null) && r.tags.Contains("v"))).ToList();
                    if (verbs.Count() != 0) { rhyme = verbs.ElementAt(rand.Next(verbs.Count())); }
				}
                else if (type == WordType.Adverb)
				{
					var adverbs = json.Where(r => ((r.tags != null) && r.tags.Contains("adv"))).ToList();
                    if (adverbs.Count() != 0) { rhyme = adverbs.ElementAt(rand.Next(adverbs.Count())); }
				}
                else if (type == WordType.Adjective)
				{
					var adjectives = json.Where(r => ((r.tags != null) && r.tags.Contains("adj"))).ToList();
					if (adjectives.Count() != 0) { rhyme = adjectives.ElementAt(rand.Next(adjectives.Count())); }
				}

                // If we couldn't find a specific type of word, just grab any of them.
                if (rhyme == null)
                {
                    rhyme = json.ElementAt(rand.Next(json.Count()));
                }

                return rhyme.Word;
			}
        }

		public string GetRelatedWord(string input, WordType type)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://api.datamuse.com/words");
				HttpResponseMessage webResponse = client.GetAsync($"words?rel_trg={input}&md=p").Result;
				webResponse.EnsureSuccessStatusCode();

				string resultString = webResponse.Content.ReadAsStringAsync().Result;
				List<WordResponse> json = JsonConvert.DeserializeObject<List<WordResponse>>(resultString);

				// If there were no related words, just return a random word.
				if (json.Count == 0)
				{
                    return GetRelatedWord("gangsta", type);
				}

				Random rand = new Random();
                WordResponse related = null;

				if (type == WordType.Noun)
				{
					var nouns = json.Where(r => (r.tags.Contains("n"))).ToList();
					related = nouns.ElementAt(rand.Next(nouns.Count()));
				}
				else if (type == WordType.Verb)
				{
					var verbs = json.Where(r => (r.tags.Contains("v"))).ToList();
					related = verbs.ElementAt(rand.Next(verbs.Count()));
				}
				else if (type == WordType.Adverb)
				{
					var adverbs = json.Where(r => (r.tags.Contains("adv"))).ToList();
					related = adverbs.ElementAt(rand.Next(adverbs.Count()));
				}
				else if (type == WordType.Adjective)
				{
					var adjectives = json.Where(r => (r.tags.Contains("adj"))).ToList();
					related = adjectives.ElementAt(rand.Next(adjectives.Count()));
				}

				// If we couldn't find a specific type of word, just grab any of them.
				if (related == null)
				{
					related = json.ElementAt(rand.Next(json.Count()));
				}

				return related.Word;
			}
		}
    }
}

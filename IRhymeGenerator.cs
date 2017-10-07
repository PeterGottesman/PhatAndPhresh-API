using System;

namespace PhatAndPhresh
{
    public interface IRhymeGenerator
    {
		/// <summary>Get a rhyming word.</summary>
		/// <param name="input">The word that you want to find a rhyme of.</param>
		/// <param name="type">The word type (Verb, noun, etc) that you want the rhyme to be.</param>
		/// <returns>The rhyming word.</returns>
		string GetRhyme(string input, WordType type);

		/// <summary>Get a related word.</summary>
		/// <param name="input">The word that you want to find a related word to.</param>
		/// <param name="type">The word type (Verb, noun, etc) that you want.</param>
		/// <returns>The related word.</returns>
		string GetRelatedWord(string input, WordType type);
    }

	public enum WordType
	{
		Noun,
        Verb,
        Adverb,
        Adjective,
        Any
	}
}

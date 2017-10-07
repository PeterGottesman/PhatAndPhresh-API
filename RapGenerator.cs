using System;

namespace PhatAndPhresh
{
    public class RapGenerator : IRapGenerator
    {
        private readonly IRhymeGenerator m_RhymeGenerator;

		public RapGenerator(IRhymeGenerator rhymeGenerator)
		{
            m_RhymeGenerator = rhymeGenerator;
		}

        public string Generate()
        {
            string rhyme = m_RhymeGenerator.GetRhyme("Spagett");
            string rap = $"Yo, Spagett my {rhyme}, yo";

            return rap;
        }
    }
}

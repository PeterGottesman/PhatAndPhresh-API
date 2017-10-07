﻿using System;
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
            string rhyme = m_RhymeGenerator.GetRhyme("Gun", WordType.Any);
            string rap = $"Yo, my gun is {rhyme}, yo";

            Random rand = new Random();

            string verse = m_templates.ElementAt(rand.Next(m_templates.Count));

            int start_token = verse.IndexOf('<');
            int end_token = verse.IndexOf('>');
            end_token = end_token - start_token;
            string token = verse.Substring(start_token + 1, end_token - 1);

            start_token = verse.IndexOf('<', start_token);
            end_token = verse.IndexOf('>', start_token);
			end_token = end_token - start_token;
			token = verse.Substring(start_token + 1, end_token - 1);

            token = m_RhymeGenerator.GetRhyme("cunt", WordType.Any);



            return token;
        }
    }
}

using System;
using System.Collections.Generic;

namespace PipelinesExercise
{
	public class ConfigFile
	{
		public List<CardData> ParseCards()
		{
			return new List<CardData>();
		}

		public static ConfigFile Matching(CharacterFile characterFile)
		{
			throw new NotImplementedException();
		}

		public static List<CardData> GetTheCards(ConfigFile c)
		{
			return c.ParseCards();
		}

        public static List<CardData> ParseCards(ConfigFile configFile)
        {
            return configFile.ParseCards();
        }
    }
}

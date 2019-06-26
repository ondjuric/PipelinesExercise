using System;
using System.Collections.Generic;

namespace PipelinesExercise
{
	public class CharacterFile
	{
		public static CharacterFile From( string fileName)
		{
			throw new NotImplementedException();
		}

		public List<CardData> ParseCards()
		{
			return new List<CardData>();
		}

		public void ResolveFormulasToValues(CardData card, ConfigFile configFile)
		{
			throw new NotImplementedException();
		}

		public static List<CardData> GetTheCards(CharacterFile c)
		{
			return c.ParseCards();
		}

        public static List<CardData> PartialCards(CharacterFile characterFile)
        {
            return characterFile.ParseCards();
        }
    }
}

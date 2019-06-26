using System;

namespace PipelinesExercise
{
	public class CardService
	{
		public static CardService Authenticate(Tuple< string, string> credentials)
		{
			throw new NotImplementedException();
		}
public static CardService Authenticate( string username, string password)
		{
			throw new NotImplementedException();
		}

		public void FetchDetailsInto(CardData card)
		{
			throw new NotImplementedException();
		}

		public void ResolveReferencesToOtherCards(CardData card)
		{
			throw new NotImplementedException();
		}
	}
}

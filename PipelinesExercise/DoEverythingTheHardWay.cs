using System.Linq;

namespace PipelinesExercise
{
    public class DoEverythingTheHardWay
    {
        public CharacterData MakeAllTheViewModels( string fileName,  string username,
             string password)
        {
            var characterFile = CharacterFile.From(fileName);
            var configFile = ConfigFile.Matching(characterFile);
            var partialCards = characterFile.ParseCards();
            var localCards = configFile.ParseCards();
            var compendiumService = CompendiumService.Authenticate(username, password);
            var cardService = CardService.Authenticate(username, password);
            foreach (var card in partialCards)
            {
                cardService.FetchDetailsInto(card);
                compendiumService.FillOutFlavorText(card);
                _LocateAndTranslateFormulas(card);
                cardService.ResolveReferencesToOtherCards(card);
            }
            foreach (var card in localCards.Concat(partialCards))
            {
                characterFile.ResolveFormulasToValues(card, configFile);
            }
            return new CharacterData(localCards.Concat(partialCards).Select(CardViewModel.From));
        }

        private void _LocateAndTranslateFormulas(CardData card)
        {
            throw new System.NotImplementedException();
        }
    }
}
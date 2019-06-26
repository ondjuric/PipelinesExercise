using System;
using System.Collections.Generic;
using System.Linq;

namespace PipelinesExercise
{
    public class DoEverythingTheEasyWay
    {
        public CharacterData MakeAllTheViewModels( string fileName,  string username,
             string password)
        {
            var characterFile = CharacterFile.From(fileName);
            var configFile = ConfigFile.Matching(characterFile);
            var partialCards = CharacterFile.PartialCards(characterFile);
            var localCards = ConfigFile.ParseCards(configFile);
            var compendiumService = CompendiumService.Authenticate(Tuple.Create(username, password));
            var cardService = CardService.Authenticate(Tuple.Create(username, password));

            var enrichedPartialCards = partialCards
                .Select(card => EnrichPartialCard(
                    Tuple.Create(Tuple.Create(cardService, compendiumService), card)))
                .ToArray();
            
            var allCardData = localCards.Concat(enrichedPartialCards);
            var allResolvedCardData = new List<CardData>();
            foreach (var card in allCardData)
            {
                characterFile.ResolveFormulasToValues(card, configFile);
                allResolvedCardData.Add(card);
            }

            var cardViewModels = allResolvedCardData.Select(CardViewModel.From);
            return new CharacterData(cardViewModels);
        }

        private CardData EnrichPartialCard(Tuple<Tuple<CardService, CompendiumService>, CardData> cards)
        {
            CardService cardService = cards.Item1.Item1;
            CompendiumService compendiumService = cards.Item1.Item2;
            CardData card = cards.Item2;

            cardService.FetchDetailsInto(card);
            compendiumService.FillOutFlavorText(card);
            _LocateAndTranslateFormulas(card);
            cardService.ResolveReferencesToOtherCards(card);
            var enrichedCard = card;
            return enrichedCard;
        }

        private void _LocateAndTranslateFormulas(CardData card)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Refactoring.Pipelines;
using Refactoring.Pipelines.Approvals;

namespace PipelinesExercise
{
    public class DoEverything
    {
        public CharacterData MakeAllTheViewModels( string fileName,  string username,
             string password)
        {
            var pipes = SetUpPipeline();
            
            // Send thru pipeline
            pipes.Item1.Send(fileName);
            pipes.Item2.Send(username);
            pipes.Item3.Send(password);
            
            // Original code
            return pipes.Item4.SingleResult;
        }

        public Tuple<InputPipe<string>, InputPipe<string>, InputPipe<string>, CollectorPipe<CharacterData>> SetUpPipeline()
        {
            // Setup Pipeline
            var fileNameInput = new InputPipe<string>("fileName");
            var usernamePipe = new InputPipe<string>("username");
            var passwordPipe = new InputPipe<string>("password");

            var fromPipe = fileNameInput.Process(CharacterFile.From);
            var configFilePipe = fromPipe.Process(ConfigFile.Matching);
            var partialCardsPipe = fromPipe.Process(ParseCards);
            var localCardsPipe = configFilePipe.Process(ParseCards);
            var credentialsPipe = usernamePipe.JoinTo(passwordPipe);
            var compendiumServicePipe = credentialsPipe.Process(CompendiumService);
            var cardServicePipe = credentialsPipe.Process(CardService);
            var foo = cardServicePipe.JoinTo(compendiumServicePipe).ApplyTo(partialCardsPipe);
            var processedPartialCardsPipe = foo.Process(ProcessAllPartialCards);
            var cardDataPipe = processedPartialCardsPipe.ConcatWith(localCardsPipe);
            var resolvingDataJoinPipe = fromPipe.JoinTo(configFilePipe).ApplyTo(cardDataPipe);
            var finalResult = resolvingDataJoinPipe.Process(ResolveAllFormulas).Process(CreateCharacterData).Collect();
            var pipes = Tuple.Create(fileNameInput, usernamePipe, passwordPipe, finalResult);
            return pipes;
        }

        private static CharacterData CreateCharacterData(IEnumerable<CardData> cardDatas)
        {
            return new CharacterData(cardDatas.Select(CardViewModel.From));
        }

        private static CardData ResolveFormulas(Tuple<Tuple<CharacterFile, ConfigFile>, CardData> cards)
        {
            cards.Item1.Item1.ResolveFormulasToValues(cards.Item2, cards.Item1.Item2);
            return cards.Item2;
        }

        private IEnumerable<CardData> ResolveAllFormulas(IEnumerable<Tuple<Tuple<CharacterFile, ConfigFile>, CardData>> cards)
        {
            return cards.Select(ResolveFormulas);
        }
        
        private IEnumerable<CardData> ProcessAllPartialCards(IEnumerable<Tuple<Tuple<CardService, CompendiumService>, CardData>> cards)
        {
            return cards.Select(ProcessPartialCards);
        }

        private CardData ProcessPartialCards(Tuple<Tuple<CardService, CompendiumService>, CardData > tuple)
        {
            var card = tuple.Item2;
            var cardService = tuple.Item1.Item1;
            var compendiumService = tuple.Item1.Item2;
            
            cardService.FetchDetailsInto(card);
            compendiumService.FillOutFlavorText(card);
            _LocateAndTranslateFormulas(card);
            cardService.ResolveReferencesToOtherCards(card);
            return card;
        }

        private static CardService CardService(Tuple<string, string> usernameAndPassword)
        {
            return PipelinesExercise.CardService.Authenticate(usernameAndPassword.Item1, usernameAndPassword.Item2);
        }

        private static CompendiumService CompendiumService(Tuple<string, string> usernameAndPassword)
        {
            return PipelinesExercise.CompendiumService.Authenticate(usernameAndPassword.Item1, usernameAndPassword.Item2);
        }

        private static List<CardData> ParseCards(ConfigFile configFile)
        {
            return configFile.ParseCards();
        }

        private static List<CardData> ParseCards(CharacterFile characterFile)
        {
            return characterFile.ParseCards();
        }

        private void _LocateAndTranslateFormulas(CardData card)
        {
            throw new System.NotImplementedException();
        }
    }
}
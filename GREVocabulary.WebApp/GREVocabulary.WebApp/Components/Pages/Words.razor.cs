using GREVocabulary.Business.Helpers;
using GREVocabulary.Business.Repository.IRepository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GREVocabulary.WebApp.Components.Pages;

public partial class Words
{
    [Inject]
    private IWordsRepository _wordsRepository { get; set; }

    [Inject]
    private ISpacedRepetitionSessionsRepository _spacedRepetitionSessionsRepository { get; set; }
    [Inject]

    private ISessionDetailsRepository _sessionDetailsRepository { get; set; }

    private List<StylizedWords> WordsByGroup { get; set; } = new();
    private List<int> AllGroupIds { get; set; } = new();
    private int StartingGroupId { get; set; }
    private int EndingGroupId { get; set; }
    private List<SessionWordResponse> SessionWordResponses { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        AllGroupIds = await _wordsRepository.GetAllGroupIds();
    }

    private async Task PopulateWordsList()
    {
        WordsByGroup.Clear();
        SessionWordResponses.Clear();

        if (StartingGroupId is 0 || EndingGroupId is 0) return;
        if (StartingGroupId > EndingGroupId) return;

        var words = await _wordsRepository.GetAllAsync(StartingGroupId, EndingGroupId);

        if (words is null || words.Count is 0) return;

        foreach (var word in words)
        {
            var wordDetails = new List<WordDetail>();

            foreach (var w in word.Words)
            {
                wordDetails.Add(new WordDetail
                {
                    WordToMemorize = w,
                    WordBootstrapClass = "btn btn-light",
                    GroupId = word.GroupId
                });
            }

            WordsByGroup.Add(new StylizedWords
            {
                Words = wordDetails
            });
        }
    }

    private async Task SaveSessionDetailsAsync()
    {
        if (SessionWordResponses is null || SessionWordResponses.Count is 0) return;

        // Check total number of Red and Green words in SessionWordResponses
        var redCount = SessionWordResponses.Count(x => x.IsRed);
        var greenCount = SessionWordResponses.Count(x => x.IsGreen);

        var totalCount = 0;
        foreach (var grouping in WordsByGroup)
        {
            totalCount += grouping.Words.Count;
        }

        var sessionDetails = new InsertSessionDetailsDto(redCount,
            greenCount,
            DateTime.Now,
            totalCount);

        var newSessionId = await _spacedRepetitionSessionsRepository.CreateSessionAsync(sessionDetails);

        if (newSessionId is null) return;

        var sessionWords = new List<InsertSessionWordDetails>();

        foreach (var entry in SessionWordResponses)
        {
            var dto = new InsertSessionWordDetails(entry.WordToMemorize,
                entry.GroupId,
                entry.IsRed ? 1 : 0,
                entry.IsGreen ? 1 : 0);
            sessionWords.Add(dto);
        }

        if (sessionWords.Count is 0) return;

        await _sessionDetailsRepository.InsertSessionWordAsync(newSessionId.Value, sessionWords);
    }

    private void ShuffleWordsWithinGroup()
    {
        if (WordsByGroup is null || WordsByGroup.Count is 0) return;

        foreach (var word in WordsByGroup)
        {
            word.Words.Shuffle();
        }
    }

    private void ShuffleEverything()
    {
        if (WordsByGroup is null || WordsByGroup.Count is 0) return;

        int noOfLists = WordsByGroup.Count;

        var combinedWords = new List<WordDetail>();

        foreach (var word in WordsByGroup)
        {
            combinedWords.AddRange(word.Words);
        }

        combinedWords.Shuffle();

        var resplitList = combinedWords.SplitList(noOfLists);

        WordsByGroup.Clear();

        if (resplitList is null || resplitList.Count is 0) return;

        foreach (var list in resplitList)
        {
            WordsByGroup.Add(new StylizedWords
            {
                Words = list
            });
        }
    }

    private void OnKeyPressed(KeyboardEventArgs e, WordDetail wordDetail)
    {
        if (e.Key == "g" || e.Key == "G")
        {
            wordDetail.WordBootstrapClass = "btn btn-success";

            var doesEntryExist = SessionWordResponses
                .Find(x => x.WordToMemorize == wordDetail.WordToMemorize);
            if (doesEntryExist != null)
            {
                SessionWordResponses.Remove(doesEntryExist);
            }

            var entry = new SessionWordResponse()
            {
                GroupId = wordDetail.GroupId,
                IsGreen = true,
                IsRed = false,
                WordToMemorize = wordDetail.WordToMemorize
            };

            SessionWordResponses.Add(entry);
        }
        else if (e.Key == "r" || e.Key == "R")
        {
            wordDetail.WordBootstrapClass = "btn btn-danger";

            var doesEntryExist = SessionWordResponses
                .Find(x => x.WordToMemorize == wordDetail.WordToMemorize);
            if (doesEntryExist != null)
            {
                SessionWordResponses.Remove(doesEntryExist);
            }

            var entry = new SessionWordResponse()
            {
                GroupId = wordDetail.GroupId,
                IsGreen = false,
                IsRed = true,
                WordToMemorize = wordDetail.WordToMemorize
            };

            SessionWordResponses.Add(entry);
        }
        else if (e.Key == "w" || e.Key == "W")
        {
            wordDetail.WordBootstrapClass = "btn btn-light";
        }
    }

    private sealed class SessionWordResponse
    {
        public string WordToMemorize { get; set; }
        public int GroupId { get; set; }
        public bool IsRed { get; set; }
        public bool IsGreen { get; set; }
    }

    private sealed class WordDetail
    {
        public string WordToMemorize { get; set; }
        public string WordBootstrapClass { get; set; }
        public int GroupId { get; set; }
    }

    private sealed class StylizedWords
    {
        public List<WordDetail> Words { get; set; }
    }
}
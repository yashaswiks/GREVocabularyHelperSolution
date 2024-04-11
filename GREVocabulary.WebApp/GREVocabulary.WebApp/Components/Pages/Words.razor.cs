using GREVocabulary.Business.Helpers;
using GREVocabulary.Business.Repository.IRepository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GREVocabulary.WebApp.Components.Pages;

public partial class Words
{
    [Inject]
    private IWordsRepository _wordsRepository { get; set; }

    private List<StylizedWords> WordsByGroup { get; set; } = new();
    private List<int> AllGroupIds { get; set; } = new();
    private int StartingGroupId { get; set; }
    private int EndingGroupId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        AllGroupIds = await _wordsRepository.GetAllGroupIds();
    }

    private async Task PopulateWordsList()
    {
        WordsByGroup.Clear();

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
        }
        else if (e.Key == "r" || e.Key == "R")
        {
            wordDetail.WordBootstrapClass = "btn btn-danger";
        }
        else if (e.Key == "w" || e.Key == "W")
        {
            wordDetail.WordBootstrapClass = "btn btn-light";
        }
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
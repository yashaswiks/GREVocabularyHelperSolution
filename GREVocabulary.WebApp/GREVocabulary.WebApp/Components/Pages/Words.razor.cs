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
                    WordBootstrapClass = "btn btn-light"
                });
            }

            WordsByGroup.Add(new StylizedWords
            {
                GroupId = word.GroupId,
                Words = wordDetails
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
    }

    private sealed class StylizedWords
    {
        public int GroupId { get; set; }
        public List<WordDetail> Words { get; set; }
    }
}
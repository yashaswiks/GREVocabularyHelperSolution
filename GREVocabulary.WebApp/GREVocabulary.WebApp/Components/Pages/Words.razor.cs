using GREVocabulary.Business;
using GREVocabulary.Business.Repository.IRepository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GREVocabulary.WebApp.Components.Pages;

public partial class Words
{
    [Inject]
    private IWordsRepository _wordsRepository { get; set; }

    private List<WordDetailsModel> WordsByGroup { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        WordsByGroup = await _wordsRepository.GetAllAsync();
    }

    private void OnKeyPressed(KeyboardEventArgs e, string word)
    {
        if (e.Key == "Enter")
        {
            // Do something
        }
    }
}
using GREVocabulary.Business.Models;
using GREVocabulary.Business.Repository.IRepository;
using Microsoft.AspNetCore.Components;

namespace GREVocabulary.WebApp.Components.Pages;

public partial class Sessions
{
    [Inject]
    private ISpacedRepetitionSessionsRepository _spacedRepetitionSessionsRepository { get; set; }

    private List<SpacedRepetitionSessionsModel> AllSessions { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        AllSessions = await _spacedRepetitionSessionsRepository.GetAllAsync();
    }
}
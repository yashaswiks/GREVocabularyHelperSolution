using GREVocabulary.Business.Models;
using GREVocabulary.Business.Repository.IRepository;
using Microsoft.AspNetCore.Components;

namespace GREVocabulary.WebApp.Components.Pages;

public partial class SessionDetails
{
    [Parameter]
    public int? SessionId { get; set; }

    [Inject]
    private ISessionDetailsRepository _sessionDetailsRepository { get; set; }

    public List<SessionDetailsModel> SessionWords { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        if (SessionId is null) return;

        SessionWords = await _sessionDetailsRepository
           .GetBySessionIdAsync(SessionId.Value);
    }
}
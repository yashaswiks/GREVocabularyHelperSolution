using GREVocabulary.Business.Models;

namespace GREVocabulary.Business.Repository.IRepository;

public interface ISpacedRepetitionSessionsRepository
{
    Task<int?> CreateSessionAsync(InsertSessionDetailsDto sessionDetails);

    Task<List<SpacedRepetitionSessionsModel>> GetAllAsync();
}

public record InsertSessionDetailsDto(
    int RedCount,
    int GreenCount,
    DateTime SessionTimestamp,
    int TotalCount);


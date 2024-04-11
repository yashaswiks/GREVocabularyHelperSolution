namespace GREVocabulary.Business.Repository.IRepository;

public interface ISpacedRepetitionSessionsRepository
{
    Task<int?> CreateSessionAsync(InsertSessionDetailsDto sessionDetails);
}

public record InsertSessionDetailsDto(
    int RedCount,
    int GreenCount,
    DateTime SessionTimestamp,
    int TotalCount);


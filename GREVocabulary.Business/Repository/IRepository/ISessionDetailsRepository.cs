namespace GREVocabulary.Business.Repository.IRepository;

public interface ISessionDetailsRepository
{
    Task<int?> InsertSessionWordAsync(int spacedRepetitionSessionId,
        List<InsertSessionWordDetails> insertSessionWordDetails);
}

public record InsertSessionWordDetails(
    string WordToMemorize,
    int GroupId,
    int Red,
    int Green);

using GREVocabulary.Business.Models;

namespace GREVocabulary.Business.Repository.IRepository;

public interface ISessionDetailsRepository
{
    Task<int?> InsertSessionWordAsync(int spacedRepetitionSessionId,
        List<InsertSessionWordDetails> insertSessionWordDetails);

    Task<List<SessionDetailsModel>> GetBySessionIdAsync(int sessionId);
}

public record InsertSessionWordDetails(
    string WordToMemorize,
    int GroupId,
    int Red,
    int Green);
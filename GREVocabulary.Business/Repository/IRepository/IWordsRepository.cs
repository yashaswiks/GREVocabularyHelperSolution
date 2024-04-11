namespace GREVocabulary.Business.Repository.IRepository;

public interface IWordsRepository
{
    Task<int?> InsertAsync(InsertWord insertWord);

    Task<List<int>> GetAllGroupIds();

    Task<List<WordDetailsModel>> GetAllAsync(int startingGroupingId, int endingGroupId);

    Task<List<string>> GetWordsBelongingToGroupIdAsync(int groupId);
}

public record InsertWord(
    int GroupId,
    string WordToMemorize);
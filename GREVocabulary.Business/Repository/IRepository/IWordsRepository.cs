namespace GREVocabulary.Business.Repository.IRepository;

public interface IWordsRepository
{
    Task<int?> InsertAsync(InsertWord insertWord);
}

public record InsertWord(
    int GroupId,
    string WordToMemorize);

namespace GREVocabulary.Business.Repository.IRepository;

public interface IWordMeaningRepository
{
    Task<List<string>> GetMeaningsAsync(string word);

    Task<int?> InsertAsync(string word);
}
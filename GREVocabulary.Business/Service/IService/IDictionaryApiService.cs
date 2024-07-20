using GREVocabulary.Business.Models;

namespace GREVocabulary.Business.Service.IService;

public interface IDictionaryApiService
{
    Task<List<string>> GetApiOutputAsync(string word);
}
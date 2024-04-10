using GREVocabulary.Business.CsvRelatedModules;

namespace GREVocabulary.Business.Service.IService;

public interface IReadCsvService
{
    Task<List<WordsModel>> ReadCsvAsync(string filePath);
}

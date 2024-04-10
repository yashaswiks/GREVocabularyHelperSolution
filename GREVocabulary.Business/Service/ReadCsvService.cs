using CsvHelper;
using GREVocabulary.Business.CsvRelatedModules;
using GREVocabulary.Business.Service.IService;
using System.Globalization;

namespace GREVocabulary.Business.Service;

public class ReadCsvService : IReadCsvService
{
    public async Task<List<WordsModel>> ReadCsvAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)) return null;

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader,
            CultureInfo.InvariantCulture);

        var records = csv.GetRecords<WordsModel>();

        return records?.ToList();
    }
}
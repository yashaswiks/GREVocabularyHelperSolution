using GREVocabulary.Business.Repository.IRepository;
using GREVocabulary.Business.Service.IService;
using Microsoft.AspNetCore.Components;

namespace GREVocabulary.WebApp.Components.Pages;

public partial class ImportVocabulary
{
    [Inject]
    private IReadCsvService _readCsvService { get; set; }

    [Inject]
    private IConfiguration _config { get; set; }

    [Inject]
    private IWordsRepository _wordsRepository { get; set; }

    private int TotalRowsInserted { get; set; } = 0;

    private async Task ImportFromCsvFileAsync()
    {
        var csvFilePath = _config.GetValue<string>("VocabularyFilePath");

        var records = await _readCsvService.ReadCsvAsync(csvFilePath);

        if (!records.Any()) return;

        foreach (var record in records)
        {
            if (record.GroupId is 0 || string.IsNullOrWhiteSpace(record.WordToMemorize)) continue;

            var model = new InsertWord(record.GroupId, record.WordToMemorize);

            var rowsAffected = await _wordsRepository.InsertAsync(model);

            if (rowsAffected is not null && rowsAffected > 0)
            {
                TotalRowsInserted += rowsAffected.Value;
            }
        }
    }
}
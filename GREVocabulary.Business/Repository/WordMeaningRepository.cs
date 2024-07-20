using Dapper;
using GREVocabulary.Business.EfCoreModels;
using GREVocabulary.Business.Repository.IRepository;
using GREVocabulary.Business.Service.IService;
using Microsoft.Data.Sqlite;
using System.Data;

namespace GREVocabulary.Business.Repository;

public class WordMeaningRepository : IWordMeaningRepository
{
    private readonly IDatabaseOptions _databaseOptions;
    private readonly IDictionaryApiService _dictionaryApiService;

    public WordMeaningRepository(
        IDatabaseOptions databaseOptions,
        IDictionaryApiService dictionaryApiService)
    {
        _databaseOptions = databaseOptions;
        _dictionaryApiService = dictionaryApiService;
    }

    public async Task<List<string>> GetMeaningsAsync(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return null;

        using IDbConnection _db = new SqliteConnection(_databaseOptions.ConnectionString);
        _db.Open();

        var param = new { WordToMemorize = word };
        var sql = @"SELECT * FROM WordMeaning w WHERE w.WordToMemorize = @WordToMemorize; ";
        var result = await _db.QueryAsync<WordMeaning>(sql, param);

        if (result is null || result.Count() is 0) return null;

        var output = new List<string>();

        foreach (var entry in result)
        {
            if(string.IsNullOrWhiteSpace(entry?.Meaning)) continue;
            output.Add(entry.Meaning);
        }

        return output;
    }

    public async Task<int?> InsertAsync(string word)
    {
        var meanings = await _dictionaryApiService.GetApiOutputAsync(word);
        if (meanings is null || meanings.Count is 0) return null;

        if (string.IsNullOrWhiteSpace(word) || meanings is null || meanings.Count is 0)
        {
            return null;
        }

        var affectedRows = 0;

        foreach (var meaning in meanings)
        {
            using IDbConnection _db = new SqliteConnection(_databaseOptions.ConnectionString);
            _db.Open();

            var param = new { WordToMemorize = word, Meaning = meaning };
            var sql = @"INSERT INTO WordMeaning (WordToMemorize, Meaning) VALUES (@WordToMemorize, @Meaning);";

            var rowsAffected = await _db.ExecuteAsync(sql, param);
            affectedRows += rowsAffected;
        }

        return affectedRows;
    }
}
using Dapper;
using GREVocabulary.Business.Repository.IRepository;
using GREVocabulary.Business.Service.IService;
using Microsoft.Data.Sqlite;
using System.Data;

namespace GREVocabulary.Business.Repository;

public class WordsRepository : IWordsRepository
{
    private readonly IDatabaseOptions _databaseOptions;

    public WordsRepository(IDatabaseOptions databaseOptions)
    {
        _databaseOptions = databaseOptions;
    }

    public async Task<int?> InsertAsync(InsertWord insertWord)
    {
        if (string.IsNullOrWhiteSpace(insertWord.WordToMemorize)
            || insertWord.GroupId is 0) return null;

        using IDbConnection _db = new SqliteConnection(_databaseOptions.ConnectionString);
        _db.Open();

        var sql = "INSERT INTO Words (GroupId, WordToMemorize) VALUES (@GroupId, @WordToMemorize);";
        var result = await _db.ExecuteAsync(sql, insertWord);

        return result;
    }
}
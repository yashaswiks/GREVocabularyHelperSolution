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

    public async Task<List<WordDetailsModel>> GetAllAsync(int startingGroupingId, int endingGroupId)
    {

        if (startingGroupingId is 0 || endingGroupId is 0) return null;
        if (startingGroupingId > endingGroupId) return null;


        var wordDetails = new List<WordDetailsModel>();

        var allGroupIds = new List<int>();

        for (var i = startingGroupingId; i <= endingGroupId; i++)
        {
            allGroupIds.Add(i);
        }

        if (allGroupIds.Count is 0) return null;

        foreach (var groupId in allGroupIds)
        {
            var words = await GetWordsBelongingToGroupIdAsync(groupId);

            if (words is null || words.Count is 0) continue;

            wordDetails.Add(new WordDetailsModel
            {
                GroupId = groupId,
                Words = words
            });
        }

        return wordDetails;
    }

    public async Task<List<int>> GetAllGroupIds()
    {
        using IDbConnection _db = new SqliteConnection(_databaseOptions.ConnectionString);
        _db.Open();

        var sql = "SELECT DISTINCT w.GroupId FROM Words w;";
        var result = await _db.QueryAsync<int>(sql);

        _db.Close();

        return result?.ToList();
    }

    public async Task<List<string>> GetWordsBelongingToGroupIdAsync(int groupId)
    {
        if (groupId is 0) return null;

        using IDbConnection _db = new SqliteConnection(_databaseOptions.ConnectionString);
        _db.Open();

        var sql = "SELECT w.WordToMemorize FROM Words w WHERE w.GroupId = @GroupId;";
        var result = await _db.QueryAsync<string>(sql, new { GroupId = groupId });
        _db.Close();

        return result?.ToList();
    }

    public async Task<int?> InsertAsync(InsertWord insertWord)
    {
        if (string.IsNullOrWhiteSpace(insertWord.WordToMemorize)
            || insertWord.GroupId is 0) return null;

        using IDbConnection _db = new SqliteConnection(_databaseOptions.ConnectionString);
        _db.Open();

        var sql = "INSERT INTO Words (GroupId, WordToMemorize) VALUES (@GroupId, @WordToMemorize);";
        var result = await _db.ExecuteAsync(sql, insertWord);

        _db.Close();

        return result;
    }
}
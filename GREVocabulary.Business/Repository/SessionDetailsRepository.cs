using Dapper;
using GREVocabulary.Business.Repository.IRepository;
using GREVocabulary.Business.Service.IService;
using Microsoft.Data.Sqlite;
using System.Data;

namespace GREVocabulary.Business.Repository;

public class SessionDetailsRepository : ISessionDetailsRepository
{
    private readonly IDatabaseOptions _databaseOptions;

    public SessionDetailsRepository(IDatabaseOptions databaseOptions)
    {
        _databaseOptions = databaseOptions;
    }

    public async Task<int?> InsertSessionWordAsync(int spacedRepetitionSessionId,
        List<InsertSessionWordDetails> insertSessionWordDetails)
    {
        if (spacedRepetitionSessionId is 0
            || insertSessionWordDetails is null
            || insertSessionWordDetails.Count is 0) return null;

        using IDbConnection _db = new SqliteConnection(_databaseOptions.ConnectionString);

        var sql = @"INSERT INTO SessionDetails (GroupId, WordToMemorize, Red, Green, SpacedRepetitionSessionId)
                        VALUES (@GroupId, @WordToMemorize, @Red, @Green, @SpacedRepetitionSessionId);";

        int totalaffectedRows = 0;

        foreach (var word in insertSessionWordDetails)
        {
            if (string.IsNullOrWhiteSpace(word.WordToMemorize)) continue;

            _db.Open();

            var param = new
            {
                word.GroupId,
                word.WordToMemorize,
                word.Red,
                word.Green,
                SpacedRepetitionSessionId = spacedRepetitionSessionId
            };

            var affectedRows = await _db.ExecuteAsync(sql, param);

            _db.Close();

            totalaffectedRows += affectedRows;
        }

        return totalaffectedRows;
    }
}
using Dapper;
using GREVocabulary.Business.Models;
using GREVocabulary.Business.Repository.IRepository;
using GREVocabulary.Business.Service.IService;
using Microsoft.Data.Sqlite;
using System.Data;

namespace GREVocabulary.Business.Repository;

public class SpacedRepetitionSessionsRepository : ISpacedRepetitionSessionsRepository
{
    private readonly IDatabaseOptions _databaseOptions;

    public SpacedRepetitionSessionsRepository(IDatabaseOptions databaseOptions)
    {
        _databaseOptions = databaseOptions;
    }

    public async Task<int?> CreateSessionAsync(InsertSessionDetailsDto sessionDetails)
    {
        using IDbConnection _db = new SqliteConnection(_databaseOptions.ConnectionString);
        _db.Open();

        var param = new
        {
            Red = sessionDetails.RedCount,
            Green = sessionDetails.GreenCount,
            sessionDetails.SessionTimestamp,
            Total = sessionDetails.TotalCount
        };

        var sql = @"INSERT INTO SpacedRepetitionSessions (Red, Green, SessionTimestamp, Total) 
                        VALUES (@Red, @Green, @SessionTimestamp, @Total);
                        SELECT last_insert_rowid();";

        var newSessionId = await _db.ExecuteScalarAsync<int?>(sql, param);
        _db.Close();
        return newSessionId;
    }

    public async Task<List<SpacedRepetitionSessionsModel>> GetAllAsync()
    {
        using IDbConnection _db = new SqliteConnection(_databaseOptions.ConnectionString);
        _db.Open();

        var sql = "SELECT * FROM SpacedRepetitionSessions";
        var sessions = await _db.QueryAsync<SpacedRepetitionSessionsModel>(sql);
        _db.Close();
        return sessions?.ToList();
    }
}
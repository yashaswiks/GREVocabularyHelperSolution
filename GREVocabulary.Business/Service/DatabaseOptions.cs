using GREVocabulary.Business.Service.IService;
using Microsoft.Extensions.Configuration;

namespace GREVocabulary.Business.Service;

public class DatabaseOptions : IDatabaseOptions
{
    private readonly IConfiguration _configuration;
    public string ConnectionString { get; set; }

    public DatabaseOptions(IConfiguration configuration,
        string connectionId = "DefaultConnection")
    {
        _configuration = configuration;
        ConnectionString = _configuration.GetConnectionString(connectionId);
    }
}

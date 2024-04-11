using GREVocabulary.Business.EfCoreModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GREVocabulary.Business;

public class VocabularyHelperDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<Word> Words { get; set; }
    public DbSet<SpacedRepetitionSession> SpacedRepetitionSessions { get; set; }
    public DbSet<SessionDetail> SessionDetails { get; set; }

    public VocabularyHelperDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
    }
}
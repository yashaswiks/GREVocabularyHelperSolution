namespace GREVocabulary.Business.EfCoreModels;

public class SpacedRepetitionSession
{
    public int Id { get; set; }
    public int Red { get; set; }
    public int Green { get; set; }
    public int Total { get; set; }
    public DateTime SessionTimestamp { get; set; }
    public ICollection<SessionDetail> SessionDetails { get; } = new List<SessionDetail>();
}
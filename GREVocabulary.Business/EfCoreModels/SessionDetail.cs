using System.ComponentModel.DataAnnotations.Schema;

namespace GREVocabulary.Business.EfCoreModels;

public class SessionDetail
{
    public int Id { get; set; }
    public int GroupId { get; set; }

    [Column(TypeName = "nvarchar(350)")]
    public string WordToMemorize { get; set; }

    public bool Red { get; set; }
    public bool Green { get; set; }
    public int SpacedRepetitionSessionId { get; set; }
    public SpacedRepetitionSession SpacedRepetitionSession { get; set; } = null!;
}
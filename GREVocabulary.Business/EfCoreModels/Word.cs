using System.ComponentModel.DataAnnotations.Schema;

namespace GREVocabulary.Business.EfCoreModels;

public class Word
{
    public int Id { get; set; }
    public int GroupId { get; set; }

    [Column(TypeName = "nvarchar(350)")]
    public string WordToMemorize { get; set; }
}
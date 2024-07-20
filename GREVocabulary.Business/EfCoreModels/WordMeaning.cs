using System.ComponentModel.DataAnnotations.Schema;

namespace GREVocabulary.Business.EfCoreModels;

public class WordMeaning
{
    [Column(TypeName = "nvarchar(350)")]
    public string WordToMemorize { get; set; }
    
    [Column(TypeName = "nvarchar(MAX)")]
    public string Meaning { get; set; }
}
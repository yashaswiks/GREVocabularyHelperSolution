namespace GREVocabulary.Business.Models;

public class SessionDetailsModel
{
    public int Id { get; set; }
    public int GroupId { get; set; }
    public string WordToMemorize { get; set; }
    public int Red { get; set; }
    public int Green { get; set; }
    public int SpacedRepetitionSessionId { get; set; }
}
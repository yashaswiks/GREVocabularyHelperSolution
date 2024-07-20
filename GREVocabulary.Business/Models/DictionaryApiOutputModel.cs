namespace GREVocabulary.Business.Models;

public class DictionaryApiOutputModel
{
    public string word { get; set; }
    public string phonetic { get; set; }
    public Phonetic[] phonetics { get; set; }
    public string origin { get; set; }
    public Meaning[] meanings { get; set; }
}

public class Phonetic
{
    public string text { get; set; }
    public string audio { get; set; }
}

public class Meaning
{
    public string partOfSpeech { get; set; }
    public Definition[] definitions { get; set; }
}

public class Definition
{
    public string definition { get; set; }
    public string example { get; set; }
    public object[] synonyms { get; set; }
    public object[] antonyms { get; set; }
}
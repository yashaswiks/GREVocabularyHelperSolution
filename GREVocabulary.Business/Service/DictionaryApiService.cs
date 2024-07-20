using GREVocabulary.Business.Models;
using GREVocabulary.Business.Service.IService;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace GREVocabulary.Business.Service;

public class DictionaryApiService : IDictionaryApiService
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public DictionaryApiService(IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<string>> GetApiOutputAsync(string word)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(word)) return null;

            var definitions = new List<string>();

            var requestUri = $"{_configuration.GetSection("DictionaryApi:Uri").Value}/v2/entries/en/{word}";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await httpClient.SendAsync(request);

            var jsonString = await response.Content.ReadAsStringAsync();
            var output = JsonSerializer.Deserialize<DictionaryApiOutputModel[]>(jsonString);

            if (output is null || output.Count() is 0) return null;

            foreach (var entry in output)
            {
                if (entry?.meanings is null || entry?.meanings.Count() is 0) continue;
                var meanings = entry?.meanings;

                if (meanings is null || meanings.Count() is 0) continue;

                foreach (var entry2 in meanings)
                {
                    if (entry2?.definitions is null || entry2?.definitions.Count() is 0) continue;
                    var d = entry2?.definitions;

                    if (d is null || d.Count() is 0) continue;

                    foreach (var entry3 in d)
                    {
                        if (string.IsNullOrWhiteSpace(entry3?.definition)) continue;
                        definitions.Add(entry3.definition);
                    }
                }
            }

            return definitions;
        }
        catch
        {
            return null;
        }
    }
}
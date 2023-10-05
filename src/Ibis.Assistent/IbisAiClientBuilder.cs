using Azure;
using Azure.AI.OpenAI;

namespace Ibis.Assistent;

public class IbisAiClientBuilder
{
    private string _aoaiApiKey;
    private string _aoaiApiUrl;

    public IbisAiClientBuilder(string aoaiApiKey, string aoaiApiUrl)
    {
        _aoaiApiKey = aoaiApiKey;
        _aoaiApiUrl = aoaiApiUrl;
    }

    public OpenAIClient InitializeClient()
    => new OpenAIClient(new Uri(_aoaiApiUrl), new AzureKeyCredential(_aoaiApiKey));

}
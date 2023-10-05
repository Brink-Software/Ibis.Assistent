using Azure;
using Azure.AI.OpenAI;

namespace Ibis.Assistent;

public class IbisAiClientBuilder
{
    private string _aoaiApiKey;
    private string _aoaiApiUrl;
    private string _searchEndpoint;
    private string _searchKey;
    private const string _searchIndexName = "leerberg-1-index";

    public IbisAiClientBuilder(string aoaiApiKey, string aoaiApiUrl, string searchEndpoint, string searchKey)
    {
        _aoaiApiKey = aoaiApiKey;
        _aoaiApiUrl = aoaiApiUrl;
        _searchEndpoint = searchEndpoint;
        _searchKey = searchKey;
    }

    public OpenAIClient InitializeClient()
    => new OpenAIClient(new Uri(_aoaiApiUrl), new AzureKeyCredential(_aoaiApiKey));
    
    public ChatCompletionsOptions GetChatCompletionOptions()
    => new ChatCompletionsOptions()
    {
        Messages =
        {
            new ChatMessage(
                ChatRole.System,
                "You are a helpful assistant that answers questions about the Contoso product database."),
            new ChatMessage(ChatRole.User, "What are the best-selling Contoso products this month?")
        },
        // The addition of AzureChatExtensionsOptions enables the use of Azure OpenAI capabilities that add to
        // the behavior of Chat Completions, here the "using your own data" feature to supplement the context
        // with information from an Azure Cognitive Search resource with documents that have been indexed.
        AzureExtensionsOptions = new AzureChatExtensionsOptions()
        {
            Extensions =
            {
                new AzureCognitiveSearchChatExtensionConfiguration()
                {
                    SearchEndpoint = new Uri(_searchEndpoint),
                    IndexName = _searchIndexName,
                    SearchKey = new AzureKeyCredential(_searchKey),
                }
            }
        }
    };
}
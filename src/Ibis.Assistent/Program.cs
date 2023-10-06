using Azure;
using Azure.AI.OpenAI;
using Ibis.Assistent;
using Microsoft.Extensions.Configuration;

IConfigurationBuilder builder = new ConfigurationBuilder()
    .AddUserSecrets<Program>();
IConfigurationRoot? configuration = builder.Build();

if (configuration["AOAI_API_KEY"] == null || configuration["AOAI_API_URL"] == null)
{
    Console.WriteLine("Please provide AOAI_API_KEY and AOAI_API_URL in your user secrets.");
    return;
}

if (configuration["CSI_API_KEY"] == null || configuration["CSI_API_URL"] == null)
{
    Console.WriteLine("Please provide CSI_API_KEY and CSI_API_URL in your user secrets.");
    return;
}

int maxInputLength = 50;
IbisAiClientBuilder clientBuilder =
    new IbisAiClientBuilder(configuration["AOAI_API_KEY"]!, configuration["AOAI_API_URL"]!,
        configuration["CSI_API_URL"]!, configuration["CSI_API_KEY"]!);
OpenAIClient client = clientBuilder.InitializeClient();

// Display options to user
string[] options = {
    "test-gpt-deployment",
};

Console.WriteLine("Please select an option:");
foreach (string option in options)
{
    Console.WriteLine(option);
}

string userChoice;

while (true)
{
    string? input = Console.ReadLine();
    if (int.TryParse(input, out int selected) && selected > 0 && selected <= options.Length)
    {
        userChoice = options[selected - 1];
        Console.WriteLine($"You selected: {userChoice}");
        break;
    }
    else
    {
        Console.WriteLine("Invalid choice. Please select a valid option.");
    }
}

if (args.Length > 0 && int.TryParse(args[0], out int parsedLength))
{
    maxInputLength = parsedLength;
}

while (true)
{
    Utils.PrintWithColor("Prompt: ", ConsoleColor.Blue);
    Utils.PrintWithColor("-----------------------------------", ConsoleColor.Blue);
    string? userInput = Console.ReadLine();
    if (string.IsNullOrEmpty(userInput))
    {
        Utils.PrintWithColor("Input is empty. Please provide some input.", ConsoleColor.Yellow);
        continue;
    }

    if (userInput.Length > maxInputLength)
    {
        Utils.PrintWithColor(
            $"Input is too long. Please enter input less than or equal to {maxInputLength} characters.",
            ConsoleColor.Yellow);
        continue;
    }

    Utils.PrintWithColor("-----------------------------------", ConsoleColor.Blue);
    // If input is valid1
    Response<ChatCompletions> completionsResponse = await client.GetChatCompletionsAsync(userChoice,
        clientBuilder.GetChatCompletionOptions(
            "You are a helpful assistant that answers questions about the Ibis Product line.", userInput));
    Utils.PrintWithColor("\nResponse from OpenAI:", ConsoleColor.Green);
    Utils.PrintWithColor("-----------------------------------", ConsoleColor.Green);
    foreach (ChatChoice? completion in completionsResponse.Value.Choices)
    {
        Utils.PrintWithColor(completion.Message.Content, ConsoleColor.Green);
    }
    Utils.PrintWithColor("-----------------------------------", ConsoleColor.Green);
    userInput = string.Empty;
}
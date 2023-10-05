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

int maxInputLength = 50;
IbisAiClientBuilder clientBuilder =
    new IbisAiClientBuilder(configuration["AOAI_API_KEY"]!, configuration["AOAI_API_URL"]!);
OpenAIClient client = clientBuilder.InitializeClient();

// Display options to user
string[] options = new string[]
{
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
    Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
    Console.WriteLine("Prompt: ");
    Console.WriteLine("-----------------------------------");
    string? userInput = Console.ReadLine();
    if (string.IsNullOrEmpty(userInput))
    {
        Console.WriteLine("Input is empty. Please provide some input.");
        continue;
    }

    if (userInput.Length > maxInputLength)
    {
        Console.WriteLine($"Input is too long. Please enter input less than or equal to {maxInputLength} characters.");
        continue;
    }

    Console.WriteLine("-----------------------------------");
    // If input is valid
    Response<Completions> completionsResponse = await client.GetCompletionsAsync(userChoice, userInput);
    Console.WriteLine("\nResponse from OpenAI:");
    Console.WriteLine("-----------------------------------");
    foreach (Choice? completion in completionsResponse.Value.Choices)
    {
        Console.WriteLine(completion.Text);
    }

    Console.WriteLine("-----------------------------------");
    userInput = string.Empty;
}
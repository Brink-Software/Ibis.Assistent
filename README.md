# 🗨️Azure OpenAI Console Application

This console application provides an interface to work with Azure OpenAI. Leveraging Azure Blob Storage, it stores various documents pertinent to Ibis Helpdesk questions. Azure Search Service then indexes these blob files, and Azure OpenAI is used to train on this data, facilitating optimized responses based on the Ibis Helpdesk dataset.

## Table of Contents

- [Setup](#setup)
- [Configuration](#configuration)
- [Usage](#usage)
- [Features](#features)
- [Contribution](#contribution)
- [Support](#support)
- [License](#license)

## Setup

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Brink-Software/Ibis.Assistent.git
   cd Ibis.Assistent
   ```
2. **Install the required .NET SDK** (if not already installed).
3. **Build the solution:**
   ```bash
   dotnet build
   ```

## Configuration

Before you run the application, ensure you have set up the necessary Azure services and have the required credentials at hand:

1. **Azure Blob Storage**: Create a container and note down the connection string.
2. **Azure Search**: Set up a Search Service, create an indexer, data source, and index to work with the blobs.
3. **Azure OpenAI**: Ensure you have an Azure OpenAI instance ready and have the endpoint and key available.

Configure the application's `appsettings.json` (or use user secrets) with the necessary credentials:

```json
{
  "AOAI_API_URL": "YOUR_AZURE_OPENAI_ENDPOINT",
  "AOAI_API_KEY": "YOUR_AZURE_OPENAI_KEY"
}
```

## Usage

To run the application, execute the following command:

```bash
dotnet run
```

## Features

1. Blob Storage Integration: Store and retrieve documents related to Ibis Helpdesk questions.
2. Azure Search Integration: Index the stored blob files for efficient querying.
3. Azure OpenAI Integration: Train on the indexed data and retrieve optimized responses for the given queries.

## Contribution

Feel free to fork the repository and submit pull requests. For major changes, open an issue first to discuss the proposed changes.

## Support

For support please contact contributors or code owners.

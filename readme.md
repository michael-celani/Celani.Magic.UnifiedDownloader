## Introduction

The Unified Downloader is a tool designed to streamline the process of downloading and managing Magic: The Gathering card data from various sources. This solution provides a unified interface to interact with APIs from Scryfall, Moxfield, and Archidekt, allowing users to easily fetch and store card information. It also contains in-development recommender systems that should probably be put in another repository.

Key features include:
- Integration with multiple Magic: The Gathering data sources.
- Easy setup and configuration through `appsettings.json`.
- Database management with Entity Framework migrations.

## Getting Started

To get started with the Celani Magic Unified Downloader, follow these steps:

### Prerequisites

Ensure that you have the following prerequisites:

- .NET 9.0 SDK installed. You can download it from the [official .NET website](https://dotnet.microsoft.com/download/dotnet/9.0).

### Installation

1. Clone the repository:
    ```sh
    git clone https://github.com/michael-celani/Celani.Magic.UnifiedDownloader.git
    ```
2. Navigate to the project directory:
    ```sh
    cd Celani.Magic.UnifiedDownloader
    ```
3. Set up path environment variables in `appsettings.json` in `Celani.Magic.Ingestion.Console`.

4. Run database migrations:
    ```sh
    dotnet ef database update --project Celani.Magic.Downloader.Storage --startup-project Celani.Magic.Ingestion.Console
    ```

5. Run Celani.Magic.UnifiedDownloader


### Usage

Navigate to one of `Celani.Magic.UnifiedDownloader`, `Celani.Magic.Recommender.Web`, or `Celani.Magic.Ingestion.Console`.

```sh
dotnet run
```

### Configuration

```json
  "Storage": {
    "DbPath": "", # Path to local database.
    "Models": {
      "RecommendationPath": "" # Path to write recommendation model to.
    }
  },
  "Backend": {
    "Scryfall": {
      "ScryfallApiUri": "https://api.scryfall.com" # URL for Scryfall API
    },
    "Moxfield": {
      "MoxfieldApiUri": "https://api2.moxfield.com", # URL for Moxfield API
      "MoxfieldApiKey": "" # Moxfield API Key
    },
    "Archidekt": {
      "ArchidektApiUri": "https://archidekt.com/api" # URL for Archidekt API
    }
  }
```

### Troubleshooting

If you encounter any problems, please open an issue on the [GitHub repository](https://github.com/michael-celani/Celani.Magic.UnifiedDownloader.git).

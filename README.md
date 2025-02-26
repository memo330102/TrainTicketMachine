# TRAIN TICKET MACHINE
# Overview
The Train Station Search API is a high-performance .NET 8 Web API that provides station search functionality. 
It follows SOLID principles, uses caching for optimized speed, and supports multiple station data providers.
# Features
+ Supports multiple station data providers (JSON, XML, API, Database)
+ In-memory caching for fast search results
+ Background service updates station data every 30 minutes
+ Global exception handling for stability
+ Structured logging with Serilog
+ Docker support 
+ API versioning for future upgrades

# Project Architecture
![https://github.com/memo330102/TrainTicketMachine/blob/master/ProjectArchitecture.jpeg](https://github.com/memo330102/TrainTicketMachine/blob/master/ProjectArchitecture.jpeg)
## Provider System (Open-Closed Principle)
The provider uses the Open-Closed Principle (OCP) to support multiple station data providers. Easy to add new provider:
+ JsonStationProvider → Fetches data from JSON
+ XmlStationProvider → Fetches data from XML
+ ApiStationProvider → Retrieves data from an external API
+ DatabaseStationProvider → Fetches data from a database
## Infrastructure Layer
+ Aggregates stations from all providers and stores them uniquely in memory
+ Uses HashSet to prevent duplicates
+ Reduces unnecessary API/database calls
## Background Service Layer
+ Loads data into memory at startup.
+ Runs as a background service to periodically refresh the cache without impacting runtime speed.
+ Calls the repository, fetches updated station data, and stores it in In-Memory Cache.
+ Uses IServiceProvider to resolve IStationRepository and trigger cache refreshes.
## Caching Layer
+ First, the API checks if data is available in cache.
+ If data exists in cache, it is retrieved and filtered.
+ If data is missing, it fetches from the providers and stores it in cache.
  
# Setup Instructions
## Prerequisites
Ensure you have the following installed:
+ .NET 8 SDK
+ Docker
## Installation
Clone the repository:
 ```
git clone https://github.com/memo330102/TrainTicketMachine.git
cd TrainStationSearchAPI
 ```
## Running the API
### Option 1: Run Locally
 ```
dotnet build
dotnet run
 ```
### Option 1: Run Locally
Option 2: Run with Docker Compose
 ```
docker-compose up -d
 ```
## API Endpoints
#### Endpoint	Method	Description
/api/stations/search?query={query}	GET	Search for train stations
#### Example Request:
 ```
GET /api/stations/search?query=Dart
 ```
#### Example Response:
 ```
{
  "stationNames": [
    "Dartford",
    "Darton"
  ],
  "nextCharacters": [
    "f",
    "o"
  ]
}
 ```
## Technology Stack
+ .NET 8 (ASP.NET Core Web API) :	Core framework for building the API
+ C# :	Programming language
+ In-Memory Cache (IMemoryCache) :	Improves API performance
+ Serilog	: Structured logging
+ Docker :	Containerization 
+ Swagger (NSwag) : 	API documentation

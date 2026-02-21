REST API Test Automation Framework (C# + xUnit + RestSharp)

Overview

This project is an API Test Automation Framework built using C#, .NET 8, xUnit, and RestSharp.
It automates testing of the public REST API:

https://api.restful-api.dev/

The framework validates core CRUD operations and verifies API responses using assertions.

Technologies Used

C# (.NET 8)
xUnit Test Framework
RestSharp (HTTP client)
Newtonsoft.Json (JSON parsing)
.NET CLI

Project Structure
TestCheck/
│
├── ApiFixture.cs         # Initializes RestClient and shared setup
├── RestfulApiTests.cs    # Contains API test cases
├── TestCheck.csproj      # Project configuration
└── README.md             # Documentation


Test Cases Implemented

The following test scenarios are implemented:

Get all objects (GET /objects)
Create a new object (POST /objects)
Get object by ID (GET /objects/{id})
Update object (PUT /objects/{id})
Delete object (DELETE /objects/{id})

Note: Some tests may be temporarily skipped due to public API rate limits.

Prerequisites

Ensure the following are installed:

.NET SDK 8.0 or later
Download: https://dotnet.microsoft.com/download

Verify installation:

dotnet --version

How to Run the Tests

Option 1: Run using Command Line (Recommended)

Step 1: Clone the repository
git clone <your-repository-url>
cd TestCheck
Step 2: Restore dependencies
dotnet restore
Step 3: Run all tests
dotnet test --logger "console;verbosity=detailed"
Step 4: Run a specific test (optional)

Example:

dotnet test --filter GetAllObjects_ShouldReturn200

Option 2: Run using Visual Studio Code (Optional)
Step 1: Open project in VS Code
code .
OR manually open the folder in VS Code.
Step 2: Restore dependencies
Open Terminal in VS Code and run:
dotnet restore
Step 3: Run tests
dotnet test

Option 3: Run using Visual Studio (Optional)

Open Visual Studio
Click File → Open → Project/Solution
Select:
TestCheck.csproj
Open Test Explorer
Click Run All Tests

Test Execution Output

Example output:

Passed GetAllObjects
Passed GetObjectById

Framework Design

This framework follows best practices:

Fixture pattern using IClassFixture
Separation of setup and test logic
Reusable RestClient instance
Clear assertions and logging
Parameterized test support

Notes

The public API has a daily rate limit. If the limit is exceeded, some POST/PUT/DELETE tests may fail or be skipped.

Author

Saman Chandana
API Test Automation Framework using C# and xUnit
# REST API Test Automation Framework (C# + xUnit)

## Overview

This project contains automated API tests for the public REST API:

https://api.restful-api.dev/

The tests are implemented using:

* C#
* xUnit testing framework
* RestSharp for HTTP requests
* Newtonsoft.Json for JSON parsing
* .NET 8

The framework validates the complete lifecycle of an object:

1. Create object (POST)
2. Get object by ID (GET)
3. Update object (PUT)
4. Get all objects (GET)
5. Delete object (DELETE)

---

## Project Structure

```
TestCheck
│
├── Fixtures
│   └── ApiFixture.cs
│
├── Helpers
│   ├── ObjectHelper.cs
│   ├── RandomDataHelper.cs
│   ├── PriorityAttribute.cs
│   └── PriorityOrderer.cs
│
├── Tests
│   └── RestfulApiTests.cs
│
└── TestCheck.csproj
```

### Fixtures

Contains shared setup logic such as RestClient initialization.

### Helpers

Contains reusable helper classes:

* ObjectHelper → API operations (Create, Get, Update, Delete)
* RandomDataHelper → generates dynamic test data
* PriorityOrderer → controls test execution order

### Tests

Contains test cases covering all required API scenarios.

---

## Test Scenarios Covered

### 1. Create Object

* Sends POST request
* Validates status code
* Validates response contains ID
* Validates response structure

### 2. Get Object by ID

* Sends GET request
* Validates correct object returned
* Validates response data integrity

### 3. Update Object

* Sends PUT request
* Validates update success
* Validates updated fields

### 4. Get All Objects

* Sends GET request
* Validates response is not empty
* Validates response structure

### 5. Delete Object

* Sends DELETE request
* Validates successful deletion
* Verifies object no longer exists

---

## Test Framework Design

This framework follows best practices:

* Separation of concerns
* Reusable helper classes
* Shared test fixture
* Random test data generation
* Strong assertions
* Clean and maintainable structure

---

## Prerequisites

Install:

* .NET 8 SDK
  Download: https://dotnet.microsoft.com/download

Verify installation:

```
dotnet --version
```

---

## How to Run the Tests

### Step 1: Clone repository

```
git clone https://github.com/<your-username>/TestCheck.git
cd TestCheck
```

---

### Step 2: Restore dependencies

```
dotnet restore
```

---

### Step 3: Run all tests

```
dotnet test --logger "console;verbosity=detailed"
```

---

### Step 4: Run specific test

Example:

```
dotnet test --filter CreateObject
```

---

## Sample Test Output

```
Passed: Create object and validate response
Passed: Get created object by ID
Passed: Update object and validate response
Passed: Get all objects and validate response structure
Passed: Delete object and validate response
```

---

## Tools and Libraries Used

| Tool            | Purpose         |
| --------------- | --------------- |
| xUnit           | Test framework  |
| RestSharp       | HTTP client     |
| Newtonsoft.Json | JSON parsing    |
| .NET 8          | Runtime         |
| GitHub          | Version control |

---

## Author

Saman Chandana

Test Automation Consultant

Skills:

• Programming Languages: Java, C#
• API Automation: RestAssured, RestSharp, Postman
• UI Automation: Selenium WebDriver, Playwright
• Test Frameworks: TestNG, xUnit
• Performance Testing: Apache JMeter
• Reporting: Allure Reports, ExtentReports
• Version Control: Git, GitHub
• Automation Framework Design: Page Object Model (POM), Hybrid Framework

---

## Notes

This project was created as part of a technical assessment to demonstrate API automation skills, framework design, and best practices.

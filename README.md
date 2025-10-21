# Actionators

A professional ASP.NET Core 9 MVC web application demonstrating modern web development practices with clean architecture, responsive design, and comprehensive testing.

## Features

- **Modern UI**: Responsive HTML5/CSS3 design with Bootstrap 5
- **Professional Navigation**: Clean navbar with branding and icons
- **Hero Section**: Eye-catching landing page with call-to-action buttons
- **Feature Showcase**: Six feature cards highlighting key capabilities
- **Contact Form**: Fully validated contact form with client and server-side validation
- **Repository Pattern**: Clean data access layer with dependency injection
- **Comprehensive Testing**: xUnit test project with 30+ tests covering controllers, repositories, and validation
- **Clean Architecture**: Separation of concerns with proper project structure

## Project Structure

```
Actionators/
├── Actionators.Web/              # Main web application
│   ├── Controllers/               # MVC Controllers
│   ├── Models/                    # Domain models
│   ├── Repositories/              # Data access layer
│   ├── Views/                     # Razor views
│   └── wwwroot/                   # Static files (CSS, JS)
├── Actionators.Tests/             # Test project
│   ├── HomeControllerTests.cs
│   ├── InMemoryContactMessageRepositoryTests.cs
│   └── ContactMessageValidationTests.cs
└── Actionators.sln                # Solution file
```

## Technologies Used

- **Framework**: ASP.NET Core 9 MVC
- **UI**: Bootstrap 5 with Bootstrap Icons
- **Testing**: xUnit, Moq, Microsoft.AspNetCore.Mvc.Testing
- **Language**: C# 13 with .NET 9
- **Architecture**: Repository Pattern, Dependency Injection
- **CI/CD**: Automated GitHub Actions workflow for testing, coverage, and builds

## Getting Started

### Prerequisites

- .NET 9 SDK or later
- Any modern IDE (Visual Studio 2022, VS Code, JetBrains Rider)

### Running the Application

1. Clone the repository:
   ```bash
   git clone https://github.com/rob-foulkrod/Actionators.git
   cd Actionators
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

4. Run the web application:
   ```bash
   cd Actionators.Web
   dotnet run
   ```

5. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

### Running Tests

```bash
dotnet test
```

All 30 tests should pass successfully.

### Running Tests with Code Coverage

To run tests with code coverage reporting:

```bash
# Run tests with coverage collection
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults

# Install ReportGenerator (one-time setup)
dotnet tool install --global dotnet-reportgenerator-globaltool

# Generate HTML coverage report
reportgenerator -reports:"./TestResults/*/coverage.cobertura.xml" -targetdir:"./CoverageReport" -reporttypes:"Html;TextSummary"

# View the summary
cat ./CoverageReport/Summary.txt

# Open the HTML report in a browser
# The report will be in ./CoverageReport/index.html
```

Current code coverage: **64%** (exceeds the minimum 30% requirement)

## Features in Detail

### Homepage
- Gradient hero section with welcome message
- Two prominent call-to-action buttons
- Six feature cards with hover effects
- Additional CTA section at bottom
- Modern footer with company information

### Contact Form
- Full name validation (2-100 characters)
- Email validation with proper format checking
- Subject validation (5-200 characters)
- Message validation (10-1000 characters)
- Success message display after submission
- Contact information display (address, phone, email)

### Architecture
- **Repository Pattern**: `IContactMessageRepository` with in-memory implementation
- **Dependency Injection**: Properly configured in `Program.cs`
- **Model Validation**: Data annotations with comprehensive validation rules
- **Separation of Concerns**: Controllers, models, repositories, and views properly separated

## Testing

The test suite includes:
- **Controller Tests**: 6 tests covering all controller actions
- **Repository Tests**: 8 tests covering CRUD operations
- **Validation Tests**: 16 tests covering all validation scenarios

## Continuous Integration

This project includes a comprehensive CI workflow using GitHub Actions that automatically:

### CI Workflow Features

The CI workflow (`.github/workflows/ci.yml`) runs on every push to the main branch and on all pull requests. It performs the following tasks:

1. **Build Verification**: Builds the solution in Release configuration
2. **Unit Tests**: Runs all 30 unit tests with detailed output
3. **Code Coverage**: Collects code coverage metrics using coverlet
4. **Coverage Validation**: Ensures code coverage meets the minimum 30% threshold (currently at 64%)
5. **Coverage Reports**: Generates HTML coverage reports as artifacts
6. **Test Results**: Uploads test results as artifacts for review

### CI Workflow Steps

```yaml
1. Checkout code
2. Setup .NET 9 SDK
3. Restore NuGet packages
4. Build solution (Release configuration)
5. Run unit tests with code coverage
6. Generate coverage reports
7. Validate coverage threshold (minimum 30%)
8. Upload coverage reports as artifacts
9. Upload test results as artifacts
```

### Viewing CI Results

- **Build Status**: Check the Actions tab in the GitHub repository
- **Coverage Reports**: Download from the workflow run artifacts
- **Test Results**: View in the workflow logs or download detailed results

### Coverage Threshold

The CI workflow enforces a **minimum 30% code coverage** requirement. If coverage falls below this threshold, the build will fail. The current coverage is **64%**, which exceeds this requirement.

## Screenshots

### Homepage
![Homepage](https://github.com/user-attachments/assets/c4465016-c59d-42d1-9195-285653b47d96)

### Contact Page
![Contact Page](https://github.com/user-attachments/assets/752934e9-21f4-4ddf-8c3b-22e9a700b6b3)

### Contact Form Success
![Contact Success](https://github.com/user-attachments/assets/a771b3b1-98b7-4c03-be1c-c2c00f38d959)

## License

This project is created for demonstration purposes.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
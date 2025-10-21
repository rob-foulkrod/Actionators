# CI Workflow Documentation

## Overview

This directory contains the GitHub Actions CI (Continuous Integration) workflow for the Actionators project. The workflow automatically validates code quality on every push to the main branch and on all pull requests.

## Workflow File: `ci.yml`

### Purpose

The CI workflow ensures code quality by:
- Building the project to catch compilation errors
- Running all unit tests to verify functionality
- Measuring code coverage to ensure adequate testing
- Validating that coverage meets the minimum threshold of 30%

### Triggers

The workflow runs automatically on:
- **Push events** to the `main` branch
- **Pull request events** targeting the `main` branch

### Environment Variables

- `DOTNET_VERSION`: Specifies the .NET SDK version (9.0.x)
- `COVERAGE_THRESHOLD`: Minimum required code coverage percentage (30%)

## Workflow Steps

### 1. Checkout Code
Uses `actions/checkout@v4` to clone the repository code to the runner.

### 2. Setup .NET
Installs the .NET 9 SDK using `actions/setup-dotnet@v4`.

### 3. Display .NET Version
Outputs the installed .NET version for verification and debugging.

### 4. Restore Dependencies
Runs `dotnet restore` to download all required NuGet packages.

### 5. Build Solution
Compiles the solution in Release configuration using `dotnet build`. The `--no-restore` flag skips restoration since it was done in step 4.

### 6. Run Unit Tests
Executes all tests using `dotnet test` with:
- Release configuration
- Code coverage collection via coverlet
- Results stored in `./TestResults` directory

### 7. Install ReportGenerator
Installs the `dotnet-reportgenerator-globaltool` to generate human-readable coverage reports.

### 8. Generate Coverage Report
Creates HTML and text summary reports from the collected coverage data.

### 9. Display Coverage Summary
Outputs the coverage summary to the workflow logs for quick visibility.

### 10. Validate Coverage Threshold
Checks if the current code coverage meets or exceeds the minimum threshold (30%). The build fails if coverage is below this threshold.

### 11. Upload Coverage Report
Uploads the HTML coverage report as a workflow artifact, available for download for 30 days.

### 12. Upload Test Results
Uploads the raw test results as a workflow artifact for detailed analysis.

## Artifacts

The workflow generates two types of artifacts:

### Coverage Report
- **Name**: `coverage-report`
- **Contents**: HTML coverage report and text summary
- **Retention**: 30 days
- **Use**: View detailed coverage metrics per file and method

### Test Results
- **Name**: `test-results`
- **Contents**: Raw test execution results
- **Retention**: 30 days
- **Use**: Detailed test failure analysis

## Coverage Requirements

The workflow enforces a **minimum 30% line coverage** requirement. This ensures:
- Critical code paths are tested
- New features include basic tests
- Code quality remains consistent

Current project coverage: **64%** (well above the minimum)

## Viewing Results

### In Pull Requests
- The workflow status appears as a check on the PR
- Click "Details" to view the full workflow run
- Download artifacts from the workflow summary page

### In the Actions Tab
- Navigate to the "Actions" tab in GitHub
- Select the "CI" workflow
- View recent runs and their status
- Download artifacts from successful runs

## Troubleshooting

### Build Fails
- Check the "Build solution" step for compilation errors
- Ensure all dependencies are properly restored

### Tests Fail
- Review the "Run unit tests" step output
- Download test results artifact for detailed failure information

### Coverage Below Threshold
- Review the coverage report artifact
- Identify untested code sections
- Add tests for critical paths
- Consider if the threshold needs adjustment

### Workflow Doesn't Trigger
- Verify the trigger conditions (branch name, event type)
- Check repository Actions settings
- Ensure workflow file is in `.github/workflows/` directory

## Maintenance

### Updating .NET Version
Change the `DOTNET_VERSION` environment variable in the workflow file.

### Adjusting Coverage Threshold
Modify the `COVERAGE_THRESHOLD` environment variable. Be careful not to set it too low as this reduces code quality assurance.

### Adding More Steps
Insert new steps in the `steps` section. Consider:
- Linting (code style checks)
- Security scanning
- Performance tests
- Integration tests

## Best Practices

1. **Keep builds fast**: Current workflow completes in ~2-3 minutes
2. **Use caching**: Consider adding NuGet package caching to speed up restore
3. **Monitor coverage trends**: Track coverage over time, not just meeting minimums
4. **Review artifacts**: Regularly check coverage reports to find improvement areas
5. **Update dependencies**: Keep GitHub Actions up to date

## Related Documentation

- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [.NET CLI Documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/)
- [ReportGenerator Documentation](https://github.com/danielpalme/ReportGenerator)
- [Coverlet Documentation](https://github.com/coverlet-coverage/coverlet)

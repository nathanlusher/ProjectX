# Overview

This solution is for a Web API created in Visual Studio 2019 using .NET 5.
To maintain anonymity, 'CompanyX' is used as the company name and 'ProjectX' as the project name.

The time spent on implementation was approximately as follows:

* Initial solution and project set-up: 1 hour
* Implementation of services and tests: 2 hours
* Implementation of controller and tests: 1 hour
* Refactoring and documentation: 1 hour

# Getting Started

To launch the Web API from Visual Studio: 

* Select CompanyX.ProjectX.WebApi as the startup project
* Run the CompanyX.ProjectX.WebApi project

The solution is configured to launch the Swagger UI by default which will provide visibility of the API endpoint and associated documentation.
It is also possible to manually test the Web API through the Swagger UI, using the 'Try it out' button.

The appsettings.json files have been intentionally included in the repository to allow for minimal set-up when assessing the work.

# Assumptions

The following assumptions have been made:

* The mock repository and mock bank objects would be substituted for a real data store and bank integration, respectively.
* The full details required for transferring funds from a merchant to a vendor are a best-guess and could be easily changed, depending on the requirements.

# Omissions

The following items were deemed out of scope for this exercise, but could be considered and prioritised if more time was available:

* Authentication
* Logging
* Cancellation
* Containerisation
* Additional error handling (for example if the bank or repository is not accessible)



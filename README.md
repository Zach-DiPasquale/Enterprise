# TERM-PROJECT: KennUWare

An ERP HR system developed in .NET Core. KennUware is "a smart wearables manufacturing company. The organization handles wearable development all the way from receiving the parts, manufacturing the wearable product, and finally selling them." This Silo handles just the HR aspect of KennUware Corp.

KennUwareHR manage all the employee data including personal information, salaries, leaves, reviews, and authentication for the rest of the ERP. Other silos can communicate with us through a set of REST APIs to access information about employees and salaries. 

## Silo:  
team_humanresources


## Team
- Zachary DiPasquale <zrd5307@rit.edu>
- Thomas Faticone <tjf4881@rit.edu>
- Ephraim Gbadebo <exg1977@rit.edu>
- Jarrod Cummings <jmc9961@rit.edu>


## Prerequisites

- .NET Core 2.0
- Visual Studios
- Docker

## How to begin (setup instructions)

#### Running in Development

###### With Visual Studios
1. Open Visual Studios and open erp-2175-epr-humanresources.sln to open the project.
2. Ensure that you are currently in debug mode.
3. Click the run button.

###### With command line
1. Go into /KennUwareHR Directory
2. Run `dotnet restore`
3. Run `dotnet run`

#### Running in Production
Make sure that all migrations have preformed (See instructions in the 'Running Migrations' section.)
###### With Docker
1. In root directory run `docker build -t TagName .`
2. Run `docker run -d=false -p 8080:80 --name NameContainer nameOfImage`

## Running Migrations
Our Databases our hosted on AWS. 

*You only need to run migrations if you have made changes to a model otherwise, everything should be already done.*

#### To make a new migration file:
1. Ensure that you added the new model to Contexts/HRContext.cs (if applicable)
2. In the /KennUwareHR directory run `dotnet ef migrations add DESCRIPTION` - For DESCRIPTION use a few words to describe what updates we made to the models. Ex. `dotnet ef migrations add AddAddressField`

#### Updating the database

Run the following commands
###### Development DB
- `dotnet ef database update`

###### Production DB
**macOS**
- `ASPNETCORE_ENVIRONMENT=Production dotnet ef database update`

**Windows**
***1. Either:***
- *Command*
  - `set ASPNETCORE_ENVIRONMENT=Production`

**OR**

- *PowerShell*
  - `$Env:ASPNETCORE_ENVIRONMENT = "Production"`

NOTE: These commands take effect only for the current window. When the window is closed, the ASPNETCORE_ENVIRONMENT setting reverts to the default setting or machine value. To use development again replace "Production" with "Development".

***2. Then***
- `dotnet ef database update`

## Testing

KennUwareHR is using XUnit as its unit testing framework.

To run the test use
`dotnet test`

or in VS switch to the testing solution by clicking on "KennUwareHR" next to the play button and 
then click "KennUwareHR.Test Unit" in the drop down. Then click the play button to run the tests.

We use postman for API tests. The postman file can be found in the root of the repo with the name KennUwareHR.postman_collection.json.

## User
Logging in as admin. 
### In Production
Username: root
Password: admin

## License
MIT License

See LICENSE for details.

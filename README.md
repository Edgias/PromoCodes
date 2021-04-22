# PromoCodes
API for managing promo codes.

## Running the API

After cloning or downloading the code you should be able to run the API using an In Memory database immediately. 

If you wish to use the API with a persistent database(SQL SERVER), you will need to run its Entity Framework Core migrations first, and update the `ConfigureServices` method in `Startup.cs` (see below).

### Configuring the sample to use SQL Server

1. Update `Startup.cs`'s `ConfigureDevelopmentServices` method as follows:

    ```csharp
    public void ConfigureDevelopmentServices(IServiceCollection services)
    {
        // use in-memory database
        //ConfigureTestingServices(services);

        // use real database
        ConfigureProductionServices(services);

    }
    ```

1. Ensure your connection strings in `appsettings.json` point to a local SQL Server instance.
1. Ensure the tool EF was already installed. You can find some help [here](https://docs.microsoft.com/ef/core/miscellaneous/cli/dotnet)

    ```
    dotnet tool install --global dotnet-ef
    ```

1. Open a command prompt in the API folder and execute the following commands:

    ```
    dotnet restore
    dotnet tool restore
    dotnet ef database update -c promocodesdbcontext -p ../Infrastructure/Infrastructure.csproj -s API.csproj
    dotnet ef database update -c promocodesidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s API.csproj
    ```

    These commands will create two separate databases, one for the services data, and one for the user credentials and identity data.

1. Run the application.

    The first time you run the application, it will seed the PromoCodesIdentityDbContext database with data such that you should be able to log in using the promocodes@therooom.com account and call the API.

    Note: If you need to create migrations, you can use these commands:

    ```
    -- create migration (from API folder CLI)
    dotnet ef migrations add InitialModel --context promocodesdbcontext -p ../Infrastructure/Infrastructure.csproj -s API.csproj -o Data/Migrations

    dotnet ef migrations add InitialIdentityModel --context promocodesidentitydbcontext -p ../Infrastructure/Infrastructure.csproj -s API.csproj -o Identity/Migrations
    ```

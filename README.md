If you want to run locally it's good to set up a SecretConnectionString (after `dotnet restore`)

```
dotnet user-secrets set SecretConnectionString "Server=.\;Database=AdventureWorksLT2012;Trusted_Connection=True;"
```

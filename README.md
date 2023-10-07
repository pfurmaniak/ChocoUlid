# ChocoUlid

<div align="center">
  <p>
    Ulid type support for HotChocolate
  </p>
  <p>
	  <a href="https://github.com/pfurmaniak/chocoulid/releases"><img alt="GitHub release" src="https://img.shields.io/github/release/pfurmaniak/chocoulid.svg"></a>
	  <a href="https://www.nuget.org/packages/ChocoUlid"><img alt="Nuget version" src="https://img.shields.io/nuget/v/ChocoUlid"></a>
	  <a href="https://www.nuget.org/packages/ChocoUlid"><img alt="NuGet downloads" src="https://img.shields.io/nuget/dt/ChocoUlid"></a> 
  </p>
</div>

## Getting started

Install the package
```bash
dotnet add package ChocoUlid
```

Add Ulid type to GraphQL
```csharp
services.AddGraphQL()
	.AddUlidType();
```

Done. Your Ulid properties will now be handled correctly. When filtering, equals and not equals operators are supported.
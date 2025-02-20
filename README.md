# 021 Many To Many Relationships

## Lecture

[![# Many To Many Relationships (Part 1)](https://img.youtube.com/vi/egmPuRlaNoo/0.jpg)](https://www.youtube.com/watch?v=LUOpye2AxVk)
[![# Many To Many Relationships (Part 2)](https://img.youtube.com/vi/PTPPW2rllRQ/0.jpg)](https://www.youtube.com/watch?v=PTPPW2rllRQ)

## Instructions

At the start of this lesson, you'll notice the `UtilityProvider` model and all associated code has been made blank. This is because you will be rebuilding that model as a new Many to Many relationship.

In `HomeEnergyApi/Models/UtilityProvider.cs`
- Create a public class `UtilityProvider` with the following property names / types
    - Id / `int`
    - Name / `string?`
    - ProvidedUtilities / `ICollection<string>`

In `HomeEnergyApi/Models/HomeUtilityProvider.cs`
- Create a public class `HomeUtilityProvider` with the following property names / types
    - Id / `int`
    - UtilityProviderId / `int`
    - UtilityProvider / `UtilityProvider`
        - With the `JsonIgnore` attribute
    - HomeId / `int`
    - Home / `Home`
        - With the `JsonIgnore` attribute

In `HomeEnergyApi/Models/Home.cs`
- Give `Home` the public property `HomeUtilityProviders` with the type `ICollection<HomeUtilityProvider>`

In `HomeEnergyApi/Models/UtilityProvider.cs`
- Give `UtilityProvider` the public property `HomeUtilityProviders` with the type `ICollection<HomeUtilityProvider>`

In `HomeEnergyApi/Models/HomeDbContext.cs`
- Give `HomeDbContext` the following property names / types
    - UtilityProviders / `DbSet<UtilityProvider>`
    - HomeUtilityProviders / `DbSet<HomeUtilityProvider>`

In `HomeEnergyApi/Controllers/UtilityProviderAdminController.cs`
- Create a class `UtilityProviderAdminController` implementing `ControllerBase`
    - Give `UtilityProviderAdminController` the following attributes
        - ApiController
        - Route("admin/UtilityProviders")
    - Give `UtilityProviderAdminController` the following private property names / types
        - repository / `IWriteRepository<int, UtilityProvider>`
        - mapper / `IMapper`
    - Create a constructor for `UtilityProviderAdminController` with two arguments of type `IWriteRepository<int, UtilityProvider>` and `IMapper`
        - Assign the value of the passed arguments to the corresponding properties with the same type
    - Create a method for `UtilityProviderAdminController` named `Post` with one argument of type `UtilityProviderDto`
        - In the body, write logic similar to the `Post()` method in the `HomeAdminController`, allowing you to map and save a new `UtilityProvider` from the data passed in from the `UtilityProviderDto`
        - Ensure you return a `Created` HTTP status code, along with the created `UtilityProvider` and its location

In `HomeEnergyApi/Models/UtilityProviderRepository.cs`
- Create a public class `UtilityProviderRepository` implementing `IWriteRepository<int, UtilityProvider>` and  `IReadRepository<int, UtilityProvider>`
    - Write code similar to that in `HomeRepository.cs` so as to ensure identical functionality for `UtilityProvider`s including the ability to Save, Update, Remove, Find all, Find by Id, and return a count

In `HomeEnergyApi/Dtos/UtilityProviderDto.cs`
- Create a public class `UtilityProviderDto` with the following property names / types
    - Name / `string`
    - ProvidedUtilities / `ICollection<string>`

In `HomeEnergyApi/Dtos/HomeProfile.cs`
- Create a new map for the source `UtilityProviderDto` to destination `UtilityProvider`
    - You won't need to add member specific logic as you did for `HomeDto`

In `HomeEnergyApi/Controllers/HomeAdminController.cs`
- Add a private property `homeUtilityProviderService` of type `HomeUtilityProviderService`
- Add an argument to the constructor of type `HomeUtilityProviderService` and assign it's value to the new property `homeUtilityProviderService`
- In the `CreateHome()` method, add a line after the home is saved that calls `homeUtilityProviderService.Associate()` with the arguments `home` and `homeDto.UtilityProviderIds`

In `HomeEnergyApi/Dtos/HomeDto.cs`
- Add a public property `UtilityProviderIds` of type `ICollection<int>?`

In `HomeEnergyApi/Services/HomeUtilityProviderService.cs`
- Create a public class `HomeUtilityProviderService`
    - Give `HomeUtilityProviderService` the following property names / types
        - utilityProviderRepository / `IReadRepository<int, UtilityProvider>`
        - homeUtilityProviderRepository / `IWriteRepository<int, HomeUtilityProvider>`
    - Create a constructor for `HomeUtilityProviderService` taking two arguments of types `IReadRepository<int, UtilityProvider>` and `IWriteRepository<int, HomeUtilityProvider>`
        - Assign the value of the passed arguments to the corresponding properties with the same type
    - Create a method `Associate()` taking two arguments of types `Home` and `ICollection<int>` (the passed  `ICollection<int>` will be Utility Provider Ids)
        - If the passed Utility Provider Ids are not null...
            - For each passed Utility Provider Id...
                - Find the `UtilityProvider` associated with the passed Id and assign it to a variable
                - Create a new `HomeUtilityProvider`
                    - Set the `UtilityProvider` property to the found `UtilityProvider`
                    - Set the `UtilityProviderId` property to id used to find the `UtilityProvider`
                    - Set the `HomeId` property to the passed home's Id
                    - Set the `Home` property to the passed home
                - Save the created `HomeUtilityProvider` to the private property of type `IWriteRepository<int, HomeUtilityProvider>`

In `HomeEnergyApi/Models/HomeUtilityProviderRepository.cs`
- Create a public class `HomeUtilityProviderRepository` implementing `IWriteRepository<int, HomeUtilityProvider>` and  `IReadRepository<int, HomeUtilityProvider>`
    - Write code similar to that in `HomeRepository.cs` so as to ensure identical functionality for `HomeUtilityProvider`s including the ability to Save, Update, Remove, Find all, Find by Id, and return a count

In `HomeEnergyApi/Models/HomeRepository.cs`
- Modify the `FindAll()` method to include `HomeUtilityProvider`s in the list it returns

In `HomeEnergyApi/Program.cs`
- Similar to how the Home repository and associated read/write repositories are being added as a scoped service, add `UtilityProviderRepository` and associated read/write repositories as scoped services
- Similar to how the Home repository and associated read/write repositories are being added as a scoped service, add `HomeUtilityProviderRepository` and associated read/write repositories as scoped services
- Add `HomeUtilityProviderService` as a transient service

In your terminal
- ONLY IF you are working on codespaces or a different computer/environment as the previous lesson and don't have `dotnet-ef` installed globally, run `dotnet tool install --global dotnet-ef`, otherwise skip this step
    - To check if you have `dotnet-ef` installed, run `dotnet-ef --version`
- Run `dotnet ef migrations add AddManyToManyUtilityProviders`
- Run `dotnet ef database update`

## Additional Information
- Some Models may have changed for this lesson from the last, as always all code in the lesson repository is available to view
- Along with `using` statements being added, any packages needed for the assignment have been pre-installed for you, however in the future you may need to add these yourself

## Building toward CSTA Standards:
- Create prototypes that use algorithms to solve computational problems by leveraging prior student knowledge and personal interests (3A-AP-13) https://www.csteachers.org/page/standards
- Decompose problems into smaller components through systematic analysis, using constructs such as procedures, modules, and/or objects (3A-AP-17) https://www.csteachers.org/page/standards
- Create artifacts by using procedures within a program, combinations of data and procedures, or independent but interrelated programs (3A-AP-18) https://www.csteachers.org/page/standards
- Use and adapt classic algorithms to solve computational problems (3B-AP-10) https://www.csteachers.org/page/standards
- Demonstrate code reuse by creating programming solutions using libraries and APIs (3B-AP-16) https://www.csteachers.org/page/standards

## Resources
- https://github.com/AutoMapper/AutoMapper

Copyright &copy; 2025 Knight Moves. All Rights Reserved.

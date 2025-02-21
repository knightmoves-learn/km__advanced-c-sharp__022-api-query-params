# 022 Query Parameter

## Lecture

[![# Query Parameter](https://img.youtube.com/vi/T9uVniEoTpg/0.jpg)](https://www.youtube.com/watch?v=T9uVniEoTpg)

## Instructions

At the start of this lesson, you'll notice the `UtilityProvider` model and all associated code has been made blank. This is because you will be rebuilding that model as a new Many to Many relationship.

In `HomeEnergyApi/Controllers/HomeController.cs`
- On `HomeController`, create a new private property `homeByOwnerLastNameRepository` of type `IOwnerLastNameQueryable<Home>`
- On the constructor for `HomeController` add an argument of type `IOwnerLastNameQueryable<Home>` and assign it's value to the newly created property `homeByOwnerLastNameRepository`
- Modify the `Get()` method
    - Add a new argument `ownerLastName` of type `string?` with the and designate it as a query parameter
    - If the query parameter is null, the behavior should be the same as before
    - If the query parameter is not null, the method should return a `200: Ok` HTTP status code, and the result of `homeByOwnerLastNameRepository.FindByOwnerLastName((string)ownerLastName)`

In `HomeEnergyApi/Models/IOwnerLastNameQueryable.cs`
- Create a new public interface `IOwnerLastNameQueryable<T>`
    - Create an interface method `FindByDate()` taking one argument of type `string`

In `HomeEnergyApi/Models/HomeRepository.cs`
- Have `HomeRepository` implement the interface `IOwnerLastNameQueryable<Home>`
- Implement the member of `IOwnerLastNameQueryable<T>`, `FindByOwnerLastName()` with one argument of type `string`
    - Have this method return the same set of homes as `FindAll()`, except only including those where the passed `string` matches the home's `OwnerLastName` property

In `HomeEnergyApi/Program.cs`
- Add a new scoped service with type `IOwnerLastNameQueryable<Home>` and passing the required service `HomeRepository` as its provider

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

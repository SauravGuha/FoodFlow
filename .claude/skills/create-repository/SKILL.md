---
name: create-repository
description: Create a repository interface and implementation for an existing domain model.
disable-model-invocation: true
argument-hint: <Domain model name>
---

# Create Repository

Create the repository interface and repository implementation for an existing domain model.

## Preconditions

Assume that:

- The domain model already exists.
- `FoodFlowContext` already contains the corresponding `DbSet<T>`.
- The EF Core `IEntityTypeConfiguration<T>` already exists.
- The repository layer and its base abstractions already exist.

Do not create or modify the domain model, DbContext, or entity configuration unless explicitly requested.

## Input

The domain model name is provided as `$ARGUMENTS`.

Example:

`/create-repository Cuisine`

Treat `$ARGUMENTS` as the requested domain model name.

## Instructions

1. Locate the requested domain model in `FoodFlow.Domain`.

2. Use RestaurantRepo as the primary reference. Only inspect BaseRepository

3. Register newly created repo for it's interface to services if required in `FoodFlow.Persistence`

4. Inspect `BaseRepository<T>` and determine what functionality it already provides.

5. Determine whether the requested entity actually requires a dedicated repository.

6. Create the repository interface in the existing repository-interface location (D:\source\repo\FoodFlow\src\backend\FoodFlow.Application\Common\Repositories), following the project's naming and namespace conventions.

7. Create the repository implementation in the existing repository implementation location.

8. Reuse `BaseRepository<T>` and existing repository abstractions instead of duplicating functionality.

9. Do not introduce a new repository pattern or abstraction.

10. Match the existing:

- Naming conventions
- Namespace structure
- Constructor/DI pattern
- Async conventions
- Method naming
- Access modifiers
- Dependency injection conventions

10. Only add entity-specific methods when the existing use cases require functionality that the base repository does not provide.

11. If dependency injection registration is required by the existing repository pattern, update the appropriate registration.

12. Do not modify unrelated files.

## Verification

After creating the repository:

1. Build the affected backend projects.
2. Fix compilation errors caused by the implementation.
3. Report the files created or modified.
4. Report the build result.

## Important

Do not blindly copy the Restaurant repository.

Use it as the reference implementation and adapt the implementation to the requested entity.

If the existing architecture does not require a dedicated repository for the requested entity, explain why before creating unnecessary code.

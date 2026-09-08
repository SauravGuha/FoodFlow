---
name: create-repository
description: Create a repository interface and implementation for an existing domain model.
disable-model-invocation: true
argument-hint: <Domain model name>
---

# Create Repository

Create the repository interface and implementation for the domain model supplied as `$ARGUMENTS`.

## Preconditions

Assume:
- The domain model exists.
- `FoodFlowContext` has its `DbSet<T>`.
- Its `IEntityTypeConfiguration<T>` exists.
- The repository layer and base abstractions exist.

Do not create or modify the model, DbContext, or entity configuration unless explicitly requested.

## Instructions

1. Locate `$ARGUMENTS` in `FoodFlow.Domain`.
2. Use `RestaurantRepo` and its interface as the primary reference. Inspect `BaseRepository<T>` when needed.
3. Determine whether a dedicated repository is required; do not add one unnecessarily.
4. Create the interface in `FoodFlow.Application/Common/Repositories`, following existing naming and namespace conventions.
5. Create the implementation in the existing repository implementation location.
6. Reuse `BaseRepository<T>` and existing abstractions; do not introduce another repository pattern.
7. Match existing naming, namespaces, constructor/DI, async, methods, access modifiers, and registration conventions.
8. Add entity-specific methods only when required by existing use cases and unsupported by the base repository.
9. Register the interface/implementation in `FoodFlow.Persistence` if required by the existing DI pattern.
10. Do not modify unrelated files.

## Verification

1. Build affected backend projects.
2. Fix compilation errors caused by the change.
3. Report created/modified files and build result.

## Important

Do not blindly copy Restaurant; adapt to the requested entity and its domain behavior.

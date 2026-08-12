# FoodFlow Backend

## Architecture

The backend follows Clean Architecture:

- `FoodFlow.Domain` — domain entities and business rules
- `FoodFlow.Application` — use cases, CQRS, DTOs, validation, mappings
- `FoodFlow.Persistence` — EF Core, repositories, database configuration
- `FoodFlow.Api` — HTTP/API concerns

Dependency direction must remain inward:

`Api → Application → Domain`

`Persistence → Application/Domain`

Domain must not depend on Application, Persistence, or Api.

## Patterns

- CQRS is implemented with MediatR.
- Commands represent state-changing operations.
- Queries represent read operations.
- FluentValidation is integrated through a MediatR pipeline.
- EF Core is used for persistence.
- Follow existing repository and mapping patterns.
- Controllers should remain thin and delegate application work to MediatR.

## Existing Implementation

The Restaurant module is the primary reference implementation.

Before implementing similar functionality:

1. Inspect the existing Restaurant implementation across Domain, Application, Persistence, and Api.
2. Follow its established patterns.
3. Reuse existing abstractions where appropriate.
4. Do not introduce a new pattern unless there is a clear reason.

Do not blindly copy Restaurant code; understand the differences in domain behavior.

## Domain Rules

Business rules belong in the Domain model where appropriate.

Do not bypass domain methods by directly manipulating state when an existing domain method represents the operation.

For example, Restaurant currently manages its cuisines and branches through domain behavior.

Preserve existing domain invariants unless the requirements explicitly change them.

## EF Core

- Entity configurations belong in `FoodFlow.Persistence/Configuration`.
- Follow the existing `IEntityTypeConfiguration<T>` pattern.
- `FoodFlowContext` discovers configurations through `ApplyConfigurationsFromAssembly`.
- Keep filtering, ordering, paging, and counting database-side.
- Avoid premature `ToList()`/materialization.
- Review generated SQL when query performance is relevant.
- Database model changes require an appropriate EF Core migration.

Before creating a new repository or abstraction, check whether the existing `BaseRepository<T>` already supports the required operation.

## Validation

Use FluentValidation for application request validation.

Do not move business rules into validators merely because they can be checked there. Domain invariants must remain protected by the domain model.

## DTOs and Mapping

- DTOs belong in the Application layer.
- Do not expose domain entities directly from API endpoints.
- Inspect existing mapping configuration before adding manual mapping.
- Follow the mapping approach already used by the project.
- Do not introduce another mapping library or mapping pattern.

## Code Changes

- Prefer existing project conventions over personal preferences.
- Keep changes focused on the requested task.
- Do not refactor unrelated code.
- Do not introduce abstractions without a concrete need.
- Do not change architecture without discussing it first.
- Do not commit or push unless explicitly asked.

## Verification

After making changes:

1. Build the affected project(s).
2. Run relevant tests if available.
3. Check compiler errors and warnings introduced by the change. Ignore warnings for deprecated libraries
4. If the database schema changed, review the generated migration.

Do not claim a change is working without appropriate verification.

## Learning Context

This is also a learning project.

For meaningful architectural or design decisions:

- Explain the reasoning.
- Explain important trade-offs.
- Recommend an approach rather than silently changing architecture.
- Prefer teaching the existing codebase's patterns over introducing unnecessary new ones.

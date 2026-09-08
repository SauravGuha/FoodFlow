# FoodFlow Backend

## Architecture

- `FoodFlow.Domain`: domain entities and business rules.
- `FoodFlow.Application`: use cases, CQRS, DTOs, validation, mappings.
- `FoodFlow.Persistence`: EF Core, repositories, database configuration.
- `FoodFlow.Api`: HTTP/API concerns.

Keep dependencies inward: `Api → Application → Domain`; `Persistence → Application/Domain`. Domain must not depend on outer layers.

## Patterns

- CQRS uses MediatR; commands change state, queries read state.
- FluentValidation runs through the MediatR pipeline.
- Keep controllers thin and delegate to MediatR.
- Follow existing repository and mapping patterns.

## Existing Patterns

Restaurant is the primary reference. Before implementing similar functionality, inspect its Domain, Application, Persistence, and Api code; reuse existing abstractions and patterns without blindly copying domain behavior.

Business rules belong in the Domain. Preserve domain invariants and use existing domain methods instead of directly manipulating state.

## EF Core

- Put entity configurations in `FoodFlow.Persistence/Configuration` using `IEntityTypeConfiguration<T>`.
- `FoodFlowContext` discovers configurations with `ApplyConfigurationsFromAssembly`.
- Keep filtering, ordering, paging, and counting database-side; avoid premature materialization.
- Review generated SQL when query performance matters.
- Schema changes require an EF Core migration.
- Before adding a repository/abstraction, check whether `BaseRepository<T>` already provides the required operation.

## Validation, DTOs & Mapping

- Use FluentValidation for application request validation; keep domain invariants in the Domain.
- DTOs belong in Application; do not expose domain entities directly from API endpoints.
- Follow the existing mapping approach; do not introduce another mapping library/pattern.

## Changes

- Prefer project conventions over personal preferences.
- Keep changes focused; do not refactor unrelated code or add abstractions without a concrete need.
- Discuss architectural changes first.
- Do not commit or push unless explicitly asked.

## Verification

After changes:
1. Build affected projects.
2. Run relevant tests when available.
3. Check introduced errors/warnings; ignore warnings for deprecated libraries.
4. If the schema changed, review the migration.

Do not claim verification without performing it.

## Learning

This is also a learning project. For meaningful architectural/design decisions, explain reasoning and trade-offs and recommend rather than silently changing architecture. Prefer teaching existing patterns over unnecessary new ones.

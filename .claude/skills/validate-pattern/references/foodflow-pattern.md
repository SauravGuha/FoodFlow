# FoodFlow Architectural Pattern

## General

- Follow Clean Architecture boundaries.
- Keep Domain independent of Application, Persistence, and API.
- Application contains use cases and coordinates domain operations.
- Persistence contains EF Core and repository implementations.
- API contains HTTP controllers and request/response handling.
- Do not bypass an established layer or abstraction.
- Reuse existing project abstractions instead of introducing parallel patterns.

## Domain

- Domain models contain domain state and domain behavior.
- Domain constructors/factory methods enforce domain invariants.
- Persistence concerns must not be placed in Domain.
- API request/response concerns must not be placed in Domain.

## Persistence

### Entity Configuration

- Entity configuration implements `IEntityTypeConfiguration<T>`.
- Configuration is kept separate from the domain model.
- Relationships, keys, constraints, indexes, lengths, required fields, and database-specific configuration belong in Persistence.
- Follow the existing configuration registration/discovery pattern.

### Repository

- Repository interfaces belong to the established repository abstraction location.
- Repository implementations belong to Persistence.
- Reuse `BaseRepository<T>` where applicable.
- Do not duplicate functionality already provided by the base repository.
- Add entity-specific repository methods only when required by the application's use cases.
- Repository implementations follow the established dependency-injection pattern.

### DbContext

- `DbSet<T>` is defined in the application's DbContext when required.
- The DbContext is responsible for EF Core persistence configuration.
- Do not place application/business logic in the DbContext.

## Application

### Commands

- State-changing operations are represented by commands.
- Commands follow the established MediatR structure.
- Command handlers coordinate the use case.
- Handlers use repositories/domain abstractions rather than directly accessing EF Core.
- Domain objects should enforce domain invariants.

### Queries

- Read operations are represented by queries.
- Queries follow the established MediatR structure.
- Query handlers use the established read/data-access pattern.
- Do not introduce a separate query pattern when an existing abstraction already supports the use case.

### Validation

- Commands and queries use validators where validation is required.
- Validation rules follow the established FluentValidation pattern.
- Validation should handle request/input constraints.
- Domain invariants remain the responsibility of the domain model.

### DTOs

- API/application boundaries use DTOs where established by the existing architecture.
- DTOs should contain data required by the boundary rather than persistence entities.
- Do not expose persistence entities directly when the established pattern uses DTOs.

### Mapping

- Follow the project's established mapping mechanism.
- Keep mapping concerns out of controllers and domain models.
- Do not introduce another mapping library/pattern when an existing project convention applies.

### Result Handling

- Use the application's established `Result<T>` pattern for application responses.
- Preserve the established success/failure and status-code handling.
- Do not create a parallel result/error abstraction.

## API

### Controllers

- Controllers follow the established base-controller inheritance pattern.
- Controllers are responsible for HTTP concerns.
- Controllers delegate application operations to MediatR.
- Controllers should not contain business logic or direct persistence logic.

### Routing

- Follow established controller and action routing conventions.
- Resource relationships should be reflected in routes when appropriate.
- Do not invent a new routing convention for an individual model.

### Responses

- Controllers use the established application result-to-HTTP response conversion.
- Follow existing HTTP status-code conventions.
- Do not introduce a separate response wrapper or error format.

## Dependency Injection

- Register new implementations using the existing DI registration pattern.
- Do not instantiate repositories, handlers, or services manually when they are already managed by DI.
- Do not introduce a second registration mechanism.

## Cross-Layer Consistency

Verify that:

- Domain properties match persistence configuration.
- Foreign-key relationships are consistent across Domain, Persistence, and Application.
- Commands match their handlers.
- Queries match their handlers.
- DTOs match their mapping configuration.
- Controllers call the appropriate MediatR requests.
- Controller routes match the intended resource relationships.
- Repository interfaces match their implementations.
- DI registrations resolve the required implementations.

## Naming Conventions

### Domain

- Domain models use PascalCase.
- Domain model files are named `<Model>.cs`.
- Models are grouped under the appropriate `<Module>Models` directory.

### Persistence

- Entity configuration files use `<Model>.cs`.
- Repository implementations use `<Model>Repo.cs`.
- Repository interfaces follow the established repository interface naming convention.

### Application Commands

- Commands are grouped under `<Model>Commands`.
- Command operations use `<Operation><Model>`.
- Command files use `<Operation><Model>Command.cs`.
- Handlers use `<Operation><Model>CommandHandler.cs`.
- Validators use `<Operation><Model>CommandValidator.cs`.

### Application Queries

- Queries are grouped under `<Model>Queries`.
- Request files use `<Operation>Request.cs`.
- Handlers use `<Operation>RequestHandler.cs`.
- Validators use `<Operation>RequestValidator.cs`.

### DTOs

- DTO files use `<Model>Dto.cs`.
- DTOs are placed in the established `DTOModels` location.

### API

- Controllers use `<Model>Controller.cs`.
- Controller names use PascalCase.
- A model does not require its own controller when its resource is intentionally exposed through a parent/resource controller.

### General

- Use PascalCase for C# types and files.
- Names should describe the responsibility of the type.
- Do not introduce alternative suffixes when an established FoodFlow suffix exists.

## Pattern Exceptions

A model does not need to contain every element listed above.

Only report a deviation when:

1. The rule applies to the model.
2. The existing architecture establishes the rule.
3. The implementation violates or bypasses that rule.

Model-specific behavior is not a pattern violation merely because it differs from another model.

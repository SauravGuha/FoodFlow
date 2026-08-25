---
name: seed-model-data
description: Generate at least 50 random valid records for a model through its POST API.
disable-model-invocation: true
argument-hint: <Domain model name>
---

# Seed Model Data

Generate at least 50 random, valid records for the model supplied as `$ARGUMENTS`.

## Rules

1. Inspect the model, `Create<Model>command`, `Create<Model>CommandValidator`, domain rules, `Create<Model>CommandHandler`, and EF configuration before generating data.
2. Discover the model's actual POST resource from the API controllers. Assume the controller is named `<Model>Controller`.
3. API base url is `https://localhost:7070`
5. Verify the API using `HEAD /api/App`. Do not use `/` or a model endpoint as the API availability check.
6. Build request bodies from the `Create<Model>command`/DTO. Respect required fields, maximum lengths, formats, enums, value ranges, validation rules, and domain rules.
7. Inspect handlers and EF configuration for uniqueness, foreign keys, and business rules. Generate data that satisfies them.
8. POST records individually. Create at least 50 successful records; failed requests do not count.
9. Use random data. Do not use fixed sample values or identical records. Keep values unique where required.
10. Do not modify source code, database files, migrations, or configuration.

## Endpoint Discovery

Do not infer endpoints from model names or REST conventions. Discover them from the actual controllers and application flow.

### Create endpoint

1. Search all API controllers for an `[HttpPost]` action that creates the requested model.
2. The action may be in:
   - `<Model>Controller`, or
   - a parent/resource controller.
3. Follow the action parameter to its create command/DTO and confirm that it creates `$ARGUMENTS`.
4. Read the controller `[Route(...)]` and action `[HttpPost(...)]`.
5. Combine the routes to obtain the actual POST URL.
6. Do not require a `<Model>Controller` to exist.

Example:

```csharp
[Route("api/[controller]")]
public class RestaurantController : AppController
{
    [HttpPost("cuisines")]
    public async Task<IActionResult> CreateCuisine(
        CreateCuisineCommand command, ...)
}
```

## Completion

Report:
- Model name
- POST endpoint used
- Number of successful records created
- Foreign-key endpoints used
- Any failures or skipped records

Stop only after at least 50 successful records have been created, or after reporting a blocking error such as a missing endpoint, unavailable API, missing foreign-key data, or unrecoverable server failure.

---
name: seed-model-data
description: Generate at least 50 random valid records for a model through its POST API.
disable-model-invocation: true
argument-hint: <Domain model name>
---

# Seed Model Data

Generate and POST at least 50 random, valid records for the model supplied as `$ARGUMENTS`.

## Rules

1. Inspect the model, its create command/DTO, validator, domain constructor/rules, and EF configuration before generating data.
2. Find the model's POST resource URL from its API controller. Do not invent an endpoint.
3. Find the API base URL from the project's launch settings or existing configuration. Prefer the HTTP URL when both HTTP and HTTPS are available locally.
4. Verify the API is reachable before creating records. If it is not running, report the error and stop.
5. Build request bodies from the create command/DTO, not from persistence/domain-only properties. Respect required fields, maximum lengths, formats, enums, value ranges, and domain rules.
6. Inspect EF configuration and application handlers for unique constraints or cross-field/business rules. Generate values that satisfy them.
7. POST records individually to the discovered create endpoint. Create at least 50 successful records; do not count failed requests.
8. Use random data. Avoid fixed sample data or identical records. Keep values valid and make fields unique when required by the model or database.
9. For every foreign-key field, identify the referenced model and find a GET endpoint that returns a list of existing referenced records. If no such list endpoint exists, report an error and stop before creating dependent records.
10. Call the foreign-key model's list endpoint, extract valid IDs, and use only those IDs. For nested list endpoints, resolve the required parent foreign key first.
11. Preserve foreign-key relationships. If multiple foreign keys must refer to related records, choose compatible combinations rather than random unrelated IDs.
12. If a foreign-key list is empty, report the dependency and stop; do not invent IDs.
13. If a POST fails because generated data violates a discovered rule, discard that record, adjust the generation strategy, and retry with new data. Do not weaken or bypass validation.
14. Do not create prerequisite records unless the user explicitly asks for them; use existing records exposed by GET list endpoints.
15. Do not modify source code, database files, migrations, or application configuration.

## Endpoint Discovery

- Inspect controllers for `[HttpPost]` and `[HttpGet]` actions and combine controller and action routes to determine the actual URL.
- A `GET /{id}` endpoint is not a list endpoint. A foreign-key dependency requires an endpoint that returns multiple existing records.
- For nested list endpoints, inspect the action and request type to determine required route/query parameters.
- Parse the project's standard API result wrapper when present; use its `Data` collection and record `Id` values.

## Completion

Report:
- Model name
- POST endpoint used
- Number of successful records created
- Foreign-key endpoints used
- Any failures or skipped records

Stop only after at least 50 successful records have been created, or after reporting a blocking error such as a missing endpoint, unavailable API, missing foreign-key data, or unrecoverable server failure.

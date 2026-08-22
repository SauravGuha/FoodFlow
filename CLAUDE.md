# FoodFlow

FoodFlow is a restaurant management and food ordering application. Backend-specific instructions are in `src/backend/CLAUDE.md`.

## Rules

- Understand existing code and patterns before changing them.
- Prefer existing conventions and abstractions; do not introduce duplicate patterns.
- Keep changes focused; do not refactor unrelated code.
- Discuss architectural changes before making them.
- Do not commit or push unless explicitly asked.

## Learning

This is also a learning project. For meaningful design decisions, explain the reasoning and trade-offs and teach the existing pattern rather than silently introducing a different approach. For straightforward changes following an existing pattern, proceed without unnecessary discussion.

## Verification

- Build affected projects after changes.
- Run relevant tests when available.
- Check for errors and warnings introduced by the change.
- Do not claim verification without performing it.
- When an API endpoint is available locally and the task requires creating, updating, or querying data through HTTP, use `curl` (via Bash) to exercise the endpoint when practical.
- Inspect the controller, request DTO, validation, and existing API conventions before constructing the HTTP request.
- Do not claim an HTTP operation succeeded without actually executing it and checking the response.
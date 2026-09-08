---
name: validate-pattern
description: Validate a model against FoodFlow architectural patterns.
disable-model-invocation: true
argument-hint: <model-name>
---

# Validate Pattern

**READ-ONLY. DO NOT MODIFY THE REPOSITORY.**

Validate whether the model specified by `$ARGUMENTS` follows the established
FoodFlow architectural pattern.

`$ARGUMENTS` is the domain model name to validate.

Example:

```text
/validate-pattern Branch

## Instructions

1. Read `references/foodflow-pattern.md`.
2. Inspect the complete implementation of the requested model.
3. Validate the implementation against the applicable rules.
4. Check Persistence, Application, and API layers.
5. Check cross-layer consistency.
6. Distinguish between:
   - Pattern compliance
   - Pattern deviation
   - Intentional model-specific behavior
   - Actual implementation problems
7. Do not require model-specific behavior that is not applicable to the requested model.
8. Do not modify source code.

## Output

Report:

- Compliant areas
- Deviations
- Missing implementation
- Potential problems
- Recommended changes

For every deviation include:

- Layer
- File
- Rule
- Current implementation
- Recommendation
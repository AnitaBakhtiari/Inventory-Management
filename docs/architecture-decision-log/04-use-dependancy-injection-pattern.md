# 4. Dependency Injection pattern

Date: 2024-11-03

## Status:  

Accepted

## Context

We need a way to manage dependencies in our application to improve code maintainability, testability, and flexibility.

## Decision

Implement the Dependency Injection (DI) pattern to decouple classes from their dependencies and manage object lifetimes.

## Consequences

- Choose Microsoft.Extensions.DependencyInjection DI container for implement DI
- Ensure classes depend on abstractions rather than concrete implementations

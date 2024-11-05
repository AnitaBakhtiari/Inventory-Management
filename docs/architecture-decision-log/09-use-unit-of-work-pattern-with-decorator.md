# 9. Use Unit of Work Pattern with Decorator Behavior

Date: 2024-11-04

## Status:  

Accepted

## Context

We need a way to manage multiple data operations in a single transaction while maintaining clean code and ensure about atomicity based on ACID principles

## Decision

Implement the Unit of Work pattern and utilize Pipeline Behavior for command request specifically for saving changes and managing cross-cutting concerns.

## Consequences

- Create a Unit of Work interface and implementation for managing transactions.
- Use Pipeline Behavior for saving changes

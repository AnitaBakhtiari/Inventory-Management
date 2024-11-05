# 8. Use Repository and Unit of Work Pattern

Date: 2024-11-04

## Status:  

Accepted

## Context

We need a way to manage data access more effectively in our application to have clear structure and makes it simpler to mock data operations in tests.
and also manage multiple data operations in a single transaction while maintaining clean code and ensure about atomicity based on ACID principles


## Decision

We implement the Repository Pattern to organize data access and improve code structure and Implement the Unit of Work pattern for command request specifically for saving changes.


## Consequences

- Create a Unit of Work interface and implementation for managing transactions.
- Create a Repository interfaces and classes for Entities


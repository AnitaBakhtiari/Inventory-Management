# 4 Use CQRS architectural style

Date: 2024-11-03

## Status: 

Accepted

##  Context

The app needs to handle two main types of requests:

Reading - When reading, we want simple data, like tables or lists.
Writing - When writing, we need a more complex object model to handle validations, business rules, and calculations.


## Decision

We decided to use the CQRS (Command Query Responsibility Segregation) pattern. In each business area, we will have a separate model for reading and another for writing. To keep it simple, the read model will have immediate consistency for now. This approach is useful even for simpler areas like managing User Access.

## Consequences

We’ll have models that are optimized separately for reading and writing (following SRP - Single Responsibility Principle).
We can handle Commands and Queries differently.
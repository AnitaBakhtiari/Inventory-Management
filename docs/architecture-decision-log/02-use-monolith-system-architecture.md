# 2.  Use Simple Monolithic Architecture

Date: 2024-11-03

## status

Accepted

## Context

I want to create a project that is easy to work with. Using a simple monolithic structure with separate layers and some DDD ideas will help.

## Decision

I decided to build a monolithic application with Three main layers(folder):

- Domain Layer: This part will deal with the main business rules and logic.
- Application Layer: This part will handle the application logic and use DDD patterns like commands and queries to do things.
- Infrastructure Layer: This part will take care of how we store data and connect to other services, using repositories for data.

I will keep each layer in its own folder to stay organized.

## Consequences

- The application will be easy to manage because it’s all in one piece (monolithic).
- Having separate layers makes the code cleaner and easier to understand.
- Using DDD ideas will help me adjust the project if things change.
- Changes in one layer won’t mess up the others, making it easier to work on.
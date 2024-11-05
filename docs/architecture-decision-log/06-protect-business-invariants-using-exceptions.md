# 6. Protect Business Rules Using Exceptions

Date: 2024-11-04

## Status:  

Accepted

## Context

Application must check business rules. If a rule is broken, processing should stop, and an error should be returned to the client.

## Decision

We will use exceptions to handle business rule violations.

## Consequences

- Create a BusinessException class for business rule errors.
- Create specific error handling 
- Define different exceptions for each rule.
- Minimal performance impact from throwing exceptions.
- Fewer if-else statements in entities.

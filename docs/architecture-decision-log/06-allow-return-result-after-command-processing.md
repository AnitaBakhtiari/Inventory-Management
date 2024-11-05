# 6. Allow Returning Result After Command Processing

Date: 2024-11-03

## Status

Accepted

## Context

In CQRS and CQS principles, Commands shouldn’t return any result—they should just perform an action. But sometimes, we need to return information right after processing a Command, like the ID of a newly created object or for operations where we can't clearly separate Command and Query

## Decision

We decided to allow returning results after command processing, when:
- create an object and need to return its ID.

## Consequences

- We'll have two types of Command definitions: some that return results, and others that don’t.
- We can immediately return the ID of created object/resource. We don't need a second call (query) to retrieve this ID.
- We need to be careful not to overuse this and try to stick to CQRS principles as much as possible
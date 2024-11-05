# Inventory Management


## Overview

The Inventory Management project is designed to streamline Inventory operations by tracking products as they enter and exit the warehouse. Built with .NET 8, this project includes Docker support for easy setup and deployment.

###  How It Works

-  Entry Issuance: When new products arrive, each receives a unique ID, and an entry document is issued to confirm their arrival.
-  Exit Issuance: For products that are released from the warehouse, an exit document is issued following an exit request.
-  Refund Issuance: If any product is found to be defective or damaged, the system manages it according to predefined scenarios for defective items.


## Sprint History

### Sprint 1: Inventory Management

Goal: Set up New product registration functionality.

- Features:
  - Submit entry issuance with unique product serial.
  - Generate issuance tracking code.
- Tasks:
  - Create database schema for issuance documents and product.
  - Implement API endpoint for entry issuance documents.
  - Write unit and integration tests.
  - Document architectural decisions (ADR).


### Sprint 2: Exist issuance documents for Online Sales

Goal: Record Exist issuance for  online sales transactions.

- Features:
  - Submit exist issuance with unique product serial.
  - Generate issuance tracking code.
- Tasks:
  - Create database schema for issuance documents.
  - Implement API endpoint for entry issuance documents.
  - Write unit and integration tests.
  - Document architectural decisions (ADR).


### Sprint 3: Inventory Counting

Goal: Implement inventory counting and inquiry.

- Features:
  - Inquiry of current stock levels.
  - View inventory by product type and product brand.
- Tasks:
  - Implement API endpoint for inventory counts.
  - Set up inventory count report.
  - Write unit and integration tests.
  - Document architectural decisions (ADR).


### Sprint 4: Refund Management

Goal: Process refunds and replacements.

- Features:
  - Create refund issuance documents for returned product.
  - Issue replacements for returned items.
- Tasks:
  - Implement API endpoint for refund issuance documents.
  - Update inventory product instance availability for refunds and replacements.
  - Write unit and integration tests.
  - Document architectural decisions (ADR).


## User Stories

- As a inventory manager, I want to add product(ex. laptops) to the inventory with unique serial numbers to prevent duplicates.
- As a inventory manager, I want to create an issuance with a tracking code to manage online sales effectively.
- As an inventory manager, I want to perform daily inventory counts to ensure accurate stock levels.
- As an inventory manager, I want to create a refund issuance for a defective or damaged product and provide a replacement.


### Branching Strategy

We use a Feature Branch Strategy to manage code development. Each feature is developed in isolation, merged into the main branch only after thorough testing.

## Dockerized Setup

This project is Dockerized for easy deployment. Follow these steps to get started:

### Clone the Repository

 ```bash
git clone https://github.com/AnitaBakhtiari/Inventory-Management.git
cd Inventory-Management
```
### Build the Docker Image

 ```bash

docker build -t inventory-management .
```

### Run SQL Server using Docker (optional):

   ```bash

    docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=MyPass@word" -p 1433:1433 --memory="2g" --cpus="2" -v sqlvolume:/var/opt/mssql -d --name=sql mcr.microsoft.com/azure-sql-edge
  ```


### Run the Application

 ```bash
docker run -p 8080:80 inventory-management
```

The API will be available at http://localhost:8080.


## Contributing

- We welcome contributions! To get started, fork the repository, make your changes, and submit a pull request.
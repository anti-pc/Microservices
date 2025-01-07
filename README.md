# Microservices

This repository showcases a collection of microservices developed to demonstrate various aspects of microservice architecture.

## Table of Contents

- [Introduction](#introduction)
- [Architecture Overview](#architecture-overview)
- [Services](#services)
- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)
  
## Introduction

Microservices architecture involves developing a single application as a suite of small services, each running in its own process and communicating with lightweight mechanisms. This approach offers flexibility in deployment and scaling, making it a popular choice for modern application development.

## Architecture Overview

The system comprises multiple services, each responsible for a specific domain. Services communicate through well-defined APIs, ensuring modularity and independence.

## Services

The repository includes the following services:

- **Service A**: Handles user authentication and authorization.
- **Service B**: Manages product catalog and inventory.
- **Service C**: Processes orders and payments.

*Note: will be replaced the service names and descriptions*

## Getting Started

Follow these instructions to set up and run the project on your local machine.

### Prerequisites

Ensure you have the following installed:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Installation

1. **Clone the repository**:

   ```bash
   git clone https://github.com/anti-pc/Microservices.git
   cd Microservices
   ```

2. **Build and start the services**:

   ```bash
   docker-compose up --build
   ```
### Usage

Once the services are running, access the application via `http://localhost:8000` (adjust the port as necessary).

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your changes. Ensure your code adheres to the project's coding standards and includes appropriate tests.
   
Program.cs 
------------------------------
builder.Services.AddAutoMapper(typeof(Program));  
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

https://docs.portainer.io/start/install-ce/server/docker/linux  
cmd > docker volume create portainer_data  
cmd > docker run -d -p 8000:8000 -p 9000:9000 --name=portainer --restart=always -v /var/run/docker.sock:/var/run/docker.sock -v portainer_data:/data portainer/portainer-ce  


RabbitMQ
------------------------------
cmd > docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management
default user & pass : guest guest

Containers
-----------------------
> mssqlserver
> postgres
> redis
> mongoDB
> rabbitMQ
> portainer

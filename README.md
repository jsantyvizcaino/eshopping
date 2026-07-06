# EShopping - Microservices E-Commerce Platform

## Overview
Microservices-based e-commerce platform demonstrating **Ocelot API Gateway**, distributed architecture, and Docker containerization.

**Stack:** .NET 6/7, Ocelot, Entity Framework Core, Angular, SQL Server, Docker

## Architecture
- **API Gateway** (Ocelot) - Routes & orchestrates requests
- **Catalog Service** - Product management
- **Order Service** - Order processing
- **Infrastructure** - Shared utilities, logging, middleware

## Tech Highlights
- Clean Architecture principles
- Microservices pattern with API Gateway
- Docker Compose multi-container orchestration
- Entity Framework Core for data access
- Angular frontend with responsive design

## Quick Start
```bash
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
cd client && npm install && ng serve
```

## What I Built
- Distributed service architecture
- API Gateway with request routing
- Docker containerization for development
- Full-stack .NET + Angular integration

**Note:** This project was developed following architectural best practices for microservices learning.

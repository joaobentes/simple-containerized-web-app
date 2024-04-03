# Simple Web App

This is a simple web application that includes an API built with .NET and a front-end built with Node.js. The API uses a PostgreSQL database. All components are containerized using Docker.

## Prerequisites

- Docker
- Docker Compose

## Getting Started

To get the project up and running, follow these steps:

1. Build the Docker images:

    ```bash
    docker-compose build
    ```

2. Start the Docker containers:

    ```bash
    docker-compose up
    ```

To stop the running services, you can either press `CTRL+C` in the terminal where the services are running, or run the following command in another terminal:

```bash
docker-compose down
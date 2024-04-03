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
```

## Known Issues

Sure, here's the updated `README.md` file with the "Known Issues" section:

```markdown
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
```

## Testing the API

The API comes with a built-in Swagger UI, which provides a user-friendly interface to test the API endpoints. You can access the Swagger UI by navigating to the following URL in your web browser:

[http://localhost:8000/swagger/index.html#/default](http://localhost:8000/swagger/index.html#/default)

From the Swagger UI, you can view the details of each API endpoint, send requests to the endpoints, and view the responses from the server.

## Known Issues

- When running `docker-compose up` for the first time, the database service may not initialize in time for the API to make a call. 
  - Workaround: Stop the cluster by running `docker-compose down`, then run `docker-compose up` again.
# Docker Compose Setup for .NET 6 Training Application

This Docker Compose configuration provides a complete setup for running the .NET 6 Training application with PostgreSQL database and pgAdmin for database management.

## Services

### 1. PostgreSQL Database (`postgres`)
- **Image**: postgres:15-alpine
- **Port**: 5432
- **Database**: CmsAppDb
- **Username**: postgres
- **Password**: password

### 2. Training API (`training-api`)
- **Port**: 8080 (mapped to container port 80)
- **Environment**: Production
- **Health Check**: Available at `/health` endpoint

### 3. pgAdmin (`pgadmin`) - Optional
- **Port**: 8081
- **Email**: admin@example.com
- **Password**: admin

## Quick Start

### Prerequisites
- Docker and Docker Compose installed
- Portainer (if using Portainer for management)

### Using Docker Compose directly:

1. Navigate to the project root directory:
   ```bash
   cd e:\muraba\DotNet6Training
   ```

2. Build and start all services:
   ```bash
   docker-compose up -d --build
   ```

3. Access the application:
   - API: http://localhost:8080
   - Swagger UI: http://localhost:8080/swagger
   - pgAdmin: http://localhost:8081

### Using with Portainer:

1. **Upload the stack**:
   - Copy the `docker-compose.yml` content
   - In Portainer, go to "Stacks" → "Add stack"
   - Paste the Docker Compose content
   - Name your stack (e.g., "dotnet6-training")

2. **Deploy the stack**:
   - Click "Deploy the stack"
   - Wait for all services to start

3. **Access services**:
   - The application will be available at the configured ports
   - Monitor container health in Portainer dashboard

## Configuration

### Environment Variables
You can override the following environment variables:

- `ASPNETCORE_ENVIRONMENT`: Set to `Development`, `Staging`, or `Production`
- `ConnectionStrings__CmsConnectionString`: Database connection string
- Database credentials in the postgres service

### Volumes
- `postgres_data`: Persistent PostgreSQL data
- `pgadmin_data`: Persistent pgAdmin configuration
- `./Training/Files`: File upload directory (mapped to host)

### Networks
All services communicate through the `training-network` bridge network.

## Database Migration

The application should automatically apply Entity Framework migrations on startup. If you need to run migrations manually:

```bash
docker-compose exec training-api dotnet ef database update
```

## Monitoring and Logs

View container logs:
```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f training-api
docker-compose logs -f postgres
```

## Troubleshooting

1. **Database connection issues**:
   - Ensure PostgreSQL container is healthy
   - Check connection string configuration
   - Verify network connectivity between containers

2. **Application startup issues**:
   - Check application logs
   - Ensure all required environment variables are set
   - Verify the Dockerfile builds successfully

3. **Port conflicts**:
   - Modify port mappings in docker-compose.yml if needed
   - Ensure ports 8080, 8081, and 5432 are available

## Production Considerations

For production deployment:

1. **Security**:
   - Change default passwords
   - Use environment variables for sensitive data
   - Enable HTTPS/SSL
   - Configure proper firewall rules

2. **Performance**:
   - Adjust resource limits
   - Configure connection pooling
   - Set up monitoring and alerting

3. **Backup**:
   - Implement database backup strategy
   - Monitor disk space for volumes

## Stopping the Application

```bash
# Stop all services
docker-compose down

# Stop and remove volumes (WARNING: This will delete all data)
docker-compose down -v
```
# Build stage
FROM node:24-alpine AS build

WORKDIR /app

# Copy package files
COPY package.json ./

# Install dependencies
RUN npm i

# Copy source code
COPY . .

# Build the application
RUN npm run build

# Production stage
FROM nginx:alpine

# Copy built application from build stage
COPY --from=build /app/dist /usr/share/nginx/html

# Copy nginx configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Create SSL directories
RUN mkdir -p /etc/ssl/certs /etc/ssl/private

# Copy SSL certificates (make sure these files exist in your project root)
COPY ssl/nginx-selfsigned.crt /etc/ssl/certs/nginx-selfsigned.crt
COPY ssl/nginx-selfsigned.key /etc/ssl/private/nginx-selfsigned.key

# Set proper permissions for SSL certificates
RUN chmod 644 /etc/ssl/certs/nginx-selfsigned.crt && \
    chmod 600 /etc/ssl/private/nginx-selfsigned.key

# Expose ports 80 and 443
EXPOSE 80 443

# Start nginx
CMD ["nginx", "-g", "daemon off;"]
FROM postgres:15-alpine
COPY init-db.sql /docker-entrypoint-initdb.d/
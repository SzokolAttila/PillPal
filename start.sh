#!/bin/bash
if ! [ -d "./mssql-data" ]; then
    mkdir mssql-data
    sudo chown -R 10001:0 ./mssql-data
    sudo chmod -R 777 ./mssql-data
fi

cp .env-example .env

docker run --rm  -v "$(pwd)/PillPalWebApp:/app" --entrypoint npm idomi27/vue install

docker compose up -d
docker compose up PillPalAPI
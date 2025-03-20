#!/bin/bash
cp .env-example .env

docker run --rm  -v "$(pwd)/PillPalWebApp:/app" --entrypoint npm idomi27/vue install

docker compose up -d
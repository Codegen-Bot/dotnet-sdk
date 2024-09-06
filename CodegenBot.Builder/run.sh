#!/usr/bin/env bash

if [ ! -f /src/bot.json ]; then
  echo "[ERROR] [codegen.bot] bot.json does not exist in this directory"
  exit 1
fi

TIMESTAMP=$(date "+%Y-%m-%d %H:%M:%S")
echo "[$TIMESTAMP] [INFORMATION] [codegen.bot] Copying bot source"
cp /src /tmp-src -r
TIMESTAMP=$(date "+%Y-%m-%d %H:%M:%S")
echo "[$TIMESTAMP] [INFORMATION] [codegen.bot] Building"
dotnet build /tmp-src -c Release -r wasi-wasm
TIMESTAMP=$(date "+%Y-%m-%d %H:%M:%S")
echo "[$TIMESTAMP] [INFORMATION] [codegen.bot] Copying built wasm file to bin/Release/net8.0/wasi-wasm/AppBundle/"
cp /tmp-src/bin/Release/net8.0/wasi-wasm/AppBundle/*.wasm /src/bin/Release/net8.0/wasi-wasm/AppBundle/

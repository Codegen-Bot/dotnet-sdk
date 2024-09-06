#!/usr/bin/env bash

if [ ! -f /src/bot.json ]; then
  echo "[ERROR] [codegen.bot] bot.json does not exist in this directory"
  exit 1
fi

TIMESTAMP=$(date "+%Y-%m-%d %H:%M:%S")
echo "[$TIMESTAMP] [INFORMATION] [codegen.bot] Copying bot source into container so that the build doesn't produce intermediate artifacts that confuse IDEs. We're explicitly specifying wasi-wasm as the runtime identifier, so that it doesn't have to be specified in the csproj file, so IDEs that don't support wasi-wasm will still show Intellisense."
cp /src /tmp-src -r
TIMESTAMP=$(date "+%Y-%m-%d %H:%M:%S")
echo "[$TIMESTAMP] [INFORMATION] [codegen.bot] Building the bot"
dotnet build /tmp-src -c Release -r wasi-wasm
TIMESTAMP=$(date "+%Y-%m-%d %H:%M:%S")
echo "[$TIMESTAMP] [INFORMATION] [codegen.bot] Copying built wasm file to bin/Release/net8.0/wasi-wasm/AppBundle/, which should be outside the container"
if [ ! -d /src/bin/Release/net8.0/wasi-wasm/AppBundle/ ]; then
  mkdir -p /src/bin/Release/net8.0/wasi-wasm/AppBundle/
fi
cp /tmp-src/bin/Release/net8.0/wasi-wasm/AppBundle/*.wasm /src/bin/Release/net8.0/wasi-wasm/AppBundle/

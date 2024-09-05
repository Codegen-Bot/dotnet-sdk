#!/usr/bin/env bash

if [ ! -f /src/bot.json ]; then
  echo "[ERROR] [codegen.bot] bot.json does not exist in this directory"
  exit 1
fi

echo "[INFORMATION] [codegen.bot] Copying bot source"
cp /src /tmp-src -r
echo "[INFORMATION] [codegen.bot] Building"
dotnet build /tmp-src -c Release -r wasi-wasm -o /tmp-bin
echo "[INFORMATION] [codegen.bot] Copying built wasm file"
cp /tmp-bin/AppBundle/*.wasm /src

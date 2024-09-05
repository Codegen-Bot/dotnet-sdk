FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
RUN dotnet workload install wasi-experimental

WORKDIR /app

RUN wget https://github.com/WebAssembly/wasi-sdk/releases/download/wasi-sdk-24/wasi-sdk-24.0-x86_64-linux.tar.gz \
    && tar xvf wasi-sdk-24.0-x86_64-linux.tar.gz 

ENV WASI_SDK_PATH=/app/wasi-sdk-24.0-x86_64-linux

COPY run.sh /app/
RUN chmod +x /app/run.sh

ENTRYPOINT ["/app/run.sh"]

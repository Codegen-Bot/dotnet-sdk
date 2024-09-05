# codegenbot/dotnet-bot-builder

[CodegenBot](https://www.codegen.bot/) is a command line tool for developers to generate boilerplate.

This docker container is used to build bots that are written in C# (and possibly F# in the near future). To use this container, run this command:

```
# Linux and MacOS:
docker run -v $(pwd):/src codegenbot/dotnet-bot-builder:net8.0
# Windows Powershell:
docker run -v ${PWD}:/src codegenbot/dotnet-bot-builder:net8.0
```

Then you will have a `.wasm` file which can be run by CodegenBot to generate code.

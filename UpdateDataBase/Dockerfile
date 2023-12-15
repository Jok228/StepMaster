FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

COPY "bin/Release/net7.0" .

ENTRYPOINT [ "dotnet", "UpdateDateBase.dll" ]
FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
COPY bin/Release/net7.0 .
EXPOSE 80 

ENTRYPOINT [ "dotnet", "SignalRHubECS.dll" ]
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
COPY bin/Release/net6.0 .

ENTRYPOINT [ "dotnet", "SignalRHubECS.dll" ]
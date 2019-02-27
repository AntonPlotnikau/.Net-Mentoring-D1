FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 82
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["CoreWebUI/CoreWebUI.csproj", "CoreWebUI/"]
RUN dotnet restore "CoreWebUI/CoreWebUI.csproj"
COPY . .
WORKDIR "/src/CoreWebUI"
RUN dotnet build "CoreWebUI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CoreWebUI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CoreWebUI.dll"]
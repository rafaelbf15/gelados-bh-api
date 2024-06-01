FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["src/GeladosBH.API/GeladosBH.API.csproj", "src/GeladosBH.API/"]
RUN dotnet restore "src/GeladosBH.API/GeladosBH.API.csproj"
COPY . .
WORKDIR "/src/src/GeladosBH.API"
RUN dotnet build "GeladosBH.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GeladosBH.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GeladosBH.API.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CheckoutGateway/CheckoutGateway.csproj", "CheckoutGateway/"]
RUN dotnet restore "CheckoutGateway/CheckoutGateway.csproj"
COPY . .
WORKDIR "/src/CheckoutGateway"
RUN dotnet build "CheckoutGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CheckoutGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CheckoutGateway.dll"]

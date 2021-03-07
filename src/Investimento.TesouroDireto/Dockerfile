#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Investimento.TesouroDireto/Investimento.TesouroDireto.csproj", "Investimento.TesouroDireto/"]
COPY ["Investimento.TesouroDireto.Domain/Investimento.TesouroDireto.Domain.csproj", "Investimento.TesouroDireto.Domain/"]
COPY ["Investimento.TesouroDireto.Infrastructure/Investimento.TesouroDireto.Infrastructure.csproj", "Investimento.TesouroDireto.Infrastructure/"]
RUN dotnet restore "Investimento.TesouroDireto/Investimento.TesouroDireto.csproj"
COPY . .
WORKDIR "/src/Investimento.TesouroDireto"
RUN dotnet build "Investimento.TesouroDireto.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Investimento.TesouroDireto.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Investimento.TesouroDireto.dll"]
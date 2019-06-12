FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TReX.Discovery/Documents/TReX.Discovery.Documents.Worker/TReX.Discovery.Documents.Worker.csproj", "TReX.Discovery/Documents/TReX.Discovery.Documents.Worker/"]
COPY ["TReX.Discovery/Documents/TReX.Discovery.Documents.DependencyInjection/TReX.Discovery.Documents.DependencyInjection.csproj", "TReX.Discovery/Documents/TReX.Discovery.Documents.DependencyInjection/"]
COPY ["TReX.Kernel/TReX.Kernel.Raven/TReX.Kernel.Raven.csproj", "TReX.Kernel/TReX.Kernel.Raven/"]
COPY ["TReX.Kernel/TReX.Kernel.Shared/TReX.Kernel.Shared.csproj", "TReX.Kernel/TReX.Kernel.Shared/"]
COPY ["TReX.Kernel/TReX.Kernel.Utilities/TReX.Kernel.Utilities.csproj", "TReX.Kernel/TReX.Kernel.Utilities/"]
COPY ["TReX.Discovery/Documents/TReX.Discovery.Documents.Archeology/TReX.Discovery.Documents.Archeology.csproj", "TReX.Discovery/Documents/TReX.Discovery.Documents.Archeology/"]
COPY ["TReX.Discovery/Documents/TReX.Discovery.Documents.Domain/TReX.Discovery.Documents.Domain.csproj", "TReX.Discovery/Documents/TReX.Discovery.Documents.Domain/"]
COPY ["TReX.Discovery/Shared/TReX.Discovery.Shared.Domain/TReX.Discovery.Shared.Domain.csproj", "TReX.Discovery/Shared/TReX.Discovery.Shared.Domain/"]
COPY ["TReX.Discovery/Shared/TReX.Discovery.Shared.Archeology/TReX.Discovery.Shared.Archeology.csproj", "TReX.Discovery/Shared/TReX.Discovery.Shared.Archeology/"]
COPY ["TReX.Discovery/Shared/TReX.Discovery.Shared.Business/TReX.Discovery.Shared.Business.csproj", "TReX.Discovery/Shared/TReX.Discovery.Shared.Business/"]
COPY ["TReX.Discovery/Shared/TReX.Discovery.Shared.Integration/TReX.Discovery.Shared.Integration.csproj", "TReX.Discovery/Shared/TReX.Discovery.Shared.Integration/"]
RUN dotnet restore "TReX.Discovery/Documents/TReX.Discovery.Documents.Worker/TReX.Discovery.Documents.Worker.csproj"
COPY . .
WORKDIR "/src/TReX.Discovery/Documents/TReX.Discovery.Documents.Worker"
RUN dotnet build "TReX.Discovery.Documents.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TReX.Discovery.Documents.Worker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TReX.Discovery.Documents.Worker.dll"]
FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TReX.Discovery/Media/TReX.Discovery.Media.Worker/TReX.Discovery.Media.Worker.csproj", "TReX.Discovery/Media/TReX.Discovery.Media.Worker/"]
COPY ["TReX.Discovery/Media/TReX.Discovery.Media.Business/TReX.Discovery.Media.Business.csproj", "TReX.Discovery/Media/TReX.Discovery.Media.Business/"]
COPY ["TReX.Discovery/Shared/TReX.Discovery.Shared.Business/TReX.Discovery.Shared.Business.csproj", "TReX.Discovery/Shared/TReX.Discovery.Shared.Business/"]
COPY ["TReX.Discovery/Media/TReX.Discovery.Media.Domain/TReX.Discovery.Media.Domain.csproj", "TReX.Discovery/Media/TReX.Discovery.Media.Domain/"]
COPY ["TReX.Kernel/TReX.Kernel.Shared/TReX.Kernel.Shared.csproj", "TReX.Kernel/TReX.Kernel.Shared/"]
COPY ["TReX.Discovery/Shared/TReX.Discovery.Shared.Domain/TReX.Discovery.Shared.Domain.csproj", "TReX.Discovery/Shared/TReX.Discovery.Shared.Domain/"]
COPY ["TReX.Discovery/Media/TReX.Discovery.Media.DependencyInjection/TReX.Discovery.Media.DependencyInjection.csproj", "TReX.Discovery/Media/TReX.Discovery.Media.DependencyInjection/"]
COPY ["TReX.Kernel/TReX.Kernel.Raven/TReX.Kernel.Raven.csproj", "TReX.Kernel/TReX.Kernel.Raven/"]
COPY ["TReX.Discovery/Media/TReX.Discovery.Media.Archeology/TReX.Discovery.Media.Archeology.csproj", "TReX.Discovery/Media/TReX.Discovery.Media.Archeology/"]
COPY ["TReX.Discovery/Shared/TReX.Discovery.Shared.Archeology/TReX.Discovery.Shared.Archeology.csproj", "TReX.Discovery/Shared/TReX.Discovery.Shared.Archeology/"]
COPY ["TReX.Kernel/TReX.Kernel.Utilities/TReX.Kernel.Utilities.csproj", "TReX.Kernel/TReX.Kernel.Utilities/"]
COPY ["TReX.Discovery/Shared/TReX.Discovery.Shared.Integration/TReX.Discovery.Shared.Integration.csproj", "TReX.Discovery/Shared/TReX.Discovery.Shared.Integration/"]
RUN dotnet restore "TReX.Discovery/Media/TReX.Discovery.Media.Worker/TReX.Discovery.Media.Worker.csproj"
COPY . .
WORKDIR "/src/TReX.Discovery/Media/TReX.Discovery.Media.Worker"
RUN dotnet build "TReX.Discovery.Media.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TReX.Discovery.Media.Worker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TReX.Discovery.Media.Worker.dll"]
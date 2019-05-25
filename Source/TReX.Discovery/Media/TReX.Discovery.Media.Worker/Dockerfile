FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Media/TReX.Discovery.Media.Worker/TReX.Discovery.Media.Worker.csproj", "Media/TReX.Discovery.Media.Worker/"]
COPY ["Media/TReX.Discovery.Media.Business/TReX.Discovery.Media.Business.csproj", "Media/TReX.Discovery.Media.Business/"]
COPY ["Shared/TReX.Discovery.Shared.Business/TReX.Discovery.Shared.Business.csproj", "Shared/TReX.Discovery.Shared.Business/"]
COPY ["Media/TReX.Discovery.Media.Domain/TReX.Discovery.Media.Domain.csproj", "Media/TReX.Discovery.Media.Domain/"]
COPY ["../TReX.Kernel/TReX.Kernel.Shared/TReX.Kernel.Shared.csproj", "../TReX.Kernel/TReX.Kernel.Shared/"]
COPY ["Media/TReX.Discovery.Media.DependencyInjection/TReX.Discovery.Media.DependencyInjection.csproj", "Media/TReX.Discovery.Media.DependencyInjection/"]
COPY ["../TReX.Kernel/TReX.Kernel.Raven/TReX.Kernel.Raven.csproj", "../TReX.Kernel/TReX.Kernel.Raven/"]
COPY ["Media/TReX.Discovery.Media.Archeology/TReX.Discovery.Media.Archeology.csproj", "Media/TReX.Discovery.Media.Archeology/"]
COPY ["../TReX.Kernel/TReX.Kernel.Utilities/TReX.Kernel.Utilities.csproj", "../TReX.Kernel/TReX.Kernel.Utilities/"]
COPY ["Shared/TReX.Discovery.Shared.Domain/TReX.Discovery.Shared.Domain.csproj", "Shared/TReX.Discovery.Shared.Domain/"]
RUN dotnet restore "Media/TReX.Discovery.Media.Worker/TReX.Discovery.Media.Worker.csproj"
COPY . .
WORKDIR "/src/Media/TReX.Discovery.Media.Worker"
RUN dotnet build "TReX.Discovery.Media.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TReX.Discovery.Media.Worker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TReX.Discovery.Media.Worker.dll"]

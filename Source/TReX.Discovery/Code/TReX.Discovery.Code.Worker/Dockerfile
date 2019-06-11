FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Code/TReX.Discovery.Code.Worker/TReX.Discovery.Code.Worker.csproj", "Code/TReX.Discovery.Code.Worker/"]
COPY ["../TReX.Kernel/TReX.Kernel.Shared/TReX.Kernel.Shared.csproj", "../TReX.Kernel/TReX.Kernel.Shared/"]
COPY ["Code/TReX.Discovery.Code.DependencyInjection/TReX.Discovery.Code.DependencyInjection.csproj", "Code/TReX.Discovery.Code.DependencyInjection/"]
COPY ["Code/TReX.Discovery.Code.Business/TReX.Discovery.Code.Business.csproj", "Code/TReX.Discovery.Code.Business/"]
COPY ["Shared/TReX.Discovery.Shared.Business/TReX.Discovery.Shared.Business.csproj", "Shared/TReX.Discovery.Shared.Business/"]
COPY ["../TReX.Kernel/TReX.Kernel.Raven/TReX.Kernel.Raven.csproj", "../TReX.Kernel/TReX.Kernel.Raven/"]
COPY ["../TReX.Kernel/TReX.Kernel.Utilities/TReX.Kernel.Utilities.csproj", "../TReX.Kernel/TReX.Kernel.Utilities/"]
COPY ["Code/TReX.Discovery.Code.Archeology/TReX.Discovery.Code.Archeology.csproj", "Code/TReX.Discovery.Code.Archeology/"]
COPY ["Code/TReX.Discovery.Code.Domain/TReX.Discovery.Code.Domain.csproj", "Code/TReX.Discovery.Code.Domain/"]
COPY ["Shared/TReX.Discovery.Shared.Domain/TReX.Discovery.Shared.Domain.csproj", "Shared/TReX.Discovery.Shared.Domain/"]
COPY ["Shared/TReX.Discovery.Shared.Archeology/TReX.Discovery.Shared.Archeology.csproj", "Shared/TReX.Discovery.Shared.Archeology/"]
COPY ["Shared/TReX.Discovery.Shared.Integration/TReX.Discovery.Shared.Integration.csproj", "Shared/TReX.Discovery.Shared.Integration/"]
RUN dotnet restore "Code/TReX.Discovery.Code.Worker/TReX.Discovery.Code.Worker.csproj"
COPY . .
WORKDIR "/src/Code/TReX.Discovery.Code.Worker"
RUN dotnet build "TReX.Discovery.Code.Worker.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TReX.Discovery.Code.Worker.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TReX.Discovery.Code.Worker.dll"]

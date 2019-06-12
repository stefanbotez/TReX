FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TReX.App/TReX.App.Museum/TReX.App.Museum.csproj", "TReX.App/TReX.App.Museum/"]
COPY ["TReX.Kernel/TReX.Kernel.Raven/TReX.Kernel.Raven.csproj", "TReX.Kernel/TReX.Kernel.Raven/"]
COPY ["TReX.Kernel/TReX.Kernel.Shared/TReX.Kernel.Shared.csproj", "TReX.Kernel/TReX.Kernel.Shared/"]
COPY ["TReX.App/TReX.App.Domain/TReX.App.Domain.csproj", "TReX.App/TReX.App.Domain/"]
COPY ["TReX.Kernel/TReX.Kernel.Utilities/TReX.Kernel.Utilities.csproj", "TReX.Kernel/TReX.Kernel.Utilities/"]
RUN dotnet restore "TReX.App/TReX.App.Museum/TReX.App.Museum.csproj"
COPY . .
WORKDIR "/src/TReX.App/TReX.App.Museum"
RUN dotnet build "TReX.App.Museum.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TReX.App.Museum.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TReX.App.Museum.dll"]
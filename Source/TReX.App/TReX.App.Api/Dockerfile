FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TReX.App.Api/TReX.App.Api.csproj", "TReX.App.Api/"]
COPY ["TReX.App.Infrastructure/TReX.App.Infrastructure.csproj", "TReX.App.Infrastructure/"]
COPY ["../TReX.Kernel/TReX.Kernel.Raven/TReX.Kernel.Raven.csproj", "../TReX.Kernel/TReX.Kernel.Raven/"]
COPY ["../TReX.Kernel/TReX.Kernel.Shared/TReX.Kernel.Shared.csproj", "../TReX.Kernel/TReX.Kernel.Shared/"]
COPY ["../TReX.Kernel/TReX.Kernel.Utilities/TReX.Kernel.Utilities.csproj", "../TReX.Kernel/TReX.Kernel.Utilities/"]
COPY ["TReX.App.Business/TReX.App.Business.csproj", "TReX.App.Business/"]
COPY ["TReX.App.Domain/TReX.App.Domain.csproj", "TReX.App.Domain/"]
RUN dotnet restore "TReX.App.Api/TReX.App.Api.csproj"
COPY . .
WORKDIR "/src/TReX.App.Api"
RUN dotnet build "TReX.App.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TReX.App.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TReX.App.Api.dll"]

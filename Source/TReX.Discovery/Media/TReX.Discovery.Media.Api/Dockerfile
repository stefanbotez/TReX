#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.2-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["TReX.Discovery.Media.Api/TReX.Discovery.Media.Api.csproj", "TReX.Discovery.Media.Api/"]
RUN dotnet restore "TReX.Discovery.Media.Api/TReX.Discovery.Media.Api.csproj"
COPY . .
WORKDIR "/src/TReX.Discovery.Media.Api"
RUN dotnet build "TReX.Discovery.Media.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TReX.Discovery.Media.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TReX.Discovery.Media.Api.dll"]
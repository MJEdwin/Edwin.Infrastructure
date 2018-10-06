FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Edwin.TestApi/Edwin.TestApi.csproj", "Edwin.TestApi/"]
COPY ["Edwin.Infrastructure.EntityFramework/Edwin.Infrastructure.EntityFramework.csproj", "Edwin.Infrastructure.EntityFramework/"]
COPY ["Edwin.Infrastructure.Domain/Edwin.Infrastructure.Domain.csproj", "Edwin.Infrastructure.Domain/"]
COPY ["Edwin.Infrastructure.Query/Edwin.Infrastructure.Query.csproj", "Edwin.Infrastructure.Query/"]
COPY ["Edwin.Infrastructure.Core/Edwin.Infrastructure.Core.csproj", "Edwin.Infrastructure.Core/"]
COPY ["Edwin.Infrastructure.AutoMapper/Edwin.Infrastructure.AutoMapper.csproj", "Edwin.Infrastructure.AutoMapper/"]
COPY ["Edwin.Infrastructure.Reflection/Edwin.Infrastructure.Reflection.csproj", "Edwin.Infrastructure.Reflection/"]
RUN dotnet restore "Edwin.TestApi/Edwin.TestApi.csproj"
COPY . .
WORKDIR "/src/Edwin.TestApi"
RUN dotnet build "Edwin.TestApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Edwin.TestApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Edwin.TestApi.dll"]
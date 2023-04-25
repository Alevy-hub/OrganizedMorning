# U¿yj oficjalnego obrazu .NET Core 7.0 SDK jako obraz bazowy
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Skopiuj plik projektu i odtwórz zale¿noœci
COPY *.csproj ./
RUN dotnet restore

# Skopiuj pozosta³e pliki Ÿród³owe i skompiluj aplikacjê
COPY . ./
RUN dotnet publish -c Release -o out

# U¿yj oficjalnego obrazu .NET Core 7.0 Runtime jako obraz bazowy
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .

# Ustaw zmienn¹ œrodowiskow¹ z ³añcuchem po³¹czenia do bazy danych
ENV ConnectionStrings__DefaultConnection "Server=mws02.mikr.us;Port=50005;Database=organizedmorningdb;Uid=root;Pwd=NeFezZxsUW;"

# Uruchom aplikacjê
ENTRYPOINT ["dotnet", "OrganizedMorning.dll"]

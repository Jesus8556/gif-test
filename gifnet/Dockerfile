# Utiliza la imagen oficial de .NET SDK como base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copia el archivo csproj y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia el resto del código y compila la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Configura la imagen de producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Exponer el puerto 5214
EXPOSE 5214

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "gifnet.dll"]
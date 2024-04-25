# PersonAPI .NET

## Configuración

### Requisitos Previos

Antes de comenzar, asegúrate de tener instalados los siguientes componentes:

- SQL Server 2019 Express
- SQL Server Management Studio 20
- Visual Studio Community 2022

### Base de Datos

Crea una coexión de base de datos
- Usa el nombre del servidor que incluye la palabra "SQLEXPRESS"
- Selecciona la opción de autenticación de Windows (Windows Authentication)
- Activa la opción "Trust server certificate" antes de hacer clic en "Conectar" para establecer la conexión. 
- Usa los archivos de disponibles en la carpeta db para crear la base de datos.

## Pasos para Configurar el Ambiente

1. Clonar este repositorio.
2. Abrir proyecto y seleccionar el archivo personapi-dotnet.sln
3. Activar la vista del Explorador de objetos de SQL Server.

### Configuración de Conexión

1. Agrega y prueba la conexión de tipo "local express" en Visual Studio.
2. Instala los siguientes paquetes NuGet en el proyecto:
   - Microsoft.EntityFrameworkCore
   - Microsoft.EntityFrameworkCore.SqlServer
   - Microsoft.EntityFrameworkCore.Tools

## Compilación

Para compilar el proyecto, siga estos pasos:

1. Abre la solución en Visual Studio Community 2022.
2. Asegurate de que el proyecto esté configurado correctamente, con las referencias y paquetes NuGet necesarios.
3. Compila el proyecto utilizando la opción "Compilar" en Visual Studio.

## Despliegue

### Despliegue Local

Para ejecutar la aplicación localmente, sigue estos pasos:
- Asegúrate de tener una instancia de SQL Server en funcionamiento.
- Verifica que la cadena de conexión en el archivo appsettings.json y PersonadDbContext.cs esté configurada adecuadamente para tu entorno local.
- Inicia la aplicación desde Visual Studio utilizando la opción "Depurar" (F5).

Para otras opciones de despliegue, consulta la documentación específica de tu plataforma. Recuerda ajustar las variables de entorno y configuraciones específicas según tus necesidades.

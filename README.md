## Integrantes del grupo
- **Alumno 1:** Fernando Paul Alonso Iglesias – Legajo 58282
- **Alumno 2:** Nombre Apellido – Legajo 67890  
- **Alumno 3:** Nombre Apellido – Legajo 54321  

## Endpoints implementados

### Productos (/api/products)
- POST → Crear producto
- GET → Listar productos
- GET /{id} → Obtener producto por ID
- PUT /{id} → Actualizar producto
- PATCH /{id} → Desactivar producto
- Clientes (/api/customers)
- POST → Crear cliente
- GET → Listar clientes
- Order (/api/orders)
- POST → Crear orden
- GET → Listar órdenes

## Configurar y ejecutar el proyecto localmente
### Clonar el repositorio
 - Abre una terminal y ejecuta:
 - git clone https://github.com/Samira-2513/Dsw2025Tpi.git
 - cd Dsw2025Tpi
### Abrir la solución en Visual Studio
 - Dentro de la carpeta Dsw2025Tpi, localiza el archivo Dsw2025Tpi.sln.
 - Haz doble clic para cargar la solución en Visual Studio.
### Crear y aplicar la migración
 - Abre la Consola del Administrador de Paquetes en Visual Studio (Tools → NuGet Package Manager → Package Manager Console).
 - Asegúrate de que el Default project sea tu proyecto de datos (donde está el DbContext).
 - Ejecuta:
 - Add-Migration InitialCreate
 - Update-Database
 - Esto generará las tablas y esquemas en tu base de datos según tus entidades y OnModelCreating.
### Ejecutar la API
 - En Visual Studio, selecciona el proyecto Dsw2025Tpi.Api como proyecto de inicio.
 - Presiona F5 o haz clic en el botón IIS Express/Play para compilar y levantar la API.


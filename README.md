# Documentación - Gestor de Tareas (TO-DO)

## Resumen del Proyecto

El proyecto trata sobre un gestor de tareas, el cual es una herramienta integral que combina la gestión de usuarios, roles y tableros para ofrecer una solución completa y flexible para la coordinación de actividades en equipos de trabajo, promoviendo así la productividad y el éxito de proyectos.

## Tecnologías Utilizadas

- **Lenguaje de Programación:** C#
- **Framework:** ASP.NET MVC
- **Base de Datos:** SQLite

## Funcionalidades Principales

1. Creacion, modificacion, edicion y eliminacion de usuarios
2. Creacion, modificacion, edicion y eliminacion de tareas
3. Creacion, modificacion, edicion y eliminacion de tableros
4. Autenticacion de usuarios
5. Manejo de errores
6. Control de viewModels

## Descripcion de Proyecto

# Base de Datos

El proyecto está basado en una base de datos SQLite llamada "Kanban", en la que hemos definido tres entidades: Usuario, Tarea y Tablero. Estas entidades están conectadas entre sí: la entidad Tarea tiene una clave externa que referencia la tabla Usuario, y la tabla Tablero tiene relaciones con ambas entidades, con claves foráneas para Usuario y Tarea.

En cuanto a la gestión de la integridad referencial, hemos implementado acciones de eliminación y modificación en cascada para todas las entidades. Esto significa que cuando se elimina o modifica un registro en la tabla principal (Usuario o Tarea), las relaciones asociadas en las otras tablas (Tarea o Tablero) se actualizan o eliminan automáticamente, según corresponda.

# Patrones de diseño

- En la implementación del proyecto, hemos adoptado el patrón de diseño Repository para nuestros repositorios de Usuario, Tarea y Tablero. El patrón Repository es un diseño que se utiliza comúnmente en el desarrollo de software para separar la lógica de acceso a los datos de la lógica de negocio. En nuestro contexto, los repositorios actúan como abstracciones sobre la capa de acceso a datos, proporcionando una interfaz común para interactuar con los datos subyacentes, lo que nos permite centralizar la lógica de acceso a la base de datos y facilitar las operaciones de lectura, escritura y eliminación.

- Además, hemos utilizado el patrón de diseño Inyección de Dependencia (DI). La inyección de dependencia es un principio de diseño en el que los componentes de una aplicación no crean directamente las dependencias en las que confían, sino que se las proporcionan desde el exterior (generalmente a través de la configuración o mediante un contenedor de inversión de control). Este enfoque promueve la modularidad y la reutilización del código al reducir el acoplamiento entre los componentes y facilitar la prueba unitaria y el intercambio de implementaciones. En resumen, la inyección de dependencia nos permite gestionar las dependencias de manera más flexible y desacoplada, lo que mejora la mantenibilidad y la escalabilidad de nuestro código.

# Diseño

Para el diseño de las diferentes vistas de nuestro proyecto hicimos uso de boostrap, ayudandonos de la documentacion en web, complementando esto con el uso de CSS para una adaptacion más personal del proyecto.

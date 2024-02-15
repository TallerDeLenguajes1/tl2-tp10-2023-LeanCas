# Documentación - Gestor de Tareas

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

# Arquitectura del Proyecto

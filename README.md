# Sistema Digital de Votación Estudiantil (SDVE)

Aplicación de escritorio desarrollada en C# con Windows Forms para organizar procesos de votación estudiantil. El sistema permite identificar alumnos mediante un padrón, consultar elecciones disponibles, registrar votos, evitar votos duplicados, mostrar resultados, visualizar gráficas de participación y exportar información en archivos CSV.

## Prototipo del sistema

El SDVE funciona como una aplicación local para Windows. La información se conserva mediante archivos incluidos con el proyecto y archivos generados durante el uso del sistema, por lo que las funciones principales no dependen de una página web, servidor externo o conexión a Internet.

El flujo principal del sistema es:

**Bienvenida -> Identificación -> Selección de elecciones -> Elección de candidatos -> Confirmación -> Registro del voto -> Folio de confirmación -> Regreso al inicio**

El sistema contempla tres procesos de elección:

- Sociedad de Alumnos.
- Consejo Universitario.
- Consejo de Representantes.

También permite usar la opción **"Otro"** para registrar un candidato que no aparezca en la lista oficial.

## Estructura del repositorio

La solución está organizada como un proyecto de Visual Studio en C# e incluye componentes de interfaz, modelos, servicios, datos, recursos y fuentes.

```text
SDVE-SistemaVotacion/
├── README.md
├── .gitignore
├── SDVE-SistemaVotacion/
│   ├── SDVE-SistemaVotacion.csproj
│   ├── SDVE-SistemaVotacion.slnx
│   ├── Datos/
│   │   └── padron.csv
│   ├── fonts/
│   │   ├── BricolageGrotesque_24pt-Bold.ttf
│   │   ├── BricolageGrotesque_24pt-Regular.ttf
│   │   ├── InstrumentSans-Bold.ttf
│   │   └── InstrumentSans-Regular.ttf
│   ├── Models/
│   │   ├── Alumno.cs
│   │   ├── Candidato.cs
│   │   ├── ResultadoReporte.cs
│   │   ├── TipoConvocatoria.cs
│   │   └── Voto.cs
│   ├── Properties/
│   │   ├── Resources.Designer.cs
│   │   └── Resources.resx
│   ├── Resources/
│   │   └── flecha_0A1931.png
│   ├── Services/
│   │   ├── AlmacenPadron.cs
│   │   ├── AlmacenVotos.cs
│   │   ├── CalculoParticipacion.cs
│   │   ├── CalculosResultados.cs
│   │   ├── Exportador.cs
│   │   ├── LectorCsv.cs
│   │   ├── ListaOficial.cs
│   │   └── Texto.cs
│   ├── Form1.cs
│   ├── HelperFuentes.cs
│   ├── HelperUI.cs
│   ├── icono_sdve.ico
│   ├── Program.cs
│   ├── UcBienvenida.cs
│   ├── UcConfirmacion.cs
│   ├── UcContenedor.cs
│   ├── UcExportar.cs
│   ├── UcGraficas.cs
│   ├── UcIdentificacion.cs
│   ├── UcPapeleta.cs
│   ├── UcResultados.cs
│   ├── UcVotarConsejoDeRepresentantes.cs
│   ├── UcVotarConsejoUniversitario.cs
│   ├── UcVotarSociedadDeAlumnos.cs
│   └── UcVotoRegistrado.cs
└── Miniproyecto-01/
    ├── Archivo_del_respaldo_del_proyecto/
    ├── Documentación de usuario y técnica del sistema/
    ├── Ejecutable/
    ├── Reporte de participación individual/
    └── video de presentacion del sistema/
```

## Documentación y entrega incluida

La carpeta `Miniproyecto-01` contiene los materiales de entrega del proyecto:

- Respaldo comprimido del proyecto fuente.
- Documentación de usuario y técnica del sistema.
- Carpeta `Ejecutable` con la versión publicada de la aplicación.
- Reporte de participación individual del equipo.
- Video de presentación del sistema.

La documentación describe el funcionamiento general del SDVE, el registro de votos, el control para evitar votos duplicados, el uso de la opción "Otro", la consulta de resultados, las gráficas de participación, la exportación de datos y los requisitos de ejecución.

## Tecnologías utilizadas

- C#
- Windows Forms
- .NET
- Visual Studio
- Guna.UI2.WinForms
- ScottPlot.WinForms
- Svg
- WinForms.DataVisualization
- Archivos CSV para padrón y exportación.
- Archivos JSON para persistencia local de votos y estado del padrón.

## Ejecutar desde Visual Studio

1. Instalar Visual Studio con soporte para desarrollo de aplicaciones de escritorio con .NET.
2. Clonar o descargar este repositorio.
3. Abrir `SDVE-SistemaVotacion/SDVE-SistemaVotacion.slnx` en Visual Studio.
4. Verificar que el archivo `SDVE-SistemaVotacion/Datos/padron.csv` esté disponible.
5. Restaurar las dependencias del proyecto si Visual Studio lo solicita.
6. Seleccionar el proyecto de Windows Forms como proyecto de inicio.
7. Compilar la solución.
8. Ejecutar el sistema con **F5** o con el botón **Iniciar**.

Durante la ejecución, el sistema usa archivos locales para cargar el padrón y conservar votos. El equipo debe contar con permisos suficientes para leer y escribir archivos en la carpeta utilizada por la aplicación.

## Ejecutar desde la carpeta publicada

También se incluye una versión publicada en:

```text
Miniproyecto-01/Ejecutable/
```

Para usarla:

1. Entrar a la carpeta `Miniproyecto-01/Ejecutable`.
2. Verificar que se conserve la carpeta `Datos` junto al ejecutable.
3. Ejecutar `SDVE-SistemaVotacion.exe`.

## Archivos de datos

El sistema utiliza `padron.csv` como base de alumnos registrados. Durante el uso puede generar archivos locales para conservar el estado del padrón, los votos registrados y los resultados exportados.

Los archivos CSV de resultados se generan desde la función de exportación del sistema.

## Derechos de autor

Proyecto universitario **Sistema Digital de Votación Estudiantil (SDVE)**.

El código, documentación y materiales incluidos se destinan al uso académico correspondiente. Cualquier distribución o modificación deberá respetar las condiciones establecidas por el equipo y la institución.

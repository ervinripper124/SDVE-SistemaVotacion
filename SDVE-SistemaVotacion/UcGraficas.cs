using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Guna.UI2.WinForms;
using SDVE_SistemaVotacion.Models;
using SDVE_SistemaVotacion.Services;

namespace SDVE_SistemaVotacion
{
    public partial class UcGraficas : UserControl
    {
        // Paleta UAA: azul marino, blanco, poco rojo
        private static readonly Color Azul = Color.FromArgb(10, 42, 102);
        private static readonly Color AzulMedio = Color.FromArgb(52, 105, 184);
        private static readonly Color Celeste = Color.FromArgb(143, 176, 225);
        private static readonly Color Rojo = Color.FromArgb(200, 16, 46);
        private static readonly Color Gris = Color.FromArgb(176, 184, 201);
        private static readonly Color Fondo = Color.FromArgb(244, 246, 251);
        private static readonly Color Borde = Color.FromArgb(222, 227, 238);
        private static readonly Color TextoSuave = Color.FromArgb(107, 115, 133);

        private static readonly Agrupacion[] Agrupaciones =
            { Agrupacion.Grupo, Agrupacion.Carrera, Agrupacion.CentroUniversitario };

        private readonly Guna2ComboBox cmbGrafica = new Guna2ComboBox();
        private readonly Guna2ComboBox cmbConvocatoria = new Guna2ComboBox();
        private readonly Guna2ComboBox cmbAgrupar = new Guna2ComboBox();

        private bool cargando = true;   // evita redibujar mientras se llenan los combos

        public UcGraficas()
        {
            InitializeComponent();
            PrepararControles();
            this.Load += (s, e) => { cargando = false; Dibujar(); };
        }

        private void PrepararControles()
        {
            int ancho = panelGrafica.Width;
            int alto = panelGrafica.Height;

            // 1. Fondo
            panelGrafica.FillColor = Fondo;
            panelGrafica.BackColor = Fondo;

            // 2. Encabezado azul con línea roja
            var encabezado = new Guna2Panel
            {
                FillColor = Azul,
                BackColor = Azul,
                Location = new Point(0, 0),
                Size = new Size(ancho, 60),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            var titulo = new Label
            {
                Text = "Gráficas de votación",
                AutoSize = true,
                Font = HelperFuentes.BricolageBold(15f),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Location = new Point(20, 15)
            };
            var lineaRoja = new Panel
            {
                BackColor = Rojo,
                Dock = DockStyle.Bottom,
                Height = 3
            };
            encabezado.Controls.Add(titulo);
            encabezado.Controls.Add(lineaRoja);
            panelGrafica.Controls.Add(encabezado);

            // 3. Combos
            ConfigurarCombo(cmbGrafica, 165);
            cmbGrafica.Items.AddRange(new object[]
            {
                "1. Votos por candidato",
                "2. Participación y abstención",
                "3. Participación por grupo"
            });

            ConfigurarCombo(cmbConvocatoria, 165);
            foreach (var c in CalculoResultados.Orden) cmbConvocatoria.Items.Add(c.ObtenerNombreMostrar());

            ConfigurarCombo(cmbAgrupar, 125);
            foreach (var a in Agrupaciones) cmbAgrupar.Items.Add(a.NombreVisible());

            cmbGrafica.SelectedIndex = 0;
            cmbConvocatoria.SelectedIndex = 0;
            cmbAgrupar.SelectedIndex = 0;
            cmbAgrupar.Enabled = false;

            cmbGrafica.SelectedIndexChanged += (s, e) =>
            {
                cmbAgrupar.Enabled = cmbGrafica.SelectedIndex == 2;   // solo la gráfica 3 agrupa
                if (!cargando) Dibujar();
            };
            cmbConvocatoria.SelectedIndexChanged += (s, e) => { if (!cargando) Dibujar(); };
            cmbAgrupar.SelectedIndexChanged += (s, e) => { if (!cargando) Dibujar(); };

            // 4. Tarjeta de filtros
            var tarjetaFiltros = new Guna2Panel
            {
                FillColor = Color.White,
                BackColor = Fondo,
                BorderColor = Borde,
                BorderThickness = 1,
                BorderRadius = 12,
                Location = new Point(20, 72),
                Size = new Size(ancho - 40, 56),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            var barra = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 8, 6, 0),
                BackColor = Color.Transparent,
                WrapContents = false,
                AutoScroll = false
            };
            barra.Controls.Add(Etiqueta("Gráfica"));
            barra.Controls.Add(cmbGrafica);
            barra.Controls.Add(Etiqueta("Convocatoria"));
            barra.Controls.Add(cmbConvocatoria);
            barra.Controls.Add(Etiqueta("Agrupar por"));
            barra.Controls.Add(cmbAgrupar);
            tarjetaFiltros.Controls.Add(barra);
            panelGrafica.Controls.Add(tarjetaFiltros);

            // 5. Tarjeta de la gráfica
            var tarjetaChart = new Guna2Panel
            {
                FillColor = Color.White,
                BackColor = Fondo,
                BorderColor = Borde,
                BorderThickness = 1,
                BorderRadius = 12,
                Padding = new Padding(10),
                Location = new Point(20, 140),
                Size = new Size(ancho - 40, alto - 140 - 16),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            panelGrafica.Controls.Add(tarjetaChart);
            tarjetaChart.Controls.Add(chartResultados);
            chartResultados.Dock = DockStyle.Fill;
            chartResultados.BackColor = Color.White;

            chartResultados.Palette = ChartColorPalette.None;
            chartResultados.PaletteCustomColors = new[] { Azul, Rojo, AzulMedio, Celeste, Gris };
        }

        private static void ConfigurarCombo(Guna2ComboBox cmb, int ancho)
        {
            cmb.DropDownWidth = 240;
            cmb.DrawMode = DrawMode.OwnerDrawFixed;
            cmb.ItemHeight = 28;
            cmb.Size = new Size(ancho, 36);
            cmb.Font = HelperFuentes.InstrumentRegular(10f);
            cmb.FillColor = Color.White;
            cmb.ForeColor = Azul;
            cmb.BorderColor = Borde;
            cmb.BorderThickness = 1;
            cmb.BorderRadius = 8;
            cmb.BackColor = Color.Transparent;
            cmb.FocusedColor = Azul;
            cmb.FocusedState.BorderColor = Azul;
            cmb.HoverState.BorderColor = Azul;
            cmb.Margin = new Padding(0, 0, 10, 0);
        }

        private static Label Etiqueta(string texto) =>
            new Label
            {
                Text = texto,
                AutoSize = true,
                Margin = new Padding(0, 9, 6, 0),
                BackColor = Color.Transparent,
                ForeColor = TextoSuave,
                Font = HelperFuentes.BricolageBold(10f)
            };

        /// <summary>Lee los votos y el padrón, y dibuja la gráfica elegida.</summary>
        private void Dibujar()
        {
            List<Voto> votos;
            List<Alumno> padron;
            try
            {
                votos = AlmacenVotos.ObtenerTodos().ToList();
                padron = AlmacenPadron.Alumnos.ToList();
            }
            catch (Exception ex) when (ex is IOException || ex is InvalidDataException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show("No se pudieron leer los datos: " + ex.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TipoConvocatoria convocatoria = CalculoResultados.Orden[Math.Max(0, cmbConvocatoria.SelectedIndex)];

            switch (cmbGrafica.SelectedIndex)
            {
                case 1: GraficaParticipacion(votos, padron, convocatoria); break;
                case 2:
                    GraficaPorGrupo(votos, padron, convocatoria,
                                        Agrupaciones[Math.Max(0, cmbAgrupar.SelectedIndex)]); break;
                default: GraficaCandidatos(votos, convocatoria); break;
            }
        }

        // ---------- Gráfica 1: votos por candidato (no necesita el padrón) ----------
        private void GraficaCandidatos(List<Voto> votos, TipoConvocatoria convocatoria)
        {
            var filas = CalculoResultados.Calcular(votos, ListaOficial.Candidatos).Filas
                                    .Where(f => f.Convocatoria == convocatoria && f.Votos > 0).ToList();

            if (filas.Count == 0) { SinDatos("Aún no hay votos en " + convocatoria.ObtenerNombreMostrar()); return; }

            Preparar("Votos por candidato - " + convocatoria.ObtenerNombreMostrar(), true);
            var serie = new Series("Votos")
            {
                ChartType = SeriesChartType.Pie,
                Legend = "Leyenda",
                LabelForeColor = Color.White,
                Font = HelperFuentes.BricolageBold(10f)
            };
            serie.BorderColor = Color.White;
            serie.BorderWidth = 2;

            foreach (var f in filas)
            {
                string nombre = f.EsWriteIn ? f.Candidato + " (Otro)" : f.Candidato;
                int i = serie.Points.AddXY(nombre, f.Votos);
                serie.Points[i].Label = f.Porcentaje.ToString("0.0") + "%";
                serie.Points[i].LegendText = nombre + ": " + f.Votos + " votos (" + f.Porcentaje.ToString("0.0") + "%)";
            }
            chartResultados.Series.Add(serie);
            chartResultados.Refresh();
        }

        // ---------- Gráfica 2: votaron vs. no votaron (usa el padrón) ----------
        private void GraficaParticipacion(List<Voto> votos, List<Alumno> padron, TipoConvocatoria convocatoria)
        {
            FilaParticipacion g = CalculoParticipacion.General(votos, padron, convocatoria);
            if (g.Electores == 0) { SinDatos("El padrón no tiene alumnos."); return; }

            Preparar("Participación y abstención - " + convocatoria.ObtenerNombreMostrar(), true);
            var serie = new Series("Participacion")
            {
                ChartType = SeriesChartType.Doughnut,
                Legend = "Leyenda",
                Font = HelperFuentes.BricolageBold(10f)
            };
            serie.BorderColor = Color.White;
            serie.BorderWidth = 2;

            int a = serie.Points.AddXY("Votaron", g.Votaron);
            serie.Points[a].Color = Azul;
            serie.Points[a].LabelForeColor = Color.White;
            serie.Points[a].Label = g.PorcentajeParticipacion.ToString("0.0") + "%";
            serie.Points[a].LegendText = "Votaron: " + g.Votaron + " de " + g.Electores +
                                         " (" + g.PorcentajeParticipacion.ToString("0.0") + "%)";

            int b = serie.Points.AddXY("No votaron", g.Abstenciones);
            serie.Points[b].Color = Gris;
            serie.Points[b].LabelForeColor = Azul;
            serie.Points[b].Label = g.PorcentajeAbstencion.ToString("0.0") + "%";
            serie.Points[b].LegendText = "No votaron (abstención): " + g.Abstenciones +
                                         " (" + g.PorcentajeAbstencion.ToString("0.0") + "%)";

            chartResultados.Series.Add(serie);
            chartResultados.Refresh();
        }

        // ---------- Gráfica 3: participación por Grupo / Carrera / Centro (usa el padrón) ----------
        private void GraficaPorGrupo(List<Voto> votos, List<Alumno> padron, TipoConvocatoria convocatoria, Agrupacion por)
        {
            var filas = CalculoParticipacion.Calcular(votos, padron, convocatoria, por);
            if (filas.Count == 0) { SinDatos("El padrón no tiene alumnos."); return; }

            Preparar("Participación por " + por.NombreVisible().ToLowerInvariant() +
                     " - " + convocatoria.ObtenerNombreMostrar(), true);

            var area = chartResultados.ChartAreas[0];
            area.AxisY.Minimum = 0;
            area.AxisY.Maximum = 100;
            area.AxisY.Title = "% de alumnos";
            area.AxisY.TitleForeColor = TextoSuave;
            area.AxisX.Interval = 1;
            area.AxisX.Minimum = 0.5;                        // un poco de margen a cada lado
            area.AxisX.Maximum = filas.Count + 0.5;

            area.AxisX.LabelStyle.Angle = filas.Count > 4 ? -35 : 0;
            area.AxisX.LabelStyle.Font = HelperFuentes.InstrumentRegular(9f);
            area.AxisX.LabelStyle.IsStaggered = false;

            var votaron = new Series("Votaron (%)")
            {
                ChartType = SeriesChartType.StackedColumn,
                Legend = "Leyenda",
                Color = Azul,
                LabelForeColor = Color.White,
                Font = HelperFuentes.BricolageBold(9f)
            };
            var faltaron = new Series("No votaron (%)")
            {
                ChartType = SeriesChartType.StackedColumn,
                Legend = "Leyenda",
                Color = Gris
            };

            for (int n = 0; n < filas.Count; n++)
            {
                var f = filas[n];
                double x = n + 1;   // posición numérica: así cada grupo ocupa su propia columna

                int i = votaron.Points.AddXY(x, f.PorcentajeParticipacion);
                votaron.Points[i].AxisLabel = f.Valor;       // el nombre del grupo/carrera/centro aparece debajo
                votaron.Points[i].Label = f.Votaron + "/" + f.Electores;
                votaron.Points[i].ToolTip = f.Valor + ": votaron " + f.Votaron + " de " + f.Electores +
                                            " (" + f.PorcentajeParticipacion.ToString("0.0") + "%)";

                int j = faltaron.Points.AddXY(x, f.PorcentajeAbstencion);
                faltaron.Points[j].AxisLabel = f.Valor;
                faltaron.Points[j].ToolTip = f.Valor + ": no votaron " + f.Abstenciones +
                                             " (" + f.PorcentajeAbstencion.ToString("0.0") + "%)";
            }

            chartResultados.Series.Add(votaron);
            chartResultados.Series.Add(faltaron);
            chartResultados.Refresh();
        }

        // ---------- utilidades ----------

        private void Preparar(string titulo, bool conLeyenda)
        {
            chartResultados.Series.Clear();
            chartResultados.ChartAreas.Clear();
            chartResultados.Titles.Clear();
            chartResultados.Legends.Clear();

            var area = new ChartArea("Area") { BackColor = Color.Transparent };
            foreach (var eje in new[] { area.AxisX, area.AxisY })
            {
                eje.LineColor = Borde;
                eje.MajorGrid.LineColor = Borde;
                eje.MajorTickMark.LineColor = Borde;
                eje.LabelStyle.ForeColor = TextoSuave;
                eje.LabelStyle.Font = HelperFuentes.InstrumentRegular(9f);
            }
            chartResultados.ChartAreas.Add(area);

            if (conLeyenda)
            {
                chartResultados.Legends.Add(new Legend("Leyenda")
                {
                    Docking = Docking.Right,
                    BackColor = Color.Transparent,
                    ForeColor = Azul,
                    Font = HelperFuentes.InstrumentRegular(10f)
                });
            }
            chartResultados.Titles.Add(new Title(titulo, Docking.Top, HelperFuentes.BricolageBold(12f), Azul));
        }

        /// <summary>En lugar de una ventana emergente, muestra el aviso dentro de la gráfica.</summary>
        private void SinDatos(string mensaje)
        {
            chartResultados.Series.Clear();
            chartResultados.ChartAreas.Clear();
            chartResultados.Legends.Clear();
            chartResultados.Titles.Clear();
            chartResultados.Titles.Add(new Title(mensaje, Docking.Top, HelperFuentes.InstrumentRegular(12f), TextoSuave));
            chartResultados.Refresh();
        }

        private void pnlResultados_Click(object sender, EventArgs e)
        {
            Form1? parent = this.FindForm() as Form1;
            if (parent != null) parent.Ir(new UcResultados());
        }

        private void pnlExportar_Click(object sender, EventArgs e)
        {
            Form1? parent = this.FindForm() as Form1;
            if (parent != null) parent.Ir(new UcExportar());
        }
    }
}
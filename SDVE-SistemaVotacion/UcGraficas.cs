using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SDVE_SistemaVotacion.Models;
using SDVE_SistemaVotacion.Services;

namespace SDVE_SistemaVotacion
{
    public partial class UcGraficas : UserControl
    {
        private static readonly Color Azul = Color.FromArgb(94, 148, 255);
        private static readonly Color Gris = Color.FromArgb(200, 200, 200);

        private static readonly Agrupacion[] Agrupaciones =
            { Agrupacion.Grupo, Agrupacion.Carrera, Agrupacion.CentroUniversitario };

        private readonly ComboBox cmbGrafica = new ComboBox();
        private readonly ComboBox cmbConvocatoria = new ComboBox();
        private readonly ComboBox cmbAgrupar = new ComboBox();

        private bool cargando = true;   // evita redibujar mientras se llenan los combos

        public UcGraficas()
        {
            InitializeComponent();
            PrepararControles();
            this.Load += (s, e) => { cargando = false; Dibujar(); };
        }

        private void PrepararControles()
        {
            cmbGrafica.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGrafica.Items.AddRange(new object[]
            {
                "1. Votos por candidato",
                "2. Participación y abstención",
                "3. Participación por grupo"
            });
            cmbGrafica.Width = 230;

            cmbConvocatoria.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var c in CalculoResultados.Orden) cmbConvocatoria.Items.Add(c.ObtenerNombreMostrar());
            cmbConvocatoria.Width = 220;

            cmbAgrupar.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var a in Agrupaciones) cmbAgrupar.Items.Add(a.NombreVisible());
            cmbAgrupar.Width = 180;

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

            var barra = new FlowLayoutPanel
            {
                Location = new Point(20, 74),
                Size = new Size(panelGrafica.Width - 40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                WrapContents = false
            };
            barra.Controls.Add(Etiqueta("Gráfica:"));
            barra.Controls.Add(cmbGrafica);
            barra.Controls.Add(Etiqueta("   Convocatoria:"));
            barra.Controls.Add(cmbConvocatoria);
            barra.Controls.Add(Etiqueta("   Agrupar por:"));
            barra.Controls.Add(cmbAgrupar);
            panelGrafica.Controls.Add(barra);

            chartResultados.Location = new Point(20, 120);
            chartResultados.Size = new Size(panelGrafica.Width - 40, panelGrafica.Height - 140);
            chartResultados.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private static Label Etiqueta(string texto) =>
            new Label
            {
                Text = texto,
                AutoSize = true,
                Margin = new Padding(0, 6, 4, 0),
                BackColor = Color.Transparent,
                ForeColor = Color.White
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
            var serie = new Series("Votos") { ChartType = SeriesChartType.Pie, Legend = "Leyenda" };

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
            var serie = new Series("Participacion") { ChartType = SeriesChartType.Doughnut, Legend = "Leyenda" };

            int a = serie.Points.AddXY("Votaron", g.Votaron);
            serie.Points[a].Color = Azul;
            serie.Points[a].Label = g.PorcentajeParticipacion.ToString("0.0") + "%";
            serie.Points[a].LegendText = "Votaron: " + g.Votaron + " de " + g.Electores +
                                         " (" + g.PorcentajeParticipacion.ToString("0.0") + "%)";

            int b = serie.Points.AddXY("No votaron", g.Abstenciones);
            serie.Points[b].Color = Gris;
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
            area.AxisX.Interval = 1;
            area.AxisX.Minimum = 0.5;                        // un poco de margen a cada lado
            area.AxisX.Maximum = filas.Count + 0.5;

            // Forzar que las etiquetas del eje X muestren exactamente el nombre (Valor) y se acomoden sin solaparse
            area.AxisX.LabelStyle.Angle = filas.Count > 4 ? -35 : 0;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9F);
            area.AxisX.LabelStyle.IsStaggered = false;

            var votaron = new Series("Votaron (%)") { ChartType = SeriesChartType.StackedColumn, Legend = "Leyenda", Color = Azul };
            var faltaron = new Series("No votaron (%)") { ChartType = SeriesChartType.StackedColumn, Legend = "Leyenda", Color = Gris };

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

            chartResultados.ChartAreas.Add(new ChartArea("Area"));
            if (conLeyenda)
                chartResultados.Legends.Add(new Legend("Leyenda") { Docking = Docking.Right });
            chartResultados.Titles.Add(new Title(titulo, Docking.Top, new Font("Segoe UI", 12f, FontStyle.Bold), Color.Black));
        }

        /// <summary>En lugar de una ventana emergente, muestra el aviso dentro de la gráfica.</summary>
        private void SinDatos(string mensaje)
        {
            chartResultados.Series.Clear();
            chartResultados.ChartAreas.Clear();
            chartResultados.Legends.Clear();
            chartResultados.Titles.Clear();
            chartResultados.Titles.Add(new Title(mensaje, Docking.Top, new Font("Segoe UI", 12f), Color.DimGray));
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

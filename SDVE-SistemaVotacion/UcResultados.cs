using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDVE_SistemaVotacion.Models;
using SDVE_SistemaVotacion.Services;

namespace SDVE_SistemaVotacion
{
    public partial class UcResultados : UserControl
    {
        private DataGridView tabla = null!;
        private Label valorTotalVotos = null!;
        private Label valorOpcionLider = null!;
        private Label valorNoRegistrados = null!;

        public UcResultados()
        {
            InitializeComponent();

            // 1. Paleta de colores institucionales
            Color colorTextoPrincipal = Color.White;
            Color colorTextoSecundario = Color.Cornsilk;
            Color amarilloUAA = Color.FromArgb(255, 204, 0);

            // 2. Títulos principales (Ajustados para no chocar)
            lTitulo.Font = HelperFuentes.BricolageBold(15f);
            lTitulo.ForeColor = colorTextoPrincipal;

            lSeleccionCategoria.Font = HelperFuentes.BricolageBold(12f);
            lSeleccionCategoria.ForeColor = colorTextoPrincipal;
            cbCategoria.Font = HelperFuentes.InstrumentRegular(11f);

            // Anclamos el combo y su texto a la derecha para que no se superpongan con el título
            cbCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lSeleccionCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbCategoria.Left = pnlResultadosGenerales.Width - cbCategoria.Width - 10;
            lSeleccionCategoria.Left = cbCategoria.Left - lSeleccionCategoria.Width - 10;

            // 3. Tipografía para los subtítulos de las métricas
            Font fuenteMetricas = HelperFuentes.BricolageBold(11f);
            lTotalVotos.Font = fuenteMetricas;
            lTotalVotos.ForeColor = colorTextoSecundario;
            lOpcionLider.Font = fuenteMetricas;
            lOpcionLider.ForeColor = colorTextoSecundario;
            lVotosNulos.Font = fuenteMetricas;
            lVotosNulos.ForeColor = colorTextoSecundario;

            // 4. Valores gigantes con el Amarillo UAA
            valorTotalVotos = CrearValor(guna2Panel3, amarilloUAA);
            valorOpcionLider = CrearValor(pnlOpcionLider, amarilloUAA);
            valorNoRegistrados = CrearValor(pnlVotosNulos, amarilloUAA);

            lVotosNulos.Text = "No Registrados";

            lFecha.Font = HelperFuentes.InstrumentRegular(10f);
            lFecha.ForeColor = colorTextoSecundario;

            PrepararTabla();

            this.Load += UcResultados_Load;
            cbCategoria.SelectedIndexChanged += CbCategoria_SelectedIndexChanged;
        }

        private void UcResultados_Load(object? sender, EventArgs e)
        {
            if (cbCategoria.Items.Count == 3 && cbCategoria.Items[0]?.ToString() != "Todas")
                cbCategoria.Items.Insert(0, "Todas");
            if (cbCategoria.SelectedIndex < 0) cbCategoria.SelectedIndex = 0;

            CargarResultados();
        }

        private void CargarResultados(TipoConvocatoria? filtro = null)
        {
            ResumenResultados resumen;
            List<Voto> todos;

            try
            {
                todos = AlmacenVotos.ObtenerTodos().ToList();
                IEnumerable<Voto> votos = todos;

                if (filtro != null)
                    votos = votos.Where(v => v.Convocatoria == filtro.Value);

                resumen = CalculoResultados.Calcular(votos, ListaOficial.Candidatos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            tabla.Rows.Clear();
            var filasParaMostrar = resumen.Filas.AsEnumerable();

            if (filtro != null)
            {
                filasParaMostrar = filasParaMostrar.Where(f => f.Convocatoria == filtro.Value);
            }

            foreach (FilaResultado f in filasParaMostrar)
            {
                tabla.Rows.Add(
                    f.Posicion,
                    f.EsWriteIn ? f.Candidato + " *" : f.Candidato,
                    f.Categoria,
                    f.Votos,
                    f.Porcentaje.ToString("0.0") + " %");
            }

            int total;
            int votosNoReg;
            FilaResultado? lider;
            int totalAlumnos = AlmacenPadron.Alumnos.Count();
            int totalConvocatorias = CalculoResultados.Orden.Count();

            if (filtro == null)
            {
                int maxVotosPosibles = totalAlumnos * totalConvocatorias;
                votosNoReg = Math.Max(0, maxVotosPosibles - resumen.TotalVotos);
                total = resumen.TotalVotos;
                lider = resumen.Lider;
            }
            else
            {
                var votosFiltro = todos.Where(v => v.Convocatoria == filtro.Value).ToList();
                int foliosUnicosFiltro = votosFiltro.Select(v => v.Folio).Distinct().Count();
                votosNoReg = Math.Max(0, totalAlumnos - foliosUnicosFiltro);

                var filas = resumen.Filas.Where(f => f.Convocatoria == filtro.Value).ToList();
                total = filas.Sum(f => f.Votos);
                lider = filas.OrderByDescending(f => f.Votos).FirstOrDefault();
            }

            valorTotalVotos.Text = total.ToString();
            valorNoRegistrados.Text = votosNoReg.ToString();
            valorOpcionLider.Text = lider == null ? "Sin votos aún" : lider.Candidato + " (" + lider.Votos + ")";
            lFecha.Text = "Ultima Actualizacion: " + resumen.Fecha.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void PrepararTabla()
        {
            txtPosicion.Visible = false;

            // Ocultamos permanentemente los paneles falsos que se cortaban
            pnlPosicion.Visible = false;
            pnlCandidato.Visible = false;
            pnlCategoria.Visible = false;
            pnlTotalVotos.Visible = false;
            pnlPorcentaje.Visible = false;

            dvgResultados.BackColor = Color.FromArgb(40, 42, 54);

            tabla = new DataGridView
            {
                // La tabla ahora ocupa todo el espacio sin dejar huecos
                Location = new Point(0, 0),
                Size = new Size(dvgResultados.Width, dvgResultados.Height),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,

                // ACTIVAMOS LOS ENCABEZADOS NATIVOS
                ColumnHeadersVisible = true,
                ColumnHeadersHeight = 45,
                EnableHeadersVisualStyles = false,

                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(60, 63, 80),
                BackgroundColor = Color.FromArgb(40, 42, 54),

                // Hace que las columnas se estiren solas como liga, rellenando lo blanco
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Damos diseño oscuro y tipografía Bricolage a los encabezados nativos
            tabla.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 42, 54);
            tabla.ColumnHeadersDefaultCellStyle.ForeColor = Color.Cornsilk;
            tabla.ColumnHeadersDefaultCellStyle.Font = HelperFuentes.BricolageBold(11f);
            tabla.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tabla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Damos diseño oscuro a las filas
            tabla.DefaultCellStyle.Font = HelperFuentes.InstrumentRegular(12f);
            tabla.DefaultCellStyle.ForeColor = Color.White;
            tabla.DefaultCellStyle.BackColor = Color.FromArgb(40, 42, 54);
            tabla.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tabla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(60, 63, 80);
            tabla.DefaultCellStyle.SelectionForeColor = Color.White;

            tabla.RowTemplate.Height = 38;

            // Agregamos las columnas indicando su "Peso" (FillWeight) en lugar de un ancho fijo
            AgregarColumna("Posicion", "Posición", 15);
            AgregarColumna("Candidato", "Nombre del Candidato", 35);
            AgregarColumna("Categoria", "Categoría", 25);
            AgregarColumna("TotalVotos", "Total Votos", 15);
            AgregarColumna("Porcentaje", "Porcentaje", 10);

            dvgResultados.Controls.Add(tabla);
            tabla.BringToFront();
        }

        private void AgregarColumna(string id, string texto, int pesoFila)
        {
            int indice = tabla.Columns.Add(id, texto);
            // Esto le dice a la columna qué porcentaje del espacio libre debe tomar
            tabla.Columns[indice].FillWeight = pesoFila;
            tabla.Columns[indice].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private static Label CrearValor(Control tarjeta, Color colorTexto)
        {
            var valor = new Label
            {
                AutoSize = false,
                AutoEllipsis = true,
                Dock = DockStyle.Bottom,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = HelperFuentes.BricolageBold(16f),
                ForeColor = colorTexto,
                BackColor = Color.Transparent,
                Text = "0"
            };
            tarjeta.Controls.Add(valor);
            return valor;
        }

        private void CbCategoria_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbCategoria.SelectedIndex <= 0) CargarResultados(null);
            else
            {
                var idx = cbCategoria.SelectedIndex - 1;
                var tipo = CalculoResultados.Orden.ElementAtOrDefault(idx);
                if (tipo != null) CargarResultados(tipo);
                else CargarResultados(null);
            }
        }

        private void pnlGraficas_Click(object sender, EventArgs e)
        {
            Form1? parent = this.FindForm() as Form1;
            if (parent != null) parent.Ir(new UcGraficas());
        }

        private void pnlExportar_Click(object sender, EventArgs e)
        {
            Form1? parent = this.FindForm() as Form1;
            if (parent != null)
            {
                UcExportar ex = new UcExportar();
                parent.Ir(ex);
            }
        }

        private void pnlFecha_Paint(object sender, PaintEventArgs e) { }
    }
}
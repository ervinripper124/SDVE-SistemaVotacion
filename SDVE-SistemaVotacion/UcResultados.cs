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
        // Paleta UAA: azul marino, blanco, poco rojo
        private static readonly Color Azul = Color.FromArgb(10, 42, 102);
        private static readonly Color Rojo = Color.FromArgb(200, 16, 46);
        private static readonly Color Fondo = Color.FromArgb(244, 246, 251);
        private static readonly Color Blanco = Color.White;
        private static readonly Color Borde = Color.FromArgb(222, 227, 238);
        private static readonly Color TextoSuave = Color.FromArgb(107, 115, 133);
        private static readonly Color Seleccion = Color.FromArgb(220, 230, 247);

        private DataGridView tabla = null!;
        private Label valorTotalVotos = null!;
        private Label valorOpcionLider = null!;
        private Label valorNoRegistrados = null!;

        public UcResultados()
        {
            InitializeComponent();

            AplicarEstiloUAA();

            PrepararTabla();

            this.Load += UcResultados_Load;
            cbCategoria.SelectedIndexChanged += CbCategoria_SelectedIndexChanged;

        }

        private void AplicarEstiloUAA()
        {
            // 1. Fondos
            pnlMain.FillColor = Fondo;
            pnlMain.BackColor = Fondo;
            guna2Panel1.FillColor = Fondo;
            guna2Panel1.BackColor = Fondo;
            panelResultados.FillColor = Fondo;
            panelResultados.BackColor = Fondo;
            pnlMetricas.FillColor = Fondo;
            pnlMetricas.BackColor = Fondo;

            int ancho = panelResultados.Width;
            int alto = panelResultados.Height;

            // 2. Encabezado azul con línea roja
            pnlResultadosGenerales.FillColor = Azul;
            pnlResultadosGenerales.BackColor = Azul;
            pnlResultadosGenerales.Location = new Point(0, 0);
            pnlResultadosGenerales.Size = new Size(ancho, 60);

            lTitulo.Text = "Resultados generales de votación";
            lTitulo.Font = HelperFuentes.BricolageBold(15f);
            lTitulo.ForeColor = Blanco;
            lTitulo.BackColor = Color.Transparent;
            lTitulo.Location = new Point(20, 15);

            var lineaRoja = new Panel
            {
                BackColor = Rojo,
                Dock = DockStyle.Bottom,
                Height = 3
            };
            pnlResultadosGenerales.Controls.Add(lineaRoja);

            // 3. Combo de categoría
            lSeleccionCategoria.Font = HelperFuentes.BricolageBold(11f);
            lSeleccionCategoria.ForeColor = Blanco;
            lSeleccionCategoria.BackColor = Color.Transparent;

            cbCategoria.Font = HelperFuentes.InstrumentRegular(11f);
            cbCategoria.FillColor = Blanco;
            cbCategoria.ForeColor = Azul;
            cbCategoria.BorderColor = Blanco;
            cbCategoria.BorderRadius = 8;
            cbCategoria.FocusedColor = Rojo;
            cbCategoria.FocusedState.BorderColor = Rojo;
            cbCategoria.Size = new Size(240, 36);
            cbCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lSeleccionCategoria.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbCategoria.Location = new Point(ancho - cbCategoria.Width - 20, 10);
            lSeleccionCategoria.Location = new Point(cbCategoria.Left - lSeleccionCategoria.Width - 10, 14);

            // 4. Tarjetas de métricas
            pnlMetricas.Location = new Point(0, 72);
            pnlMetricas.Size = new Size(ancho, 72);

            Font fuenteMetricas = HelperFuentes.BricolageBold(10.5f);
            ConfigurarTarjeta(guna2Panel3, lTotalVotos, fuenteMetricas);
            ConfigurarTarjeta(pnlOpcionLider, lOpcionLider, fuenteMetricas);
            ConfigurarTarjeta(pnlVotosNulos, lVotosNulos, fuenteMetricas);
            lVotosNulos.Text = "No registrados";

            valorTotalVotos = CrearValor(guna2Panel3, Azul, 16f);
            valorOpcionLider = CrearValor(pnlOpcionLider, Azul, 14f);
            valorNoRegistrados = CrearValor(pnlVotosNulos, Azul, 16f);

            pnlMetricas.Resize += (s, e) => DistribuirTarjetas();
            DistribuirTarjetas();

            // 5. Contenedor de tabla
            dvgResultados.FillColor = Blanco;
            dvgResultados.BackColor = Fondo;
            dvgResultados.BorderColor = Borde;
            dvgResultados.BorderThickness = 1;
            dvgResultados.BorderRadius = 10;
            dvgResultados.Location = new Point(20, 156);
            dvgResultados.Size = new Size(ancho - 40, alto - 156 - 40);

            // 6. Pie
            lFecha.Font = HelperFuentes.InstrumentRegular(10f);
            lFecha.ForeColor = TextoSuave;
            lFecha.BackColor = Color.Transparent;
            lFecha.Location = new Point(20, alto - 30);
        }

        private void ConfigurarTarjeta(Guna.UI2.WinForms.Guna2Panel tarjeta, Guna.UI2.WinForms.Guna2HtmlLabel titulo, Font fuente)
        {
            tarjeta.FillColor = Blanco;
            tarjeta.BackColor = Fondo;
            tarjeta.BorderColor = Borde;
            tarjeta.BorderThickness = 1;
            tarjeta.BorderRadius = 12;
            tarjeta.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            titulo.Font = fuente;
            titulo.ForeColor = TextoSuave;
            titulo.BackColor = Color.Transparent;
            titulo.Location = new Point(0, 8);
        }

        private void DistribuirTarjetas()
        {
            int margen = 20, separacion = 14;
            int ancho = (pnlMetricas.Width - margen * 2 - separacion * 2) / 3;
            var tarjetas = new Control[] { guna2Panel3, pnlOpcionLider, pnlVotosNulos };

            for (int i = 0; i < tarjetas.Length; i++)
            {
                tarjetas[i].SetBounds(margen + i * (ancho + separacion), 0, ancho, pnlMetricas.Height);
            }
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
            lFecha.Text = "Última actualización: " + resumen.Fecha.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void PrepararTabla()
        {
            txtPosicion.Visible = false;

            tabla = new DataGridView
            {
                Location = new Point(6, 6),
                Size = new Size(dvgResultados.Width - 12, dvgResultados.Height - 12),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,

                ColumnHeadersVisible = true,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
                ColumnHeadersHeight = 40,
                EnableHeadersVisualStyles = false,

                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Borde,
                BackgroundColor = Blanco,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Encabezado azul
            tabla.ColumnHeadersDefaultCellStyle.BackColor = Azul;
            tabla.ColumnHeadersDefaultCellStyle.ForeColor = Blanco;
            tabla.ColumnHeadersDefaultCellStyle.SelectionBackColor = Azul;
            tabla.ColumnHeadersDefaultCellStyle.SelectionForeColor = Blanco;
            tabla.ColumnHeadersDefaultCellStyle.Font = HelperFuentes.BricolageBold(10.5f);
            tabla.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tabla.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Filas blancas con alternado suave
            tabla.DefaultCellStyle.Font = HelperFuentes.InstrumentRegular(11f);
            tabla.DefaultCellStyle.ForeColor = Azul;
            tabla.DefaultCellStyle.BackColor = Blanco;
            tabla.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tabla.DefaultCellStyle.SelectionBackColor = Seleccion;
            tabla.DefaultCellStyle.SelectionForeColor = Azul;
            tabla.AlternatingRowsDefaultCellStyle.BackColor = Fondo;
            tabla.AlternatingRowsDefaultCellStyle.SelectionBackColor = Seleccion;
            tabla.AlternatingRowsDefaultCellStyle.SelectionForeColor = Azul;

            tabla.RowTemplate.Height = 36;

            AgregarColumna("Posicion", "Posición", 15);
            AgregarColumna("Candidato", "Nombre del candidato", 35);
            AgregarColumna("Categoria", "Categoría", 25);
            AgregarColumna("TotalVotos", "Total de votos", 15);
            AgregarColumna("Porcentaje", "Porcentaje", 10);

            dvgResultados.Controls.Add(tabla);
            tabla.BringToFront();
        }

        private void AgregarColumna(string id, string texto, int pesoFila)
        {
            int indice = tabla.Columns.Add(id, texto);
            tabla.Columns[indice].FillWeight = pesoFila;
            tabla.Columns[indice].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private static Label CrearValor(Control tarjeta, Color colorTexto, float tamano)
        {
            var valor = new Label
            {
                AutoSize = false,
                AutoEllipsis = true,
                Dock = DockStyle.Bottom,
                Height = 35,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = HelperFuentes.BricolageBold(tamano),
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

        private void panelResultados_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
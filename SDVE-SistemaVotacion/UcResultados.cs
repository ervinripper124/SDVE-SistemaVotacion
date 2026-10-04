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

            valorTotalVotos = CrearValor(guna2Panel3, lTotalVotos);
            valorOpcionLider = CrearValor(pnlOpcionLider, lOpcionLider);
            valorNoRegistrados = CrearValor(pnlVotosNulos, lVotosNulos);

            lVotosNulos.Text = "No Registrados";

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

                resumen = CalculoResultados.Calcular(
                    votos,
                    ListaOficial.Candidatos);
            }
            catch (Exception ex) when (
                ex is IOException ||
                ex is InvalidDataException ||
                ex is UnauthorizedAccessException)
            {
                MessageBox.Show(
                    "No se pudieron leer los votos: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            tabla.Rows.Clear();

            var filasParaMostrar = resumen.Filas.AsEnumerable();

            if (filtro != null)
            {
                filasParaMostrar = filasParaMostrar
                    .Where(f => f.Convocatoria == filtro.Value);
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
                // Todas las convocatorias (globales): 
                // Suma total de boletas faltantes en todas las categorías (Alumnos * Convocatorias - Votos emitidos)
                int maxVotosPosibles = totalAlumnos * totalConvocatorias;
                votosNoReg = Math.Max(0, maxVotosPosibles - resumen.TotalVotos);

                total = resumen.TotalVotos;
                lider = resumen.Lider;
            }
            else
            {
                // Una categoría específica
                var votosFiltro = todos.Where(v => v.Convocatoria == filtro.Value).ToList();
                int foliosUnicosFiltro = votosFiltro.Select(v => v.Folio).Distinct().Count();
                votosNoReg = Math.Max(0, totalAlumnos - foliosUnicosFiltro);

                var filas = resumen.Filas
                    .Where(f => f.Convocatoria == filtro.Value)
                    .ToList();

                total = filas.Sum(f => f.Votos);

                lider = filas
                    .OrderByDescending(f => f.Votos)
                    .FirstOrDefault();
            }

            valorTotalVotos.Text = total.ToString();
            valorNoRegistrados.Text = votosNoReg.ToString();

            valorOpcionLider.Text = lider == null
                ? "Sin votos aún"
                : lider.Candidato + " (" + lider.Votos + ")";

            lFecha.Text =
                "Ultima Actualizacion: " +
                resumen.Fecha.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void PrepararTabla()
        {
            txtPosicion.Visible = false;

            tabla = new DataGridView
            {
                Location = txtPosicion.Location,
                Size = new Size(dvgResultados.Width, dvgResultados.Height - txtPosicion.Top - 6),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                ColumnHeadersVisible = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.FromArgb(60, 63, 80),
                BackgroundColor = Color.FromArgb(40, 42, 54),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            };

            tabla.RowTemplate.Height = 32;
            tabla.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tabla.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 242, 255);
            tabla.DefaultCellStyle.SelectionForeColor = Color.Black;

            AgregarColumna("Posicion", 160);
            AgregarColumna("Candidato", 317);
            AgregarColumna("Categoria", 265);
            AgregarColumna("TotalVotos", 160);
            AgregarColumna("Porcentaje", 154);

            dvgResultados.Controls.Add(tabla);
        }

        private void AgregarColumna(string nombre, int ancho)
        {
            int indice = tabla.Columns.Add(nombre, nombre);
            tabla.Columns[indice].Width = ancho;
            tabla.Columns[indice].SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private static Label CrearValor(Control tarjeta, Control titulo)
        {
            var valor = new Label
            {
                AutoSize = false,
                AutoEllipsis = true,
                Dock = DockStyle.Bottom,
                Height = 38,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = titulo.ForeColor,
                BackColor = Color.Transparent,
                Text = "0"
            };
            tarjeta.Controls.Add(valor);
            return valor;
        }

        private void CbCategoria_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cbCategoria.SelectedIndex <= 0)
                CargarResultados(null);
            else
            {
                var idx = cbCategoria.SelectedIndex - 1;
                var tipo = Services.CalculoResultados.Orden.ElementAtOrDefault(idx);
                if (tipo != null)
                    CargarResultados(tipo);
                else
                    CargarResultados(null);
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

        private void pnlFecha_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
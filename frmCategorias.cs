using System;
using System.Windows.Forms;
using Dominio;
using TPWinForm_equipo_6.Negocio;
using Microsoft.VisualBasic;

namespace TPWinForm_equipo_6
{
    public partial class frmCategorias : Form
    {
        public frmCategorias()
        {
            InitializeComponent();
            Text = "Categorías";
            StartPosition = FormStartPosition.CenterParent;
            dgvCategorias.ReadOnly = true;
            dgvCategorias.AllowUserToAddRows = false;
            dgvCategorias.AllowUserToDeleteRows = false;
            dgvCategorias.MultiSelect = false;
            dgvCategorias.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Load += frmCategorias_Load;
        }

        private void frmCategorias_Load(object? sender, EventArgs e)
        {
            try { CargarCategorias(); }
            catch (Exception ex) { MessageBox.Show("No se pudieron cargar las categorías.\n" + ex.Message); }
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            dgvCategorias.DataSource = negocio.Listar();
            dgvCategorias.Columns["Id"]!.Visible = false;
            dgvCategorias.Columns["descripcion"]!.HeaderText = "Descripción";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string descripcion = Interaction.InputBox(
                "Ingresá la descripción de la categoría:",
                "Agregar categoría");

            if (string.IsNullOrWhiteSpace(descripcion))
                return;

            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.Agregar(descripcion);

                MessageBox.Show(
                    "Categoría agregada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo agregar la categoría.\n\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow?.DataBoundItem is not Categoria seleccionada)
            {
                MessageBox.Show("Seleccioná una categoría.");
                return;
            }


            string descripcion = Interaction.InputBox(
                "Ingresá la nueva descripción:",
                "Modificar categoría",
                seleccionada.descripcion);

            if (string.IsNullOrWhiteSpace(descripcion))
                return;

            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();

                negocio.Modificar(
                    seleccionada.Id,
                    descripcion);

                MessageBox.Show(
                    "Categoría modificada correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                CargarCategorias();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo modificar la categoría.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // El Designer tiene conectado este nombre
        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dgvCategorias.CurrentRow?.DataBoundItem is not Categoria seleccionada)
            {
                MessageBox.Show(
                    "Seleccioná una categoría.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }


            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();

                if (negocio.EstaEnUso(seleccionada.Id))
                {
                    MessageBox.Show(
                        "No se puede dar de baja la categoría \"" +
                        seleccionada.descripcion +
                        "\" porque está siendo utilizada por un artículo.",
                        "Categoría en uso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Querés dar de baja la categoría \"" +
                    seleccionada.descripcion +
                    "\"?",
                    "Confirmar baja",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);

                if (respuesta == DialogResult.Yes)
                {
                    negocio.EliminarLogico(seleccionada.Id);

                    MessageBox.Show(
                        "Categoría dada de baja correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarCategorias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo dar de baja la categoría.\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
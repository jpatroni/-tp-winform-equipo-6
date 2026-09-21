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
            CargarCategorias();
        }

        private void frmCategorias_Load(object sender, EventArgs e)
        {
            CargarCategorias();
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            dgvCategorias.DataSource = negocio.Listar();
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
            if (dgvCategorias.CurrentRow == null)
            {
                MessageBox.Show("Seleccioná una categoría.");
                return;
            }

            Categoria seleccionada =
                (Categoria)dgvCategorias.CurrentRow.DataBoundItem;

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
            if (dgvCategorias.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccioná una categoría.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            Categoria seleccionada =
                (Categoria)dgvCategorias.CurrentRow.DataBoundItem;

            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();

                if (negocio.EstaEnUso(seleccionada.Id))
                {
                    MessageBox.Show(
                        "No se puede eliminar la categoría \"" +
                        seleccionada.descripcion +
                        "\" porque está siendo utilizada por un artículo.",
                        "Categoría en uso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                DialogResult respuesta = MessageBox.Show(
                    "¿Querés eliminar la categoría \"" +
                    seleccionada.descripcion +
                    "\"?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    negocio.Eliminar(seleccionada.Id);

                    MessageBox.Show(
                        "Categoría eliminada correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    CargarCategorias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo eliminar la categoría.\n\n" +
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
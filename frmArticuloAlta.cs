using System;
using System.Collections.Generic;
using Dominio;
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class frmArticuloAlta : Form
    {
        public frmArticuloAlta()
        {
            InitializeComponent();
            MarcaNegocio negocio = new MarcaNegocio();
            cboMarca.DataSource = negocio.Listar();
            cboMarca.DisplayMember = "descripcion";
            cboMarca.ValueMember = "id";
            // Creo el objeto que sabe consultar categorías.
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            cboCategoria.DisplayMember = "descripcion";
            cboCategoria.ValueMember = "id";
            // Entrego esas categorías al desplegable.
            cboCategoria.DataSource = categoriaNegocio.Listar();


        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new();
            try
            {
                if (string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    MessageBox.Show("El campo codigo no puede estar vacio ");
                    return;
                }
                if(string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    MessageBox.Show("El campo nombre no puede estar vacio");
                    return;
                }

                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Precio = nudPrecio.Value;
                if (cboMarca.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una marca.");
                    return;
                }
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                if (cboCategoria.SelectedItem == null)
                {
                    MessageBox.Show("Debe seleccionar una categoría.");
                    return;
                }
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;



                new ArticuloNegocio().Agregar(articulo);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }

        private void btnCancelar_Click(Object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using Dominio;
using System.Configuration;
using System.IO;
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class frmArticuloAlta : Form
    {
        private Articulo articulo = new Articulo();
        private string? archivoLocal;
        public frmArticuloAlta()
        {
            InitializeComponent();
        }

        //Modificar
        public frmArticuloAlta(Articulo articulo)
        {
            InitializeComponent();
            // Editamos una copia: cancelar no debe alterar el artículo de la grilla.
            this.articulo = new Articulo
            {
                Id = articulo.Id, Codigo = articulo.Codigo, Nombre = articulo.Nombre,
                Descripcion = articulo.Descripcion, Precio = articulo.Precio,
                Marca = articulo.Marca, Categoria = articulo.Categoria,
                Imagenes = articulo.Imagenes.Select(i => new Imagen
                {
                    Id = i.Id, IdArticulo = i.IdArticulo, IdImagen = i.IdImagen
                }).ToList()
            };
            Text = "Modificar Artículo";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
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
                if (nudPrecio.Value <=0)
                {
                    MessageBox.Show("El precio debe ser mayor a 0");
                    return;
                }


                articulo.Codigo = txtCodigo.Text.Trim();
                articulo.Nombre = txtNombre.Text.Trim();
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

                // Conservamos las demás imágenes al editar la primera.
                string direccion = txtURLImagen.Text.Trim();
                if (direccion.Length > 0)
                {
                    bool esWeb = Uri.TryCreate(direccion, UriKind.Absolute, out Uri? uri)
                        && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
                    if (!esWeb && !File.Exists(direccion))
                    {
                        MessageBox.Show("Ingresá una URL http/https o seleccioná un archivo existente.");
                        return;
                    }
                    if (!esWeb && direccion == archivoLocal)
                    {
                        string carpeta = ConfigurationManager.AppSettings["images-folder"]
                            ?? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CatalogoEquipo6", "Imagenes");
                        Directory.CreateDirectory(carpeta);
                        string destino = Path.Combine(carpeta, Guid.NewGuid() + Path.GetExtension(direccion));
                        File.Copy(direccion, destino);
                        direccion = destino;
                        txtURLImagen.Text = destino;
                        archivoLocal = null;
                    }
                    if (articulo.Imagenes.Count == 0)
                        articulo.Imagenes.Add(new Imagen());
                    articulo.Imagenes[0].IdImagen = direccion;
                }
                else if (articulo.Imagenes.Count > 0)
                    articulo.Imagenes.RemoveAt(0);

                ArticuloNegocio negocio = new ArticuloNegocio();

                if (articulo.Id != 0)
                {
                    negocio.Modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    negocio.Agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }

                DialogResult = DialogResult.OK;


            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "No se pudo completar la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }





        private void frmArticuloAlta_Load(object sender, EventArgs e)
        {
            try
            {

                MarcaNegocio negocio = new MarcaNegocio();
                cboMarca.DataSource = negocio.Listar();
                cboMarca.DisplayMember = "Descripcion";
                cboMarca.ValueMember = "Id";
                cboMarca.DropDownStyle = ComboBoxStyle.DropDownList;

                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                cboCategoria.DisplayMember = "descripcion";
                cboCategoria.ValueMember = "Id";
                cboCategoria.DataSource = categoriaNegocio.Listar();

                if (articulo.Id != 0)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    nudPrecio.Maximum = Math.Max(nudPrecio.Maximum, articulo.Precio);
                    nudPrecio.Minimum = Math.Min(nudPrecio.Minimum, articulo.Precio);
                    nudPrecio.Value = articulo.Precio;
                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                    txtURLImagen.Text = articulo.Imagenes.FirstOrDefault()?.IdImagen ?? string.Empty;
                    cargarImagen(txtURLImagen.Text);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "No se pudo completar la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }
        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtURLImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Image?.Dispose();
                pbxArticulo.Image = null;
                if (string.IsNullOrWhiteSpace(imagen)) return;
                if (File.Exists(imagen))
                {
                    using var original = Image.FromFile(imagen);
                    pbxArticulo.Image = new Bitmap(original);
                }
                else
                    pbxArticulo.LoadAsync(imagen);
            }
            catch (Exception)
            {
                pbxArticulo.Image = null;
            }
        }

        private void btnCancelar_Click(Object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            using var archivo = new OpenFileDialog();
            archivo.Filter = "Imágenes|*.jpg;*.jpeg;*.png";
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                archivoLocal = archivo.FileName;
                txtURLImagen.Text = archivo.FileName;
                cargarImagen(archivo.FileName);

                //guardo la imagen
                //File.Copy(archivo.FileName, ConfigurationManager.AppSettings["images-folder"] + archivo.SafeFileName);
            }
        }
    }
}
using TPWinForm_equipo_6.Negocio;

namespace TPWinForm_equipo_6
{
    public partial class frmArticulo : Form
    {
        public frmArticulo()
        {
            InitializeComponent();
            MarcaNegocio negocio = new MarcaNegocio();
            cboMarca.DataSource = negocio.Listar();
            cboMarca.DisplayMember = "descripcion";
            cboMarca.ValueMember = "id";


        }

        private void cboMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
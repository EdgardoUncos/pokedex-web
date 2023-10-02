using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using dominio;
using negocio;

namespace pokedex_web
{
    public partial class FormularioPokemon : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;
            try
            {
                if (!IsPostBack)
                {

                    // COnfiguracion inicial de la pantalla
                    ElementoNegocio elementoNegocio = new ElementoNegocio();
                    List<Elemento> listaE = elementoNegocio.listar();

                    // Precarga de los desplegables dropdownlist
                    ddlTipo.DataSource = listaE;
                    ddlTipo.DataValueField = "Id";
                    ddlTipo.DataTextField = "Descripcion";
                    ddlTipo.DataBind();

                    ddlDebilidad.DataSource = listaE;
                    ddlDebilidad.DataValueField = "Id";
                    ddlDebilidad.DataTextField = "Descripcion";
                    ddlDebilidad.DataBind();

                    if (Request.QueryString["id"] != null)
                    {
                        PokemonNegocio negocio = new PokemonNegocio();
                        List<Pokemon> lista = negocio.listar(Request.QueryString["id"].ToString());
                        Pokemon seleccionado = lista[0];

                        // guardo pokemon seleccionado en session
                        Session.Add("pokeSeleccionado", seleccionado);

                        //Pre cargar todo los campos
                        string id = Request.QueryString["id"].ToString();
                        txtId.Text = id;
                        txtNombre.Text = seleccionado.Nombre;
                        txtDescripcion.Text = seleccionado.Descripcion;
                        txtImagenUrl.Text = seleccionado.UrlImagen;
                        txtNumero.Text = seleccionado.Numero.ToString();

                        ddlTipo.SelectedValue = seleccionado.Tipo.Id.ToString();
                        ddlDebilidad.SelectedValue = seleccionado.Debilidad.Id.ToString();
                        txtImagenUrl_TextChanged(sender, e);

                        // configurar acciones
                        if (!seleccionado.Activo)
                            btnInactivar.Text = "Reactivar";
                    }
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnInactivar_Click(object sender, EventArgs e)
        {
            try
            {
                PokemonNegocio negocio = new PokemonNegocio();
                Pokemon seleccionado = (Pokemon)Session["pokeSeleccionado"];

                negocio.eliminarLogico(seleccionado.Id, !seleccionado.Activo);
                              
                Response.Redirect("PokemonsLista.aspx", false);
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
            }
        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgPokemon.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon nuevo = new Pokemon();
                PokemonNegocio negocio = new PokemonNegocio();

                nuevo.Numero = int.Parse(txtNumero.Text);
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.UrlImagen = txtImagenUrl.Text;

                nuevo.Tipo = new Elemento();
                nuevo.Tipo.Id = int.Parse(ddlTipo.SelectedValue);
                nuevo.Debilidad = new Elemento();
                nuevo.Debilidad.Id = int.Parse(ddlDebilidad.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(Request.QueryString["id"].ToString());
                    negocio.modificarConSP(nuevo);
                }
                else
                    negocio.agregarConSP(nuevo);

                Response.Redirect("PokemonsLista.aspx", false);
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminacion_Click(object sender, EventArgs e)
        {
            try
            {
                if(chkConfirmaEliminacion.Checked)
                {
                    PokemonNegocio negocio = new PokemonNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));

                    Response.Redirect("PokemonsLista.aspx", false);
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
            }
        }

        protected void btnInactivar_Click1(object sender, EventArgs e)
        {

        }
    }
}
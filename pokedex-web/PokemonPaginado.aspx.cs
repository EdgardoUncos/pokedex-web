using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace pokedex_web
{
    public partial class PokemonPaginado : System.Web.UI.Page
    {
        public PokemonNegocio negocio = new PokemonNegocio();
        public List<Pokemon> ListaPokemon { get; set; }
        public List<Pokemon> ListaPokemonPagina { get; set; }
        public PaginacionViewModel PaginacionViewModel { get; set; } = new PaginacionViewModel();

        public PaginacionRespuesta<Pokemon> PaginacionRespuesta { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ListaPokemon = negocio.listar();
                ListaPokemonPagina = negocio.listarPaginacionViewModel(PaginacionViewModel);
                PaginacionRespuesta = new PaginacionRespuesta<Pokemon>()
                {
                    Elementos = ListaPokemonPagina,
                    Pagina = 1,
                    RecordsPorPagina = 5,
                    CantidadTotalRecords = ListaPokemon.Count,
                };

            }

            if (Request.QueryString["pagina"] != null && Request.QueryString["recordsPorPagina"] != null)
            {
                var nPagina = int.Parse(Request.QueryString["pagina"].ToString());
                var nRecordsPorPagina = int.Parse(Request.QueryString["recordsPorPagina"].ToString());
                int totalPokemones = ListaPokemon.Count;

                // Parametros de paginacion
                PaginacionViewModel.Pagina = nPagina;
                PaginacionViewModel.RecordsPorPagina = nRecordsPorPagina;
                ListaPokemonPagina.Clear();

                // Listar con parametros de paginacion
                ListaPokemonPagina = negocio.listarPaginacionViewModel(PaginacionViewModel);

                PaginacionRespuesta = new PaginacionRespuesta<Pokemon>()
                {
                    Elementos = ListaPokemonPagina,
                    Pagina = PaginacionViewModel.Pagina,
                    RecordsPorPagina = PaginacionViewModel.RecordsPorPagina,
                    CantidadTotalRecords = totalPokemones,
                };

            }
        }

        protected void ddlPaginas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nRegitros = ddlPaginas.SelectedItem.Value.ToString();
            string url = "PokemonPaginado.aspx?pagina=1&recordsPorPagina=" + nRegitros;
            Response.Redirect(url);
            Page_Load(sender, e);


        }
    }
}
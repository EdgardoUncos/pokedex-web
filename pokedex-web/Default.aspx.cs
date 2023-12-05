using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace pokedex_web
{
    public partial class Default : System.Web.UI.Page
    {
        PokemonNegocio negocio = new PokemonNegocio();
        public List<Pokemon> ListaPokemon { get; set; }
        public List<Pokemon> ListaPokemonPagina { get; set; }

        public PaginacionViewModel PaginacionViewModel { get; set; } = new PaginacionViewModel();
        public PaginacionRespuesta<Pokemon> PaginacionRespuesta {  get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                ListaPokemon = negocio.listar();
                ListaPokemonPagina = ListarConParametros(ListaPokemon, PaginacionViewModel);

                PaginacionRespuesta = new PaginacionRespuesta<Pokemon>()
                {
                    Elementos = ListaPokemonPagina,
                    Pagina = 1,
                    RecordsPorPagina = 5,
                    CantidadTotalRecords = ListaPokemon.Count
                };

            }

            if (Request.QueryString["pagina"] != null && Request.QueryString["recordsPorPagina"] != null)
            {
                // Parametros de paginacion
                PaginacionViewModel.Pagina = int.Parse(Request.QueryString["pagina"].ToString());
                PaginacionViewModel.RecordsPorPagina = int.Parse(Request.QueryString["recordsPorPagina"].ToString());

                // Listar con parametros de paginacion
                ListaPokemonPagina = ListarConParametros(ListaPokemon, PaginacionViewModel);

                PaginacionRespuesta = new PaginacionRespuesta<Pokemon>
                {
                    Elementos = ListaPokemonPagina,
                    Pagina = PaginacionViewModel.Pagina,
                    RecordsPorPagina = PaginacionViewModel.RecordsPorPagina,
                    CantidadTotalRecords = ListaPokemon.Count,

                };



            }
        }

        public List<Pokemon> ListarConParametros(List<Pokemon> lista, PaginacionViewModel paramPaginacion)
        {
            List<Pokemon> PaginaPokemon;
            
            PaginaPokemon = (from l in lista select l).Skip(paramPaginacion.RecordsASaltar).Take(paramPaginacion.RecordsPorPagina).ToList();

            return PaginaPokemon;
        }




    }
    
}
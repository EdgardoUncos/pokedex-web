using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class PaginacionRespuesta
    {
        // Pagina en la que se encuentra el usuario, x def 1
        public int Pagina { get; set; } = 1;
        public int RecordsPorPagina { get; set; } = 10;
        public int CantidadTotalRecords { get; set; } // cant total de pokemones
        public int CantidadTotalPaginas => (int)Math.Ceiling((double)CantidadTotalRecords / RecordsPorPagina);

        // Cuando clickee un boton se invoque la accion correcta del controlador
        public string BaseURL { get; set; }
    }

    // Lista de genericos, listado de pokemons, usuario. Hereda todo lo de arriba
    public class PaginacionRespuesta<T> : PaginacionRespuesta
    {
        public IEnumerable<T> Elementos { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class PaginacionViewModel
    {
        // Pagina que vamos a mostrar
        public int Pagina { get; set; } = 1;

        // Registros por pagina
        private int recordsPorPagina = 5;

        // Cantidad maxima de registros por pagina, el limite es 50
        private readonly int cantidadMaximaRecordsPorPagina = 50;

        // Propiedad para setear recordsPorPagina, tiene una validacion
        public int RecordsPorPagina
        {
            get { return recordsPorPagina; }
            set
            {
                recordsPorPagina = (value > cantidadMaximaRecordsPorPagina) ?
                    cantidadMaximaRecordsPorPagina : value;
            }
        }

        // Propiedad de solo lectura, que al solicitar RecordsASaltar, no da el calculo de la derecha
        public int RecordsASaltar => recordsPorPagina * (Pagina - 1);
    }
}

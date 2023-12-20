using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAppAleff.Application.Dto
{
    public class AcessosHoraDto
    {
        public int Hora { get; set; }
        public int Quantidade { get; set; }

        public AcessosHoraDto() { }

        public AcessosHoraDto(int hora, int acessos) 
        {
            Hora = hora;
            Quantidade = acessos;
        }
    }
}

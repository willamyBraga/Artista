using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artista.Shared
{
    public class PerfilUsuario
    {
        public const string Administrador = "admin";
        public const string Moderador = "moderador";

        public static List<string> ListaDePerfis => new List<string>()
        {
            Administrador, Moderador
        };
    }
}

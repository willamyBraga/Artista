using Artista.Data.Controlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artista.Data.Model
{
    public class UserLogin
    {
        public UserLogin() { }

        public UserLogin(DadosAutenticado dados)
        {
            Login = dados.nomeLogin;
            Senha = dados.Senha;
            Perfil = dados.Perfil; 
        }

        public int UsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Perfil { get; set; }
      
    }
}

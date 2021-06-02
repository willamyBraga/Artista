using Artista.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Artista.Data.Controlers
{
    public class DadosAutenticado
    {
        private readonly MeUsuarios usuarios;
        private readonly UserLogin tbUsuarios; 

        public DadosAutenticado(MeUsuarios usuario)
        {
            tbUsuarios = new UserLogin();
            usuarios = usuario; 
        }
        public DadosAutenticado(UserLogin novousuario, MeUsuarios metodo)
        {
            tbUsuarios = novousuario;
            ConfirmaSenha = novousuario.Senha;
            usuarios = metodo; 
        }
        public string nomeLogin
        {
            get => tbUsuarios.Login;
            set => tbUsuarios.Login = value; 
        }
        public string Senha
        {
            get => tbUsuarios.Senha;
            set => tbUsuarios.Senha = value;
        }

        public string Perfil
        {
            get => tbUsuarios.Perfil;
            set => tbUsuarios.Perfil = value;
        }
        public string ConfirmaSenha { get; set; }
            
    }
}

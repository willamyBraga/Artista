using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using Artista.Data.Model;

namespace Artista.Data.Controlers
{
    public class Usuario
    {
        private readonly StringConexao _conexao;

        public Usuario(StringConexao conexao)
        {
            _conexao = conexao;
        }

        public static string LoginNome { get; set; }
        public static string Senha { get; private set; }
        public static string UsuarioPerfil { get; private set; }
        public static bool UserLogado { get; private set; }

        public static void Logout()
        {
            UserLogado = false;
            LoginNome = string.Empty;
            UsuarioPerfil = string.Empty; 
            Senha = string.Empty;
        }

        public async Task<UserLogin> BuscarLogin(string login)
        {
            UserLogin user = null;

            user = await BuscarusuarioParaLogin(login);
            LoginNome = user.Login;
            Senha = user.Senha;
            UsuarioPerfil = user.Perfil; 
            UserLogado = true;

            return user;
        }

        public async Task<List<UserLogin>> LerDados()
        {
            List<UserLogin> usuarios = new List<UserLogin>();

            DataTable dt = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT *FROM tbl_usuario ", conn);
            da.Fill(dt);
            foreach (DataRow item in dt.Rows)
            {
                UserLogin registro = new UserLogin
                {
                    Login = item["loginUsuario"].ToString(),
                    Senha = item["loginUsuario"].ToString()
                };
                usuarios.Add(registro);
                
            }
            return usuarios;
        }

        public async Task<UserLogin> BuscarusuarioParaLogin(string login)
        {
            List<UserLogin> usuarios = new List<UserLogin>();
            UserLogin registro = null;

            string sql = $"SELECT * FROM tbl_usuario WHERE loginUsuario = '{login}'";
            NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao);
            conn.Open();
            NpgsqlCommand query = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader reader = await query.ExecuteReaderAsync();

            if (reader.Read())
            {
                registro = new UserLogin()
                {
                    Login = reader["loginUsuario"].ToString(),
                    Senha = reader["senha"].ToString(),
                    Perfil = reader["Perfil"].ToString()
                    
                };
            }
            return await Task.FromResult(registro);
        }
        public async Task<DataTable> AdicionarDataTable(DataTable dt)
        {
            NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao);
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT *FROM tbl_usuario ", conn);
            //criar os comandos  automatico 
            NpgsqlCommandBuilder cmbuilder = new NpgsqlCommandBuilder(da);
            da.Fill(dt);
            da.Update(dt);
            conn.Close();
            return await Task.FromResult(dt);
        }
             
    }
}

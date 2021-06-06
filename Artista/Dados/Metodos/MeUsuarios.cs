using System.Threading.Tasks;
using Npgsql;
using System.Data;

namespace Artista.Data.Controlers
{
    public class MeUsuarios
    {
        private readonly StringConexao _conexao;

        public MeUsuarios(StringConexao conexao)
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
        public async Task<DataTable> lerDadosdatatable()
        {
            DataTable dt = new DataTable();
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
        
        public async Task<DataTable> dtLogin(string login)
        {
            DataTable dt = new DataTable(); 
            string sql = $"SELECT * FROM tbl_usuario WHERE Senha = '{login}'";
            NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao);
            conn.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            da.Fill(dt);
           //controle de nivel de usuario
            DataRow[] row = dt.Select("Perfil = 'admin' OR Perfil = 'moderador'");
            UsuarioPerfil = row[0]["Perfil"].ToString();
            Senha = row[0]["Senha"].ToString(); 
            
            return await Task.FromResult(dt);
        }
    }
}

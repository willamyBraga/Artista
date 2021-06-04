using System.Data;
using System.Threading.Tasks;
using Npgsql;


namespace Artista.Data.Controlers
{
    public class MeArtistas
    {
        private readonly StringConexao _conexao;

        public MeArtistas(StringConexao conexao)
        {
            _conexao = conexao;
        }


        //ler dados com datatable
        public async Task<DataTable> LerDadosDataTable()
        {
            DataTable dt = new DataTable();
            NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT *FROM tbl_artista ", conn);
            da.Fill(dt);
           
            return await Task.FromResult(dt);                     
        }

        //faz as operações crud com datatable
        public async Task<DataTable> AdicionarDataTable(DataTable dt)
        {
            NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao);
            conn.Open(); 
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("SELECT *FROM tbl_artista ORDER BY NomeArtista", conn);
            //criar os comandos  automatico 
            NpgsqlCommandBuilder cmbuilder = new NpgsqlCommandBuilder(da);
            da.Fill(dt);
            da.Update(dt);
            conn.Close(); 
            return await Task.FromResult(dt);
        }            
    }
}

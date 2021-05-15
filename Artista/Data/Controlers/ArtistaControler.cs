using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Artista.Data.Model;
using Npgsql;


namespace Artista.Data.Controlers
{
    public class ArtistaControler
    {
        private readonly StringConexao _conexao;

        public ArtistaControler(StringConexao conexao)
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

        //adicionar dados com datatable
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

        //mostrar/ler dados
        public async Task<List<ArtistaModel>> LerDados()
        {
            List<ArtistaModel> artistas = new List<ArtistaModel>();

            using (NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao))
            {
                string sql = "SELECT *FROM tbl_artista ORDER BY NomeArtista";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conn);
                conn.Open();

                NpgsqlDataReader reader = await comando.ExecuteReaderAsync();

                while (reader.Read())
                {
                    ArtistaModel registros = new ArtistaModel
                    {
                        ArtistaId = int.Parse(reader["artistaId"].ToString()),
                        ArtistaNome = reader["NomeArtista"].ToString(),
                        MusicaArtista = reader["MusicaArtista"].ToString()

                    };

                    artistas.Add(registros);
                }
                conn.Dispose();
            }

            return artistas;
        }
        //adicionar novos registros
        public async Task Adicionar(ArtistaModel artista)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao))
            {
                string sql = "INSERT INTO tbl_artista(NomeArtista, MusicaArtista) VALUES (@nome, @musica)";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conn);
                comando.Parameters.AddWithValue("@nome", artista.ArtistaNome);
                comando.Parameters.AddWithValue("@musica", artista.MusicaArtista);
                conn.Open();
                await comando.ExecuteNonQueryAsync();
                conn.Dispose();
            }
        }

        //buscar 
        public async Task<ArtistaModel> BuscarArtista(int artistaid)
        {
            ArtistaModel registro = new ArtistaModel();
            using (NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao))
            {
                string sql = "SELECT *FROM tbl_artista WHERE artistaId =" + artistaid;
                NpgsqlCommand comando = new NpgsqlCommand(sql, conn);
                conn.Open();
                NpgsqlDataReader comReader = await comando.ExecuteReaderAsync();

                if (comReader.Read())
                {
                    registro.ArtistaId = Convert.ToInt32(comReader["artistaId"].ToString());
                    registro.ArtistaNome = comReader["NomeArtista"].ToString();
                    registro.MusicaArtista = comReader["MusicaArtista"].ToString();
                }
                conn.Close();
                conn.Dispose();
            }
            return registro;
        }
        //editar 
        public async Task Editar(ArtistaModel artista, int artistaid)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao))
            {
                string sql = "UPDATE tbl_artista SET NomeArtista=@nome, MusicaArtista=@musica WHERE artistaId=@id";
                NpgsqlCommand comando = new NpgsqlCommand(sql, conn);
                comando.Parameters.AddWithValue("@nome", artista.ArtistaNome);
                comando.Parameters.AddWithValue("@musica", artista.MusicaArtista);
                comando.Parameters.AddWithValue("@id", artistaid);

                conn.Open();
                await comando.ExecuteNonQueryAsync();
                conn.Close();
                conn.Dispose();
            }
        }
        //excluir dados
        public async Task Excluir(object artistaid)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao))
            {
                string sql = "DELETE FROM tbl_artista WHERE artistaId= " + artistaid;

                NpgsqlCommand comando = new NpgsqlCommand(sql, conn);
                conn.Open();
                await comando.ExecuteNonQueryAsync();
                conn.Close();
                conn.Dispose();
            }
        }

    }
}

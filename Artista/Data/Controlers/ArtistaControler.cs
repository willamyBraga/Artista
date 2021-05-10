using System;
using System.Collections.Generic;
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
        //mostrar/ler dados
        public async Task<List<ArtistaModel>> LerDados()
        {
            List<ArtistaModel> artistas = new List<ArtistaModel>();

            using(NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao))
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
            using(NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao))
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
        //excluir dados
        public async Task Excluir(int artistaid)
        {
            using(NpgsqlConnection conn = new NpgsqlConnection(_conexao.conexao))
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

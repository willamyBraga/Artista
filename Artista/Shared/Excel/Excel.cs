using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.IO.Packaging;
using ClosedXML.Excel;
using System.Data;
using artista = Artista.Data.Controlers;
using js = Microsoft.JSInterop.IJSRuntime;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Mvc;

namespace Artista.Shared.Excel
{
    public class Excel
    {
        public async Task ExcelFile(DataTable dtfront)
        {
            //caminho do arquivo
            string FilePath = "artista.xlsx";
            XLWorkbook wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("artista");
            ws.Cell(1, 1).Value = "Nome Artista";
            ws.Cell(1, 2).Value = "Musica";
            ws.Cell(1, 3).Value = "Autor";
            ws.Cell(1, 4).Value = "Observações";
            ws.Cell(1, 5).Value = "Data de criação";

            int index = 1;
            foreach (DataRow item in dtfront.Rows)
            {
                ws.Cell(index + 1, 1).Value = item["NomeArtista"];
                ws.Cell(index + 1, 2).Value = item["MusicaArtista"];
                ws.Cell(index + 1, 3).Value = item["AutorMusica"];
                ws.Cell(index + 1, 4).Value = item["ObservacaoMusica"]; //DataCriacao
                ws.Cell(index + 1, 5).Value = item["DataCriacao"];

                ws.Columns().AdjustToContents();

                index++;
            }            
            ws.Columns().AdjustToContents();
            wb.SaveAs(FilePath);
            await Task.FromResult(FilePath); 
        }
    }
}

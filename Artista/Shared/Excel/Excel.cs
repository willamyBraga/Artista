using System;
using System.Threading.Tasks;
using System.IO;
using ClosedXML.Excel;
using System.Data;
using Microsoft.JSInterop;

namespace Artista.Shared.Excel
{
    public class Excel
    {
        private readonly IJSRuntime js;

        public Excel(IJSRuntime js)
        {
            this.js = js;
        }

        public  async Task ExcelFile(DataTable dtfront)
        {
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
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                var content = stream.ToArray();
                var fileName = "artista.xlsx";
                await js.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(content));
            }
        }
    }

}


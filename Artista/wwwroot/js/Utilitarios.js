
//função responsavel pela impressão no brawser 
function Impressao() {
   var conteudo = document.getElementById('print').innerHTML;
   tela_impressao = window.open('about:blank');
   tela_impressao.document.write(conteudo);
   tela_impressao.window.print();
   tela_impressao.window.close();
}


//saveAsFile, função responsavel pela geração do excel 
window.saveAsFile = function (fileName, byteBase64) {
	var link = this.document.createElement('a');
	link.download = fileName;
	link.href = "data:application/octet-stream;base64," + byteBase64;
	this.document.body.appendChild(link);
	link.click();
	this.document.body.removeChild(link);
}
//formatar data formato dd/MM/yyy
function formataData(val) {
	var pass = val.value;
	var expr = /[0123456789]/;

	for (i = 0; i < pass.length; i++) {
		var lchar = val.value.charAt(i);
		var nchar = val.value.charAt(i + 1);

		if (i == 0) {
		
			if ((lchar.search(expr) != 0) || (lchar > 3)) {
				val.value = "";
			}

		} else if (i == 1) {

			if (lchar.search(expr) != 0) {
			
				var tst1 = val.value.substring(0, (i));
				val.value = tst1;
				continue;
			}

			if ((nchar != '/') && (nchar != '')) {
				var tst1 = val.value.substring(0, (i) + 1);

				if (nchar.search(expr) != 0)
					var tst2 = val.value.substring(i + 2, pass.length);
				else
					var tst2 = val.value.substring(i + 1, pass.length);

				val.value = tst1 + '/' + tst2;
			}

		} else if (i == 4) {

			if (lchar.search(expr) != 0) {
				var tst1 = val.value.substring(0, (i));
				val.value = tst1;
				continue;
			}

			if ((nchar != '/') && (nchar != '')) {
				var tst1 = val.value.substring(0, (i) + 1);

				if (nchar.search(expr) != 0)
					var tst2 = val.value.substring(i + 2, pass.length);
				else
					var tst2 = val.value.substring(i + 1, pass.length);

				val.value = tst1 + '/' + tst2;
			}
		}

		if (i >= 6) {
			if (lchar.search(expr) != 0) {
				var tst1 = val.value.substring(0, (i));
				val.value = tst1;
			}
		}
	}

	if (pass.length > 10)
		val.value = val.value.substring(0, 10);
	return true;
}
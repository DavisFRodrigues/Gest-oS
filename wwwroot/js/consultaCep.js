$(document).ready(function () {


	$('#cep').blur(function (e) {
		var cep = $('#cep').val();
		$('#aguarde').show();
		var url = "https://viacep.com.br/ws/" + cep + "/json/";
		var retorno = pesquisaCEP(url);

		console.log(url);
		//function loading_show('#show') {
		//	$('#load').fadeIn('fast');
		//}

		//function loading_hide() {
		//	$('#load').fadeOut('fast');
		//}
	});

	function pesquisaCEP(endereco) {

		$.ajax({
			type: "GET",
			url: endereco,
			async: true,
			beforeSend: function () {//Chama a função para mostrar a imagem gif de loading antes do carregamento
				$('#aguarde').show();
			}
		}).done(function (data) {



			$('#bairro').val(data.bairro);
			$('#endereco').val(data.logradouro);
			$('#cidade').val(data.localidade);
			$('#estado').val(data.uf);
			$('#aguarde').hide();

		}).fail(function () {
			console.log("erro");
			$('#aguarde').hide();
		});
	}
	

});


	//================================================================================

	//ANTIGA NÃO MEXER
	//$('#cep').blur(function (e) {
	//	var cep = $('#cep').val();
	//	var url = "https://viacep.com.br/ws/" + cep + "/json/";
	//	var retorno = pesquisaCEP(url);

	//});

	//function pesquisaCEP(endereco) {

	//	$.ajax({
	//		type: "GET",
	//		url: endereco,
	//		async: false,

	//	}).done(function (data) {

	//		$('#bairro').val(data.bairro);
	//		$('#endereco').val(data.logradouro);
	//		$('#cidade').val(data.localidade);
	//		$('#estado').val(data.uf);

	//	}).fail(function () {
	//		console.log("erro");
	//	});
	//}

//);

$(function () {
	$('#tel').mask("(99)99999-9999");
	$('#cep').mask("99999-999");

});
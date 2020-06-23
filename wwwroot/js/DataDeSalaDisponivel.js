$(document).ready(function () {
  $('#IdSalaBusca').change(function () {

         var IdSala = $('#IdSalaBusca').val();
        $('#DataLocacao').change(function () {
            
            var DtLocacao = $('#DataLocacao').val();
            
            console.log(IdSala);
            console.log(DtLocacao);
            if (IdSala > 0) {

                $.ajax({
                    url: '/SiteGestaoS/LocaSalas/DataLocacaoSala/',
                    type: "GET",
                    dataType: "json",
                    data: {
                        idSalaData: IdSala,
                        DataLocacao: DtLocacao
                    },
                    success: function (data) {


                        if (data.length > 0) {
                            console.log(data[0].DATA_DISPONIVEL);

                            var Disponivel = data[0].DATA_DISPONIVEL


                            //if (Disponivel > quantiEqui) {



                            //}
                            //else {

                            //var dataL = new Date(Disponivel).toLocaleString('pt-BR').split(' ');
                            //alert('A próxima data que esta sala estará liberada é: ' + dataL[0]);

                            //}


                            $(function () {
                                var dataL = new Date(Disponivel).toLocaleString('pt-BR').split(' ');
                                $('#TextAlertData').text("A próxima data que esta sala estará liberada é: " + dataL[0]);
                                $('#AlertData').modal('show');
                                //$('#DataLocacao').empty();
                                $('#DataLocacao').val()+1;

                            });




                        }


                    },


                    error: function (data) {
                        // em caso de erro...
                        console.log(error);
                    },

                    complete: function () {
                        // ao final da requisição...
                        $("#carregando").hide();
                    }


                });
            }

        });
  });

    $("#DataLocacao").change(function () {
        alert($("#DataLocacao").datepicker('getDate'));
    });
});








$('#IdSalaBusca').change(function () {

    var IdSala = $('#IdSalaBusca').val();
    $('#DataFim').change(function () {

        var DtLocacao = $('#DataFim').val();

        console.log(IdSala);
        console.log(DtLocacao);
        if (IdSala > 0) {

            $.ajax({
                url: '/SiteGestaoS/LocaSalas/DataLocacaoSalaFim/',
                type: "GET",
                dataType: "json",
                data: {
                    idSalaData: IdSala,
                    DtFimLocacaoS: DtLocacao
                },
                success: function (data) {


                    if (data.length > 0) {
                        console.log(data[0].DATA_DISPONIVEL);

                        var Disponivel = data[0].DATA_DISPONIVEL


                        //if (Disponivel > quantiEqui) {



                        //}
                        //else {

                        //var dataL = new Date(Disponivel).toLocaleString('pt-BR').split(' ');
                        //alert('A data fim que está sala estará liberada é: ' + dataL[0]);

                        $(function () {
                            var dataL = new Date(Disponivel).toLocaleString('pt-BR').split(' ');
                            $('#TextAlertData').text("A data fim que está sala estará liberada é: " + dataL[0]);
                            $('#AlertData').modal('show');
                            //$('#DataLocacao').empty();
                            //$('#DataLocacao').val() + 1;

                        });

                    }


                },


                error: function (data) {
                    // em caso de erro...
                    console.log(error);
                },

                complete: function () {
                    // ao final da requisição...
                    $("#carregando").hide();
                }


            });
        }

    });
});
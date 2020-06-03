$(document).ready(function () {
    $('#IdEquipamento').change(function () {

        $('#QtdeEqui').empty();
        $('#QtdeEqui').change(function () {
            var IdEquipamentoQtde = $('#IdEquipamento').val();
            var quantiEqui = $('#QtdeEqui').val();

            if (IdEquipamentoQtde > 0) {

                $.ajax({
                    url: '/LocaEquipamentoes/QtdeEquipamento/',
                    type: "GET",
                    dataType: "json",
                    data: {
                        IdEquipamentoQtde: IdEquipamentoQtde
                    },
                    success: function (data) {


                        if (data.length > 0) {
                            console.log(data[0].QTDE_DISPONIVEL);

                            var Disponivel = data[0].QTDE_DISPONIVEL


                            if (Disponivel > quantiEqui) {



                            }
                            else {

                               
                                $(function () {

                                    $('#TextAlert').text("A quantidade de equipamento disponível é: " + Disponivel);
                                    $('#Alert').modal('show');
                                    $('#QtdeEqui').val(Disponivel);
                                    
                                });
                                
                
                                                               
                            }

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
});
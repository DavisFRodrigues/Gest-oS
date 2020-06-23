$(document).ready(function () {
    $('#IdFilialBusca').change(function () {
        var idFilial = $('#IdFilialBusca').val();
        
        if (idFilial > 0) {

            $.ajax({
                url: '/SiteGestaoS/LocaEquipamentoes/ObterSala/',
                type: "GET",
                dataType: "json",
                data: {
                    idFilial: idFilial
                },
                success: function (data) {

                    if (data.length > 0) {
                        
                        for (var i = 0; i < data.length; i++) {
                           
                            $('#IdSalaBusca').empty();
                           // $('#IdSalaBusca').append('<option value="' + data[i].id + '">' + data[i].nome + '</option>');

                            $("#IdSalaBusca").append('<option value=0>Selecione uma Sala</option>');
                            $(data).each( function () {
                                $("#IdSalaBusca").append($('<option></option>', { value: this.id }).html(this.nome));
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

$(document).ready(function () {
    $('#IdFilialBuscaEdit').change(function () {
        var idFilial = $('#IdFilialBuscaEdit').val();

        //if (idFilial > 0) {

        $.ajax({
            url: '/SiteGestaoS/LocaEquipamentoes/ObterSala/',
            type: "GET",
            dataType: "json",
            data: {
                idFilial: idFilial
            },
            success: function (data) {

                if (data.length > 0) {

                    for (var i = 0; i < data.length; i++) {

                        $('#IdSalaBuscaEdit').empty();
                        // $('#IdSalaBusca').append('<option value="' + data[i].id + '">' + data[i].nome + '</option>');

                        $("#IdSalaBuscaEdit").append('<option value=0>Selecione uma Sala</option>');
                        $(data).each(function () {
                            $("#IdSalaBuscaEdit").append($('<option></option>', { value: this.id }).html(this.nome));
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
        //}

    });

});
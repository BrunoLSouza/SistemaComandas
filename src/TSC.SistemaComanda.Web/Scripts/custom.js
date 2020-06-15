
    function AdicionarProduto(idComanda, idProduto) {

        if (idComanda != null && idComanda != undefined && idComanda > 0
            && idProduto != null && idProduto != undefined && idProduto> 0) {

            var url = "/Home/AdicionarProdutos";

            $.ajax({
                type: "POST",
                url: url,
                data: { idComanda: idComanda, idProduto: idProduto },
                success: function (data, textStatus, jQxhr) {
                    Alerta("containerAlerta", data)
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    Alerta("containerAlerta", data)
                }
            });

        }
        else {
        
            }

        //$("#alerta").fadeTo(2000, 500).slideUp(500, function () {
        //    $("#alerta").slideUp(500);
        //});

        //$("#alerta").delay(4000).slideUp(200, function () {
        //    $(this).alert('close');
        //});

    }

    function Alerta(idDiv, data) {

    if (data != null && data != undefined) {

        var alert = '<div id="alerta" class="alert alert-TIPOALERTA" role="alert"><strong>MENSAGEM</div>';

        switch (data.Tipo.toLowerCase()) {
            case 'sucesso':
                alert = alert.replace("TIPOALERTA", "success")
                break;
            case 'erro':
                alert = alert.replace("TIPOALERTA", "danger")
                break;
            case 'aviso':
                alert = alert.replace("TIPOALERTA", "info")
                break;
            case 'atencao':
                alert = alert.replace("TIPOALERTA", "warning")
                break;
            default:
                alert = alert.replace("TIPOALERTA", "info")
        };

        alert = alert.replace("MENSAGEM", data.Mensagem);

        $("#" + idDiv).empty();
        $("#" + idDiv).append(alert);

        $("#alerta").fadeTo(1000, 500).slideUp(500, function () {
            $("#alerta").slideUp(500);
        });

    }
}
     
    function ConfirmarFechamentoComanda(parametro) {

        var titulo = "Aviso";
        var mensagem = "Tem certeza que deseja fechar essa comanda?";

        if (parametro != "" && parametro != null && parametro != undefined) {
            var param = parametro

            ModalConfirmacao(titulo, mensagem, "FecharComanda?idComanda=" + parametro);
        }
        
    }

    function ModalConfirmacao(titulo, mensagem, rota) {

        $("#modalConfirmacaoSrc .modal-title").empty();
        $("#modalConfirmacaoSrc .modal-title").append(titulo);

        $("#modalConfirmacaoSrc .modal-body").empty();
        $("#modalConfirmacaoSrc .modal-body").append(mensagem);

        var conteudoFooter = $("#modalConfirmacaoSrc .modal-footer").html();
        conteudoFooter = conteudoFooter.replace("rota", rota);
        $("#modalConfirmacaoSrc .modal-footer").empty();
        $("#modalConfirmacaoSrc .modal-footer").append(conteudoFooter);

        $('#modalConfirmacaoSrc').modal("show");
    }

    function FechaComanda(idComanda) {
        $('#modalConfirmacaoSrc').modal("hide");

        if (idComanda != undefined) {

            var url = "/Home/AdicionarProdutos";

            $.ajax({
                type: "POST",
                url: url,
                data: { idComanda: idComanda},
                success: function (data, textStatus, jQxhr) {
                    alerta("containerAlerta", data)
                },
                error: function (jqXhr, textStatus, errorThrown) {
                    alerta("containerAlerta", data)
                }
            });
        }
}



   



  


$(document).ready(function () {

    var $JQuery = jQuery.noConflict();//n da conflito no jquery
    CarregaProduto();

    //$("#FormaPagamento").click(function ()//se ocorrer tudo bem chama modal
    //{
      
    //});

    $("#SalvarFechamento").click(function ()//se ocorrer tudo bem chama modal
    {
        jQuery.ajax
            ({
                type: "POST",
                url: "/Fluxo_Venda/FecharComanda/",//chama metodo do controler '/controler/metodo/'
                dataType: "json",
                data: {
                    Id_Venda: $("#Venda").val(),
                    FormaPagamento: $("#FormaPgse").val()
                },
                success: function (data) {
                },
                error: function (request, status, erro) {
                },
                complete: function (jqXHR, textStatus) {
                    window.location.reload();
                }
            });   
    });

    $("#NovaVenda").click(function ()//se ocorrer tudo bem chama modal
    {
        var mesa = prompt("Deseja abrir uma nova venda? Informe a mesa!");
        if (mesa != null)
        {          
            if (!isNaN(mesa))
            {
                PostFluxoVenda(mesa);
            }
            else
            {
                alert("Só aceita números!");
            }
        }
    });

	$("#btnLogin").click(function ()//se ocorrer tudo bem chama modal
	{
		console.log("aquibtn"); 
		if (Session["Erro"] == 0)
		{
		console.log("entrou no if"); 
			window.alert("Login ou senha inválido.");
		}
    });
	
    $("#AdicionarItem").click(function () {
        jQuery.ajax
            ({
                type: "POST",
                url: "/Fluxo_Venda/SalvarItens/",//chama metodo do controler '/controler/metodo/'
                dataType: "json",
                data: {
                    Id_Venda: $("#IdVenda").val(),
                    Id_Produto: $("#produto").val(),
                    Quantidade: $("#Quantidade").val()
                },
                success: function (data) {
                    $("#myModal").modal("hide");

                },
                error: function (request, status, erro) {

                },
                complete: function (jqXHR, textStatus) {
                    $("#Quantidade").val("");
                }
            });
    
});

function PostFluxoVenda(mesa) {
        var dados = $("Form"/*id do form da tela de cadastro*/).serialize();
        jQuery.ajax
            ({
                type: "POST",
                url: "/Fluxo_Venda/AbreVenda",//chama metodo do controler '/controler/metodo/'
                dataType: "json",
                data: { mesa: mesa},
                success: function (data)
                {
                    window.location.reload();//Atualiza tela
                },
                
            });
    }

    function CarregaProduto()//carrega item para por no drop
    {
        $.ajax
            ({      //drop no produto para fluxo_item..
                url: "/Produto/DropSelecionarProduto",
                success: function (data)
                {
                    $("#produto").empty();
                    $.each(data, function (i, element)
                    {                    
                        $("#produto").append("<option value=" + element.Id + ">" + element.Descricao + "</option>");
                    }
                    )
                }
            });
    }   
});

function AdicionarItem(Id) {
    $("#IdVenda").val(Id);
    $("#myModal").modal("show");
}

function VerItem(Id)
{
    $("#myModal2").modal("show");
   var totalzao = 0;
    //Limpa tabela
    $("#tblAcao > tbody").empty();
    $("#totalComanda").val("");

    $.ajax({
        type: "GET",
        url: "/Fluxo_Venda/CarregaTodaComanda",
        dataType: "json",
        data: { Id_Venda: Id },
        success: function (data) {
            $.each(data, function (i, element) {
                $("#tblAcao > tbody").append
                    (
                    "<tr>" +
                         "<td>" + element.Descricao + "</td> " +
                         "<td>" + element.Quantidade + "</td>" +
                         "<td>" + element.Valor + "</td> " +
                         "<td>" + element.Total + "</td> " +
                    "</tr>"
                    );
                totalzao = totalzao + element.Total;
            });
        },
        complete: function (jqXHR, textStatus)
        {
            $("#totalComanda").val(parseFloat(totalzao.toFixed(2)));
        }
    });
}

function FecharStatus(Id_Venda)
{
    //Chama um modal que ja vem com <select> 

    $("#Venda").val(Id_Venda);
    $("#myModal3").modal("show");

//ajax vai para o click do salvar da forma do pg
   

}


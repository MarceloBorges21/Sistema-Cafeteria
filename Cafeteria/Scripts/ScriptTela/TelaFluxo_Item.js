$(document).ready(function (){
    var $JQuery = jQuery.noConflict();//n da conflito no jquery

    var Id = $("#Id").val();
    //verifica se recebeu id
    if (Id > 0)
    {
        CarregaProduto(Id);
        CarregaDados(Id);
        CarregaFluxo_Venda(Id);
        $("input").prop("disabled", true);

        $("#Editar").prop("disabled", false);
        $("#Excluir").prop("disabled", true);
        $("#ValidaCadastro").prop("disabled", true);
    }
    else
    {
        //formulario novo

        CarregaProduto(0);
        CarregaFluxo_Venda(0);
        $("#Excluir").prop("disabled", true);
        //$("#ValidaCadastro").prop("disabled", false);
        $("#Editar").prop("disabled", true);
    }

    $("#Salvar").click(function ()//id=Salvar, chama o metodo salvar no ajax
    {
        console.log("no salvar");
        Salvar();
    });

    $("#ValidaCadastro").click(function () {
        console.log("valida");
        validaDados();

        if (validaDados()) {
            $("#myModal").modal("show");
        }
        else {
            $("#myModal").modal("hide");
        }
	});

	

    function validaDados() {
        var mensagem = "";

        if ($("#fluxo_venda").val() == null || $("#fluxo_venda").val() == "") {
            $("#fluxo_venda").css({ "border-color": "#F00", "padding": "1px" });
            mensagem = "1";
        }
        else {
            $("#fluxo_venda").css({ "border-color": "#blue", "padding": "1px" });
        }

        if ($("#produto").val() == null || $("#produto").val() == "") {
            $("#produto").css({ "border-color": "#F00", "padding": "1px" });
            mensagem = "1";
        }
        else {
            $("#produto").css({ "border-color": "#blue", "padding": "1px" });
        }

        if ($("#Quantidade").val() == null || $("#Quantidade").val() == "") {
            $("#Quantidade").css({ "border-color": "#F00", "padding": "1px" });
            mensagem = "1";
        }
        else {
            $("#Quantidade").css({ "border-color": "#blue", "padding": "1px" });
        }

        if (mensagem != "")//se a variavel mensagem tiver conteudo, ou seja, se tiver 
        {
            $("#resposta").addClass("alert alert-danger");
            $("#resposta").html("Verifique o formulario, existem campos obrigatorios!");
            $("#resposta").show();//mostra a div de resposta
            return false;
        }
        else//se não...
        {
            $("#resposta").html("");
            $("#resposta").hide();
            return true;
        }
    }

    function CarregaProduto(produto)//carrega item para por no drop
    {
        $.ajax
            ({      //drop no produto para fluxo_item..
                url: "/Produto/DropSelecionarProduto",
                success: function (data)
                {
                               
                    $("#produto").empty();

                    $.each(data, function (i, element)
                    {
                        if (produto > 0)
                        {
                            if (produto == element.Id)
                            {
                                $("#produto").append("<option value=" + element.Id + " selected>" + element.Descricao + "</option>");
                            }
                        }
                        $("#produto").append("<option value=" + element.Id + ">" + element.Descricao + "</option>");
                    }
                )}
            });
    }

    function CarregaDados(Id) {
        $.ajax
            ({
                url: "/Fluxo_Item/CarregaDados/" + Id,
                success: function (data) {
                    $.each(data, function (i, element) {

                        $("#Id").val(element.Id);
                        $("#Id_Fluxo").val(element.Id_Fluxo);
                        $("#Id_Produto").val(element.Id_Produto);
                        $("#Quantidade").val(element.Quantidade);
                    });
                }
            });
    }

    function Salvar()
    {
        if (Id > 0)
        {
            Put(Id);
        }
        else
        {
            Post();

        }
    }

    function Post() {
        var dados = $("Form"/*id do form da tela de cadastro*/).serialize();
        jQuery.ajax
            ({
                type: "POST",
                url: "/Fluxo_Item/SalvarAjax/",//chama metodo do controler '/controler/metodo/'
                dataType: "json",
                data: dados,
                success: function (data) {
                    $("#myModal").modal("hide");//fecha o modal de confirmaçõa

                    //retorna uma resposta de sucesso na 'div resposta'
                    $("#resposta").addClass("alert alert-success");
                    $("#resposta").html("Registro salvo com sucesso");
                    $("#resposta").show();

                    //some com a div de resposta apos 5seg
                    setTimeout(function () {
                        $("#resposta").fadeOut("fast");
                        window.location.assign("/Fluxo_Item/Index");
                    }, 2000);

                },
                error: function (request, status, erro) {
                    $("#resposta").addClass("alert alert-danger");
                    $("#resposta").html("Registro NÃO FOI SALVO.");
                    $("#resposta").show();
                },
                complete: function (jqXHR, textStatus) { }//mostra oq vc quiser assim q finalizar tudo
            });
    }

    function Put() {
        jQuery.ajax
            ({
                type: "POST",
                url: "/Fluxo_Item/EditarAjax/",//chama metodo do controler '/controler/metodo/'
                dataType: "json",
                data: {
                    Id: $("#Id").val(),
                    Id_Fluxo: $("#Id_Fluxo").val(),
                    Id_Produto: $("#Id_Produto").val(),
                    Quantidade: $("#Quantidade").val()
                },
                success: function (data) {
                    $("#myModal").modal("hide");//fecha o modal de confirmaçõa

                    //retorna uma resposta de sucesso na 'div resposta'
                    $("#resposta").addClass("alert alert-success");
                    $("#resposta").html("Registro salvo com sucesso");
                    $("#resposta").show();

                    //some com a div de resposta apos 5seg
                    setTimeout(function () {
                        $("#resposta").fadeOut("fast");
                        window.location.assign("/Fluxo_Item/Index");
                    }, 2000);

                },
                error: function (request, status, erro) {
                    $("#resposta").addClass("alert alert-danger");
                    $("#resposta").html("Registro NÃO FOI SALVO.");
                    $("#resposta").show();
                },
                complete: function (jqXHR, textStatus) { }//mostra oq vc quiser assim q finalizar tudo
            });
    }

    function Excluir() {

        jQuery.ajax
            ({
                type: "POST",
                url: "/Fluxo_Item/ExcluirAjax/",//chama metodo do controler '/controler/metodo/'
                dataType: "json",
                data: {
                    Id: $("#Id").val()

                },
                success: function (data) {
                    window.location.assign("/Fluxo_Item/Index");
                }
            });
    }
});

	function FecharStatus() {
		jQuery.ajax
			({
				type: "POST",
				url: "/Login/Sair/",//chama metodo do controler '/controler/metodo/'
				dataType: "json",
				data: {},
				success: function (data) {
				},
				error: function (request, status, erro) {
				},
				complete: function (jqXHR, textStatus) {
					window.location.reload();
				}
			}); 

   

    

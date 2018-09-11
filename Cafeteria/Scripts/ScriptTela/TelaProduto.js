$(document).ready(function () {
var $JQuery = jQuery.noConflict();//n da conflito no jquery

    var Id = $("#Id").val();
    //verifica se recebeu id
    if (Id > 0) {
        CarregaDados(Id);

        //desabilitar inputs
        $("input").prop("disabled", true);

        $("#Editar").prop("disabled", false);
        $("#Excluir").prop("disabled", true);
        $("#ValidaCadastro").prop("disabled", true);
    }
    else {
        //formulario novo
        
        $("#Excluir").prop("disable", true);
        $("#ValidaCadastro").prop("disable", false);
        $("#Editar").prop("disable", true);
    }



    $("#Editar").click(function ()
    {
        //desabilitar inputs
        $("#Valor").prop("disabled", false);
        $("#ValidaCadastro").prop("disabled", false);
        $("#Editar").prop("disabled", true);

        $("#Excluir").prop("disabled", false);

       
    });

    $("#Salvar").click(function ()//id=Salvar, chama o metodo salvar no ajax
    {
        Salvar();            
    });

    $("#ValidaCadastro").click(function ()//se ocorrer tudo bem chama modal
    {
        validaDados();

        if (validaDados())
        {
            $('#myModal').modal('show');
        }
        else
        {
            $('#myModal').modal('hide');
        }
    })

    $("#Excluir").click(function () {
           

        if (confirm("Deseja realmente excluir?") == true) {
            Excluir();
        } else {
            return false;
        }
    });

    function validaDados()//se os campos n estiverem informaçao fica cm borda verm etc..
    {
        var mensagem = "";

        if ($("#Descricao").val() == null || $("#Descricao").val() == "")
        {
            $("#Descricao").css({ "border-color": "#F00", "padding": "1px" });
            mensagem = "1";
        }
        else
        {
            $("#Descricao").css({ "border-color": "#blue", "padding": "1px" });
        }

        if ($("#Valor").val() == null || $("#Valor").val() == "")
        {
            $("#Valor").css({ "border-color": "#F00", "padding": "1px" });
            mensagem = "1";
        }
        else
        {
            $("#Valor").css({ "border-color": "#blue", "padding": "1px" });
        }

        if ($("#Data").val() == null || $("#Data").val() == "")
        {
            $("#Data").css({ "border-color": "#F00", "padding": "1px" });
            mensagem = "1";
        }
        else
        {
            $("#Data").css({ "border-color": "#blue", "padding": "1px" });
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
 

    function CarregaDados(Id)
    {
        $.ajax
            ({
                url: "/Produto/CarregaDados/" + Id,
                success: function (data)
                {                 
                    $.each(data, function (i, element)
                    {
                        
                        $("#Id").val(element.Id);
                        $("#Descricao").val(element.Descricao);
                        $("#Valor").val(element.Valor);
                        $("#Data").val(element.Data);
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

    function Post()
    {
        var dados = $("Form"/*id do form da tela de cadastro*/).serialize();
        jQuery.ajax
            ({
                type: "POST",
                url: "/Produto/SalvarAjax/",//chama metodo do controler '/controler/metodo/'
                dataType: "json",
                data: dados,
                success: function (data)
                {
                    $("#myModal").modal("hide");//fecha o modal de confirmaçõa

                    //retorna uma resposta de sucesso na 'div resposta'
                    $("#resposta").addClass("alert alert-success");
                    $("#resposta").html("Registro salvo com sucesso");
                    $("#resposta").show();

                    //some com a div de resposta apos 5seg
                    setTimeout(function ()
                    {
                        $("#resposta").fadeOut("fast");
                        window.location.assign("/Produto/Index");
                    }, 2000);

                },
                error: function (request, status, erro) {
                    $("#resposta").addClass("alert alert-danger");
                    $("#resposta").html("Registro NÃO FOI SALVO.");
                    $("#resposta").show();
                },
                complete: function (jqXHR, textStatus)
                { }//mostra oq vc quiser assim q finalizar tudo
            });
    }

    function Put() {
        
        jQuery.ajax
            ({
                type: "POST",
                url: "/Produto/EditarAjax/",//chama metodo do controler '/controler/metodo/'
                dataType: "json",
                data: {
                    Id: $("#Id").val(),
                    Valor: $("#Valor").val()
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
                        window.location.assign("/Produto/Index");
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
                url: "/Produto/ExcluirAjax/",//chama metodo do controler '/controler/metodo/'
                dataType: "json",
                data: {
                    Id: $("#Id").val()
                    
                },
               success: function (data) {
                   window.location.assign("/Produto/Index");
               }
            });
    }
});
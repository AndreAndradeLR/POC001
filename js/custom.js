$(document).ready(function () {

    // ---------------------------------------------------------
    // Tabs
    // ---------------------------------------------------------
    $(".tabs").each(function () {

        $(this).find(".tab").hide();
        $(this).find(".tab-menu li:first a").addClass("active").show();
        $(this).find(".tab:first").show();

    });

    $(".tabs").each(function () {

        $(this).find(".tab-menu a").click(function () {

            $(this).parent().parent().find("a").removeClass("active");
            $(this).addClass("active");
            $(this).parent().parent().parent().parent().find(".tab").hide();
            var activeTab = $(this).attr("href");
            $(activeTab).fadeIn();
            return false;

        });

    });


    // ---------------------------------------------------------
    // Toggle
    // ---------------------------------------------------------

    $(".toggle").each(function () {

        $(this).find(".box").hide();

    });

    $(".toggle").each(function () {

        $(this).find(".trigger").click(function () {

            $(this).toggleClass("active").next().stop(true, true).slideToggle("normal");

            return false;

        });

    });


});

// Função única que fará a transação
//function getEndereco(id_cep, id_logradouro, id_bairro, id_cidade, id_uf) {
//    // Se o campo CEP não estiver vazio
//    if ($.trim($("#" + id_cep).val()) != "") {
//        /* 
//        Para conectar no serviço e executar o json, precisamos usar a função
//        getScript do jQuery, o getScript e o dataType:"jsonp" conseguem fazer o cross-domain, os outros
//        dataTypes não possibilitam esta interação entre domínios diferentes
//        Estou chamando a url do serviço passando o parâmetro "formato=javascript" e o CEP digitado no formulário
//        http://cep.republicavirtual.com.br/web_cep.php?formato=javascript&cep="+$("#cep").val()
//        */
//        $.getScript("http://cep.republicavirtual.com.br/web_cep.php?formato=javascript&cep=" + $("#" + id_cep).val().replace(".", "").replace("-", ""), function () {
//            // o getScript dá um eval no script, então é só ler!
//            //Se o resultado for igual a 1
//            if (resultadoCEP["resultado"]) {
//                // troca o valor dos elementos
//                $("#" + id_logradouro).val(unescape(resultadoCEP["tipo_logradouro"]) + " " + unescape(resultadoCEP["logradouro"]));
//                $("#" + id_bairro).val(unescape(resultadoCEP["bairro"]));
//                $("#" + id_cidade).val(unescape(resultadoCEP["cidade"]));
//                $("#" + id_uf).val(unescape(resultadoCEP["uf"]));
//            } else {
//                alert("Endereço não encontrado");
//            }
//        });
//    }
//}



// initialise plugins
$(function () {
    $(".datepicker").datepicker();
    $(".datetimepicker").datetimepicker();

});



$(document).ready(function () {
    $('a[rel="external"]').each(function () {
        $(this).attr('target', '_blank');
    });
});
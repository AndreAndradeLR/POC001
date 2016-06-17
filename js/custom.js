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

// Fun��o �nica que far� a transa��o
//function getEndereco(id_cep, id_logradouro, id_bairro, id_cidade, id_uf) {
//    // Se o campo CEP n�o estiver vazio
//    if ($.trim($("#" + id_cep).val()) != "") {
//        /* 
//        Para conectar no servi�o e executar o json, precisamos usar a fun��o
//        getScript do jQuery, o getScript e o dataType:"jsonp" conseguem fazer o cross-domain, os outros
//        dataTypes n�o possibilitam esta intera��o entre dom�nios diferentes
//        Estou chamando a url do servi�o passando o par�metro "formato=javascript" e o CEP digitado no formul�rio
//        http://cep.republicavirtual.com.br/web_cep.php?formato=javascript&cep="+$("#cep").val()
//        */
//        $.getScript("http://cep.republicavirtual.com.br/web_cep.php?formato=javascript&cep=" + $("#" + id_cep).val().replace(".", "").replace("-", ""), function () {
//            // o getScript d� um eval no script, ent�o � s� ler!
//            //Se o resultado for igual a 1
//            if (resultadoCEP["resultado"]) {
//                // troca o valor dos elementos
//                $("#" + id_logradouro).val(unescape(resultadoCEP["tipo_logradouro"]) + " " + unescape(resultadoCEP["logradouro"]));
//                $("#" + id_bairro).val(unescape(resultadoCEP["bairro"]));
//                $("#" + id_cidade).val(unescape(resultadoCEP["cidade"]));
//                $("#" + id_uf).val(unescape(resultadoCEP["uf"]));
//            } else {
//                alert("Endere�o n�o encontrado");
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
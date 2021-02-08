$(document).ready(function () {
    // Desabilita botão direito do mouse sobre o frame para não poder fazer download
    $('#videoFrame').bind('contextmenu', function () { return false; });

    //$(".menuAulas").css("transition", "max-width 2s")
    $(".menuAulas").css("transition", "max-height .2s")
    //$(".modulo").css("transition", "display 2s");

    // Ajustes finais da aula sendo assistida
    $(".li-aula").each(function (index) {
        if ($(this).hasClass("ativo")) {
            $("#assistindoAulaId").attr("value", $(this).children().attr("id"));
            $("#iFrame").attr("src", PlayVideo($(this).children().attr("name")));
            $("#nomeAula").text($(this).children().text());
            //getMaterialApoio($(this).children().attr("id"));
            getStatusCheckAssistida($(this).children().attr("id"), $("#matriculaId").val());
            getArquivosApoio($(this).children().attr("id"));
        };
    });
});

$(function () {
    // Quando tira o foco de anotações da aula
    $("#validationTextarea").focusout(function () {
        postAnotacoes($("#assistindoAulaId").val(), $("#alunoId").val(), $("#validationTextarea").val());
    });

    // Quando clica em salvar anotações
    $("#btnSalvarAnotacoes").click(function () {
        postAnotacoes($("#assistindoAulaId").val(), $("#alunoId").val(), $("#validationTextarea").val());
    });

    // Quando clica em uma aula
    $(".itemAula").click(function (obj) {
        $("#nomeAula").text($(obj.target).text());

        // Limpa ativo de todas as aulas
        $(".li-aula").each(function (index) {
            $(this).removeClass("ativo");
        });

        // Adiciona ativo à aula clicada
        $(obj.target).parent().addClass("ativo");

        // Obter status de aula assistida (da aula clicada) e habilitar ou não o checkBox
        getStatusCheckAssistida($(obj.target).attr("id"), $("#matriculaId").val());

        // Carregar o video da aula clicada
        $("#iFrame").attr("src", PlayVideo($(obj.target).attr("name")));

        $("#assistindoAulaId").attr("value", $(obj.target).attr("id"));

        // Atualiza status "assistindo" da aula clicada
        postStatusAssistindo($("#assistindoAulaId").val(), $("#matriculaId").val());

        getAnotacoes($(obj.target).attr("id"), $("#alunoId").val());
        //getMaterialApoio($(obj.target).attr("id"));
        getArquivosApoio($(obj.target).attr("id"));

    });

    // Quando clica em Assistida
    $("#checkAssistida").click(function () {
        if ($("#checkAssistida").is(":checked")) {
            $("#" + $("#assistindoAulaId").val()).parent().addClass("realizado");

            // Atualiza status "assistida" da aula clicada
            postStatusAssistida($("#assistindoAulaId").val(), $("#matriculaId").val());
        }
    });
});
function getAnotacoes(aulaId, alunoId) {
    var url = '/pt-br/SalaAulas/GetAnotacoes/';
    var dados = { aulaId: aulaId, alunoId: alunoId }
    $.get(url, dados,
        function (returnedData) {
            $("#validationTextarea").val(returnedData);
        }
    )
};
//function getMaterialApoio(aulaId) {
//    var url = '/pt-br/SalaAulas/GetMaterialApoio/';
//    var dados = { aulaId: aulaId }

//    $("#materialApoio").text('');

//    $.get(url, dados,
//        function (returnedData) {
//            $("#materialApoio").text(returnedData);
//        }
//    )
//};
function getArquivosApoio(aulaId) {
    var url = '/pt-br/SalaAulas/GetArquivosApoio/';
    var dados = { aulaId: aulaId }

    $("#arquivosApoio").text('');

    $.get(url, dados,
        function (returnedData) {
            for (i = 0; i < returnedData.length; i++) {
                $("#arquivosApoio").append('<a href="/pt-BR/SalaAulas/Download?filename=' + returnedData[i] + '">' + returnedData[i].replace("/uploads/", "") + '</a><br>');
            }
        }, "json");

};
function GetAvaliacaoEnabled(aulaId, matriculaId) {
    var url = '/pt-br/SalaAulas/GetAvaliacaoEnabled/';
    var dados = { aulaId: aulaId, matriculaId: matriculaId }

    $.get(url, dados,
        function (returnedData) {
            $(returnedData.nomeBotao).prop('hidden', !returnedData.enabled);
            if (returnedData.enabled) {
                $('#avaliacaoLiberada').modal();
                $('html, body').animate({ scrollTop: '0px' }, 300);
            }
        }, "json");
};
function getStatusCheckAssistida(aulaId, matriculaId) {
    var url = '/pt-br/SalaAulas/GetStatusCheckAssistida/';
    var dados = { aulaId: aulaId, matriculaId: matriculaId }

    $.get(url, dados,
        function (returnedData) {
            $("#checkAssistida").prop("disabled", !returnedData.habilitar);
            $("#checkAssistida").prop("checked", returnedData.assistida);
        }
    )
};
function postAnotacoes(aulaId, alunoId, anotacoes) {
    var url = '/pt-br/SalaAulas/PostAnotacoes/';
    var dados = { aulaId: aulaId, alunoId: alunoId, anotacoes: anotacoes }
    $.post(url, dados,
        function (returnedData) {
            //alert(returnedData);
        }
    )
};

function postStatusAssistindo(aulaId, matriculaId) {
    var url = '/pt-br/SalaAulas/UpdateAssistindoAula/';
    var dados = { aulaId: aulaId, matriculaId: matriculaId };

    $.post(url, dados,
        function (returnedData) {
            //console.log(returnedData);
        }
    );
};

function postStatusAssistida(aulaId, matriculaId) {
    var url = '/pt-br/SalaAulas/UpdateAulaAssistida/';
    var dados = { aulaId: aulaId, matriculaId: matriculaId };

    $.post(url, dados,
        function (returnedData) {
            //alert(returnedData.response);
            //console.log(returnedData.response);
            $("#checkAssistida").prop("disabled", true);
            GetAvaliacaoEnabled($("#assistindoAulaId").val(), $("#matriculaId").val());

            if (returnedData.response == "reload") {
                location.reload();
            }

            if (returnedData.response == "end") {
                alert("Curso finalizado!")
            }
        }
    );
};

function AbrirMenu() {
    $(".btn-avaliacao").css("display", "block")
    $(".menuAulas").css("max-width", "600px");
    $(".menuAulas").css("max-height", "1000px");
    $(".modulo").css("display", "block");
    $(".menuAulas h2").css("display", "block");
    $(".fecharMenu").css("display", "block");
    $(".videoAula").removeClass("col-11");
    $(".videoAula").addClass("col-9");

};

function FecharMenu() {
    $(".btn-avaliacao").css("display", "none")
    $(".menuAulas").css("max-height", "50px");
    $(".menuAulas").css("max-width", "40px");

    $(".modulo").css("display", "none");
    $(".menuAulas h2").css("display", "none");
    $(".fecharMenu").css("display", "none");
    $(".videoAula").removeClass("col-9");
    $(".videoAula").addClass("col-11");

};

function PlayVideo(url) {
    var src;
    var content;

    if (url.indexOf("www.youtube") >= 0) {
        url = url.split('v=')[1];
        src = "//www.youtube.com/embed/" + url;
        //$("#iFrame")[0].src = "//www.youtube.com/embed/" + url;
        content = $('<iframe id="iFrame" style="position:absolute;top:0;left:0;width:100%;height:100%;" src="' + src + '" frameborder="0" allowfullscreen> #document <html> <head> <meta name="viewport" content="width=device-width"> </head> <body> <video controls autoplay name="media" controlslist="nodownload"> <source src="' + src + '" type="video/mp4"> </video> </body> </html> </iframe>')
    } else {
        //$("#iFrame")[0].src = url;
        src = url;
        content = $('<video style="position:absolute;top:0;left:0;width:100%;height:100%;" frameborder="0" controls autoplay name="media" controlslist="nodownload"> <source src="' + src + '" type="video/mp4"> </video>');
    }

    $('#videoFrame').html(content);

};
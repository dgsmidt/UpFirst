var i = 0;

function DeleteArquivoEntry(deleteButton) {
    $(deleteButton).parent().closest('.input-group').remove();
    RenomearInputs(); 
}
function RenomearInputs() {
    var inputs = $("#arquivosApoio :input");
    i = 0;
    inputs.each(function (index) {
        if (inputs[index].name != '') {
            //alert(inputs[index].name);
            $(inputs[index]).attr('name', 'ArquivosApoio[' + i + '].FileName');
            i++;
            //inputs[index].attr('name','teste');
        }
    });
}
function ModalArquivoVideoOkClicked() {
    $('#Aula_Video').val("/uploads/" + $('#SelectVideo').val());
    $('#exampleModal').modal('toggle');
}

function ModalArquivoApoioOkClicked() {
    var content = $('<div id="' + $('#SelectArquivoApoio').val().replace(".", "_") + '" class="input-group mb-3"><input name="ArquivosApoio[' + i + '].FileName" class="form-control" value="/uploads/' + $('#SelectArquivoApoio').val() + '"/><div class="input-group-append"><button class="btn btn-outline-secondary" type="button" onclick="DeleteArquivoEntry(this)">Excluir</button></div></div>');
    $('#arquivosApoio').append(content);
    $('#arquivoApoioModal').modal('toggle');
    RenomearInputs();
}
function ModalOkClicked() {
    $('#Video').val("/uploads/" + $('#SelectVideo').val());
    $('#exampleModal').modal('toggle');
}
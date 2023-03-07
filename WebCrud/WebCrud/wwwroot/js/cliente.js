var msgErroPadrao = "Não foi possível fazer a solicitação, favor entrar em contato com suporte.";
var arrClientes = [];
$(function () {  
    RetornaClientes();
});

//#region Inicia tela Cliente

async function RetornaClientes() {
    NProgressStart();
    var options = {
        type: "POST",
        url: "/Client/ObterClientes",
        contentType: "application/json;charset=utf-8",
        cache: false,
        dataType: "json"
    }
    ajaxPromisse(options).then(function fulfillHandler(result) {
        if (result.success) {
            MontarGridCliente(result.clients.clients);
        } else {            
            ToastMensagem(result.message);
        }
        NProgressDone();
    }, function rejectHandler(jqXHR, textStatus, errorThrown) {
        ToastMensagem(msgErroPadrao);
        NProgressDone();
    }).catch(function errorHandler(error) {        
        ToastMensagem(msgErroPadrao);
        NProgressDone();
    });
}

async function MontarGridCliente(items) {
    $('#tblClientes').DataTable().destroy();
    $('#tblClientes > tbody').empty();
    var html = "";
    arrClientes = items;
    $(items).each(function (i, v) {
        html += `<tr>
                    <td>${v.codcliente}</td>
                    <td>${v.nome}</td>
                    <td>${v.endereco}</td>
                    <td>${v.cidade}</td>
                    <td>${v.uf}</td>
                    <td>${v.datainsercao}</td>
                    <td>
                        <button class="btn btn-sm" data-bs-toggle="modal" data-bs-target="#modalCliente" onclick='ObterCliente(${v.codcliente})'>
                            Editar
                        </button>
                        <button class="btn btn-sm" id="btnDeletarCliente" onclick='AbrirDeletarCliente(${v.codcliente})'>
                            Deletar
                        </button>
                    </td>
                </tr>`;
    });
    $('#tblClientes > tbody').html(html);
    $('#tblClientes').DataTable({
        language: {
            url: '/lib/datatable/lib/lang/pt-br.json',
        },
    });    
}

//#endregion

//#region Editar Cliente

async function ObterCliente(codcliente) {
    var cliente = _.find(arrClientes, function (o) { return o.codcliente == codcliente; });
    PopulaForm("form-cliente", cliente);
    await AdaptaForm("atualizar", codcliente);
}

async function AtualizaCliente() {
    NProgressStart();
    var cliente = $('#form-cliente').serialize();
    var options = {
        type: "POST",
        url: "/Client/AtualizarClientes",
        cache: false,
        data: cliente
    }
    ajaxPromisse(options).then(function fulfillHandler(result) {
        if (result.success) {
            RetornaClientes();
            ToastMensagem(result.message);
        } else {            
            ToastMensagem(result.message);
        }
        NProgressDone();
    }, function rejectHandler(jqXHR, textStatus, errorThrown) {
        ToastMensagem(msgErroPadrao);
        NProgressDone();
    }).catch(function errorHandler(error) {
        ToastMensagem(msgErroPadrao);
        NProgressDone();
    });
}

//#endregion

//#region Salvar Cliente

async function AbrirSalvarCliente() {
    $('.hidesave').hide();
    $('#form-cliente').trigger("reset");
    var dataDia = await RetornaDataDia();
    $('#datainsercao').val(dataDia);   
    $('[name=datainsercao]').val(dataDia);
    await AdaptaForm("salvar");
}

async function SalvarCliente() {
    NProgressStart();
    var cliente = $('#form-cliente').serialize();
    var options = {
        type: "POST",
        url: "/Client/SalvarCliente",
        cache: false,
        data: cliente
    }
    ajaxPromisse(options).then(function fulfillHandler(result) {
        if (result.success) {
            RetornaClientes();
            $('#form-cliente').trigger("reset");
            $('#modalCliente').modal('hide');
            ToastMensagem(result.message);
        } else {
            ToastMensagem(result.message);
        }
        NProgressDone();
    }, function rejectHandler(jqXHR, textStatus, errorThrown) {
        ToastMensagem(msgErroPadrao);
        NProgressDone();
    }).catch(function errorHandler(error) {
        ToastMensagem(msgErroPadrao);
        NProgressDone();
    });
}

//#endregion

//#region Deletar Cliente

async function AbrirDeletarCliente(codcliente) {
    let text = "Deseja realmente deletar esse cliente? Essa operação não pode ser desfeita.";
    if (confirm(text) == true) {
        await DeletarCliente(codcliente);
    } 
}

async function DeletarCliente(codcliente) {
    NProgressStart();
    var options = {
        type: "POST",
        url: "/Client/DeletarCliente",
        cache: false,
        data: { codcliente: codcliente }
    }
    ajaxPromisse(options).then(function fulfillHandler(result) {
        if (result.success) {
            RetornaClientes();
            
            ToastMensagem(result.message);
        } else {
            ToastMensagem(result.message);
        }
        NProgressDone();
    }, function rejectHandler(jqXHR, textStatus, errorThrown) {
        ToastMensagem(msgErroPadrao);
        NProgressDone();
    }).catch(function errorHandler(error) {
        ToastMensagem(msgErroPadrao);
        NProgressDone();
    });
}

//#endregion

//#region utils

async function PopulaForm(currentForm, data) {
    $.each(data, function (key, value) {
        var input = $('[name=' + key + ']');
        if (input[0].type == 'hidden') {
            input.val(value);
            $('#' + key).val(value);
        } else {
            input.val(value);
        }
        
        //$('[name=' + key + ']').val(value);
    });
}

async function RetornaDataDia() {
    const today = new Date();
    const yyyy = today.getFullYear();
    let mm = today.getMonth() + 1; // Months start at 0!
    let dd = today.getDate();

    if (dd < 10) dd = '0' + dd;
    if (mm < 10) mm = '0' + mm;

    const formattedToday = dd + '/' + mm + '/' + yyyy;
    return formattedToday;
}

async function AdaptaForm(type, codcliente) {
    if (type == "atualizar") {
        $('#btnClienteMods').html("Atualizar");
        $('#btnClienteMods').removeAttr('onclick');
        $('#btnClienteMods').attr('onClick', `AtualizaCliente(${codcliente});`);
    } else {
        $('#btnClienteMods').html("Salvar");
        $('#btnClienteMods').removeAttr('onclick');
        $('#btnClienteMods').attr('onClick', `SalvarCliente();`);
    }
}

async function ToastMensagem(mensagem) {
    $('.toast-body').html(mensagem);
    $('.toast').toast('show');
}

//#endregion
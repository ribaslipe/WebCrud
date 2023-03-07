var msgErroPadrao = "Não foi possível fazer a solicitação, favor entrar em contato com suporte.";

async function Logar() {
    NProgressStart();

    var mod = {
        usuario: $("#txtUsuario").val(),
        senha: $("#txtSenha").val()
    };

    var options = {
        type: "POST",
        url: "/Home/Logar",        
        cache: false,        
        data: mod
    }
    ajaxPromisse(options).then(function fulfillHandler(result) {
        if (result.success) {
            window.location.href = "/Client";
        } else {
            alert(result.message);
        }        
        NProgressDone();
    }, function rejectHandler(jqXHR, textStatus, errorThrown) {
        alert(msgErroPadrao);        
        NProgressDone();
    }).catch(function errorHandler(error) {
        alert(msgErroPadrao);   
        NProgressDone();
    });
}

async function Sair() {
    var options = {
        type: "POST",
        url: "/Home/Sair",
        cache: false
    }
    ajaxPromisse(options).then(function fulfillHandler(result) {
        if (result.success) {
            window.location.href = "/Home";
        } else {
            alert(result.message);
        }
        NProgressDone();
    }, function rejectHandler(jqXHR, textStatus, errorThrown) {
        alert(msgErroPadrao);
        NProgressDone();
    }).catch(function errorHandler(error) {
        alert(msgErroPadrao);
        NProgressDone();
    });

}
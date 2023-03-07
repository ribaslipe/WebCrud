$(document).ready(function () {
    NProgress.settings.template = '<div class="bar" role="bar"><div class="peg"></div></div>';
    NProgress.settings.template += '<div class="modal-backdrop bg-brand-80" id="modalLoading" role="spinner"><div class="modal-dialog-centered text-center"><div class="modal-content bg-transparent border-0">';
    NProgress.settings.template += '<div class="fs-1"><span class="spinner-border md text-brand" role="status" aria-hidden="true"></span><p class="text-brand fa-2x">Carregando...</p>';
    NProgress.settings.template += '</div></div></div></div>';

    NProgress.start();

    setTimeout(function () {
        NProgress.done();
    }, 1000);
});

function NProgressStart() {
    NProgress.start();
}

function NProgressDone() {
    NProgress.done();
}

function ajaxPromisse(options) {

    if (isInjection(options.data)) {
        alert("Requisição não permitida encontrada, o sistema bloqueou.\ Security issues were found on the request. The system blocked the process.");        
        return false;
    }

    return new Promise(function (resolve, reject) {
        $.ajax(options).done(resolve).fail(reject);
    });
}

function isInjection(text) {

    var isInj = false;

    if ((/\w*((\%27)|(\'))((\%6F)|o|(\%4F))((\%72)|r|(\%52))/).test(text))
        return true;

    if ((/((\%27)|(\'))union/).test(text))
        return true;

    if ((/((\%27)|(\'))select/).test(text))
        return true;

    if ((/((\%27)|(\'))insert/).test(text))
        return true;

    if ((/((\%27)|(\'))update/).test(text))
        return true;

    if ((/((\%27)|(\'))delete/).test(text))
        return true;

    if ((/((\%27)|(\'))drop/).test(text))
        return true;

    if ((/((\%27)|(\'))alter/).test(text))
        return true;

    if ((/((\%27)|(\'))create/).test(text))
        return true;

    if ((/((\%27)|(\'))inner/).test(text))
        return true;

    if ((/((\%27)|(\'))join/).test(text))
        return true;

    if ((/((\%27)|(\'))where/).test(text))
        return true;

    if ((/((\%27)|(\'))execute/).test(text))
        return true;

    if ((/((\%27)|(\'))exec/).test(text))
        return true;

    return isInj;
}
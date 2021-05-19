$(document).ready(function () {
    
    function pers() {
        //$("#PeopleList").find(".list-group-item").find("input[id$='__IdPerson']").each(function (idx, elem) {
        var list = "";
        $("#PeopleList").find(".list-group-item").find("input[id$='__IdPerson']").each(function (idx, elem) {
            var peopleId = $(elem).val();
            list += peopleId + ";";
        });
        return list;
    };

    //Соглашение об изъятии
    $("body").on('click', ".rr-report-takeover-agreement", function (e) {
        var idContract = $(this).data("id-contract");
        var idsPers = $(this).data("ids-persons");
        $("#takeoverAgreementModal").find("[name='TakeoverAgreement.idContract']").val(idContract);
        $("#takeoverAgreementModal").find("[name='TakeoverAgreement.idsPersons']").val(idsPers);
        $("#takeoverAgreementModal").modal("show");
        e.preventDefault();
    });

    $("#takeoverAgreementModal .rr-report-submit").on("click", function (e) {
        e.preventDefault();
        var isValid = $(this).closest("#takeoverAgreementForm").valid();
        fixBootstrapSelectHighlight($(this).closest("#takeoverAgreementForm"));
        if (!isValid) {
            return false;
        }
        var idsPersons = pers();
        if (idsPersons == "") {
            idsPersons = $("#takeoverAgreementModal").find("[name='TakeoverAgreement.idsPersons']").val();
        }
        var idContract = $("#takeoverAgreementModal").find("[name='TakeoverAgreement.idContract']").val();
        var idSigner = $("#takeoverAgreementModal").find("[name='TakeoverAgreement.idSigner']").val();
        var date = $("#takeoverAgreementModal").find("[name='TakeoverAgreement.Date']").val();
        if ($("#takeoverAgreementModal").find(".input-validation-error").length > 0) {
            return false;
        }

        var url = "/ContractReport/GetTakeoverAgreement?idContract=" + idContract + "&idsPersons=" + idsPersons + "&idSigner=" + idSigner + "&date=" + date;

        if (url !== undefined) {
            downloadFile(url);
        }

        $("#takeoverAgreementModal").modal("hide");
    });

    //дог с датами
    $("body").on('click', ".rr-report-contract-date", function (e) {
        var idContract = $(this).data("id-contract");
        url = "/ContractReport/GetContractWithDate?idContract=" + idContract;
        downloadFile(url);
        e.preventDefault();
    });
    //Соглашение о передаче в мун. собственность
    $("body").on('click', ".rr-report-agreement-transfer", function (e) {
        var idContract = $(this).data("id-contract");
        url = "/ContractReport/GetAgreementTransfer?idContract=" + idContract;
        downloadFile(url);
        e.preventDefault();
    });
    //дог без дат
    $("body").on('click', ".rr-report-contract-without-date", function (e) {
        var idContract = $(this).data("id-contract");
        url = "/ContractReport/GetContractWithoutDate?idContract=" + idContract;
        downloadFile(url);
        e.preventDefault();
    });
    //дог дкср
    $("body").on('click', ".rr-report-dksr-contract", function (e) {
        var idContract = $(this).data("id-contract");
        url = "/ContractReport/GetContractDksr?idContract=" + idContract;
        downloadFile(url);
        e.preventDefault();
    });

    //предварительный договор
    $("body").on('click', ".rr-report-contract-pre", function (e) {
        var idContract = $(this).data("id-contract");
        $("#preContractModal").find("[name='PreContract.idContract']").val(idContract);
        $("#preContractModal").find("input, textarea, select").prop("disabled", false);
        $("#preContractModal").modal("show");
        e.preventDefault();
    });

    $("#preContractModal .rr-report-submit").on("click", function (e) {
        e.preventDefault();
        var isValid = $(this).closest("#preContractForm").valid();
        fixBootstrapSelectHighlight($(this).closest("#preContractForm"));
        if (!isValid) {
            return false;
        }

        var idContract = $("#preContractModal").find("[name='PreContract.idContract']").val();
        var idPreamble = $("#preContractModal").find("[name='PreContract.idPreamble']").val();
        var idDep = $("#preContractModal").find("[name='PreContract.idDep']").val();
        if ($("#preContractModal").find(".input-validation-error").length > 0) {
            return false;
        }

        var url = "/ContractReport/GetPreContract?idContract=" + idContract + "&idPreamble=" + idPreamble
            + "&idDep=" + idDep;

        if (url !== undefined) {
            downloadFile(url);
        }

        $("#preContractModal").modal("hide");
    });


    //уведомления
    $("body").on('click', ".rr-report-notification", function (e) {
        var idContract = $(this).data("id-contract");
        var notifyType = $(this).data("notify-type");
        $("#notifyMenaModal").find("[name='NotifyMena.idContract']").val(idContract);
        $("#notifyMenaModal").find("[name='NotifyMena.notifyType']").val(notifyType);
        $("#notifyMenaModal").find("input, textarea, select").prop("disabled", false);
        $("#notifyMenaModal").modal("show");
        e.preventDefault();
    });

    $("#notifyMenaModal .rr-report-submit").on("click", function (e) {
        e.preventDefault();
        var isValid = $(this).closest("#notifyMenaForm").valid();
        fixBootstrapSelectHighlight($(this).closest("#notifyMenaForm"));
        if (!isValid) {
            return false;
        }
        var notifyType = $("#notifyMenaModal").find("[name='NotifyMena.notifyType']").val();
        var idContract = $("#notifyMenaModal").find("[name='NotifyMena.idContract']").val();
        var idSigner = $("#notifyMenaModal").find("[name='NotifyMena.idSigner']").val();
        var idPrepered = $("#notifyMenaModal").find("[name='NotifyMena.idPrepared']").val();
        if ($("#notifyMenaModal").find(".input-validation-error").length > 0) {
            return false;
        }

        var url = "/ContractReport/GetNotifyMena?idContract=" + idContract + "&notifyType=" + notifyType + "&idPrepared=" + idPrepered
            + "&idSigner=" + idSigner;

        if (url !== undefined) {
            downloadFile(url);
        }

        $("#notifyMenaModal").modal("hide");
    });

    //запрос в мвд
    $("body").on('click', ".rr-report-request-mvd", function (e) {
        var idContract = $(this).data("id-contract");
        var requestType = $(this).data("request-type");
        $("#requestMvdModal").find("[name='RequestMvd.idContract']").val(idContract);
        $("#requestMvdModal").find("[name='RequestMvd.requestType']").val(requestType);
        $("#requestMvdModal").find("input, textarea, select").prop("disabled", false);
        $("#requestMvdModal").modal("show");
        e.preventDefault();
    });

    $("#requestMvdModal .rr-report-submit").on("click", function (e) {
        e.preventDefault();
        var isValid = $(this).closest("#requestMvdForm").valid();
        fixBootstrapSelectHighlight($(this).closest("#requestMvdForm"));
        if (!isValid) {
            return false;
        }
        var requestType = $("#requestMvdModal").find("[name='RequestMvd.requestType']").val();
        var idContract = $("#requestMvdModal").find("[name='RequestMvd.idContract']").val();
        var idSigner = $("#requestMvdModal").find("[name='RequestMvd.idSigner']").val();
        var idPrepered = $("#requestMvdModal").find("[name='RequestMvd.idPrepared']").val();
        if ($("#requestMvdModal").find(".input-validation-error").length > 0) {
            return false;
        }

        var url = "/ContractReport/GetRequestToMvd?idContract=" + idContract + "&requestType=" + requestType + "&idPrepared=" + idPrepered
            + "&idSigner=" + idSigner;

        if (url !== undefined) {
            downloadFile(url);
        }

        $("#requestMvdModal").modal("hide");
    });
    
    //постановление
    $("body").on('click', ".rr-report-resolution", function (e) {
        var idContract = $(this).data("id-contract");
        $("#resolutionModal").find("[name='Resolution.idContract']").val(idContract);
        $("#resolutionModal").modal("show");
        e.preventDefault();
    });

    $("#resolutionModal .rr-report-submit").on("click", function (e) {
        e.preventDefault();
        var isValid = $(this).closest("#resolutionForm").valid();
        fixBootstrapSelectHighlight($(this).closest("#resolutionForm"));
        if (!isValid) {
            return false;
        }
        var idContract = $("#resolutionModal").find("[name='Resolution.idContract']").val();
        var idPrepared = $("#resolutionModal").find("[name='Resolution.idPrepared']").val();
        var idSigner = $("#resolutionModal").find("[name='Resolution.idSigner']").val();
        var idLawyer = $("#resolutionModal").find("[name='Resolution.idLawyer']").val();
        var idExecutor = $("#resolutionModal").find("[name='Resolution.idExecutor']").val();
        var idWarrantTemplate = $("#resolutionModal").find("[name='Resolution.idWarrantTemplate']").val();
        var idAgree = $("#resolutionModal").find("[name='Resolution.idAgree']").val();
        if ($("#resolutionModal").find(".input-validation-error").length > 0) {
            return false;
        }

        var url = "/ContractReport/GetResolution?idContract=" + idContract + "&idWarrantTemplate=" + idWarrantTemplate + "&idAgree=" + idAgree
            + "&idSigner=" + idSigner + "&idPrepared=" + idPrepared + "&idLawyer=" + idLawyer + "&idExecutor=" + idExecutor;

        if (url !== undefined) {
            downloadFile(url);
        }

        $("#resolutionModal").modal("hide");
    });

    //Соглашение
    $("body").on('click', ".rr-report-agreement", function (e) {
        var idContract = $(this).data("id-contract");
        var agreementType = $(this).data("agreement-type");
        $("#agreementModal").find("[name='Agreement.idContract']").val(idContract);
        $("#agreementModal").find("[name='Agreement.agreementType']").val(agreementType);
        $("#agreementModal").modal("show");
        e.preventDefault();
    });

    $("#agreementModal .rr-report-submit").on("click", function (e) {
        e.preventDefault();
        var isValid = $(this).closest("#agreementForm").valid();
        fixBootstrapSelectHighlight($(this).closest("#agreementForm"));
        if (!isValid) {
            return false;
        }
        var agreementType = $("#agreementModal").find("[name='Agreement.agreementType']").val();
        var idContract = $("#agreementModal").find("[name='Agreement.idContract']").val();
        var idSigner = $("#agreementModal").find("[name='Agreement.idSigner']").val();
        var date = $("#agreementModal").find("[name='Agreement.Date']").val();
        if ($("#agreementModal").find(".input-validation-error").length > 0) {
            return false;
        }

        var url = "/ContractReport/GetAgreement?idContract=" + idContract + "&idSigner=" + idSigner + "&agreementType=" + agreementType + "&date=" + date;

        if (url !== undefined) {
            downloadFile(url);
        }

        $("#agreementModal").modal("hide");
    });

    //Распоряжение
    $("body").on('click', ".rr-report-rasp", function (e) {
        var idContract = $(this).data("id-contract");
        $("#raspModal").find("[name='Rasp.idContract']").val(idContract);
        $("#raspModal").modal("show");
        e.preventDefault();
    });
    $("#raspModal .rr-report-submit").on("click", function (e) {
        e.preventDefault();
        var isValid = $(this).closest("#raspForm").valid();
        fixBootstrapSelectHighlight($(this).closest("#raspForm"));
        if (!isValid) {
            return false;
        }
        var idContract = $("#raspModal").find("[name='Rasp.idContract']").val();
        var idPrepared = $("#raspModal").find("[name='Rasp.idPrepared']").val();
        var idSigner = $("#raspModal").find("[name='Rasp.idSigner']").val();
        var idLawyer = $("#raspModal").find("[name='Rasp.idLawyer']").val();
        var idExecutor = $("#raspModal").find("[name='Rasp.idExecutor']").val();
        var dateNote = $("#raspModal").find("[name='RaspNote.Date']").val();
        var numberNote = $("#raspModal").find("[name='RaspNote.Number']").val();
        var dateRasp = $("#raspModal").find("[name='Rasp.Date']").val();
        var numberRasp = $("#raspModal").find("[name='Rasp.Number']").val();
        if ($("#raspModal").find(".input-validation-error").length > 0) {
            return false;
        }

        var url = "/ContractReport/GetRasp?idContract=" + idContract + "&dateNote=" + dateNote + "&numberNote=" + numberNote + "&dateRasp=" + dateRasp
            + "&numberRasp=" + numberRasp
            + "&idSigner=" + idSigner + "&idPrepared=" + idPrepared + "&idLawyer=" + idLawyer + "&idExecutor=" + idExecutor;

        if (url !== undefined) {
            downloadFile(url);
        }

        $("#raspModal").modal("hide");
    });

    $("#preContractModal select, #notifyMenaModal select, #requestMvdModal select, #resolutionModal select, #agreementModal select, #takeoverAgreementModal select, #raspModal select").on("change", function () {
        $(this).valid();
        fixBootstrapSelectHighlightOnChange($(this));
    });
});



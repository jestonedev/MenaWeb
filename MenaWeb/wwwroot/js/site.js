if ($.validator !== undefined) {
    $.extend($.validator.methods, {
        number: function (value, element) {
            return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
        },
        range: function (value, element, param) {
            return this.optional(element) || (Number(value.replace(",", ".")) >= param[0] && Number(value.replace(",", ".")) <= param[1]);
        },
        required: function (b, c, d) {
            return $.trim(b).length > 0;
        }
    });
    $("[data-val-number]").attr("data-val-number", "Введите числовое значение");
    refreshValidationForm($("form"));
}

$.fn.inputFilter = function (inputFilter) {
    return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
        if (inputFilter(this.value)) {
            this.oldValue = this.value;
            this.oldSelectionStart = this.selectionStart;
            this.oldSelectionEnd = this.selectionEnd;
        } else if (this.hasOwnProperty("oldValue")) {
            this.value = this.oldValue;
            this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
        } else {
            this.value = "";
        }
    });
};

$('.input-filter-numbers, .input-numbers').inputFilter(function (value) {
    return /^\d*$/.test(value);
});

$('.input-filter-chars, .input-chars').inputFilter(function (value) {
    return /^[а-яА-Я]*$/.test(value);
});

$('.input-filter-snp, .input-snp').inputFilter(function (value) {
    return /^([а-яА-Я]+[ ]?)*$/.test(value);
});

$('.input-filter-cadastral-num, .input-cadastral-num').inputFilter(function (value) {
    return /^(\d+:?)*$/.test(value);
});

$('.input-filter-decimal, .input-decimal').inputFilter(function (value) {
    return /^-?\d*[.,]?\d{0,2}$/.test(value);
});

$('.input-filter-premise-num, .input-premise-num').inputFilter(function (value) {
    return /^[0-9\\/а-яА-Я,-]*$/.test(value);
});

$('.input-filter-house, .input-house').inputFilter(function (value) {
    return /^[0-9\\/а-яА-Я]*$/.test(value);
});

function updateControl(idx, control, namePropRegex, idPropRegex) {
    $(control)
        .find("[name]")
        .filter(function (fieldIdx, field) {
            return $(field).prop("name").match(namePropRegex) != null;
        })
        .each(function (fieldIdx, field) {
            var name = $(field).prop("name").replace(namePropRegex, "$1[" + idx + "]");
            $(field).prop("name", name);
            var id = $(field).prop("id").replace(idPropRegex, "$1_" + idx + "__");
            $(field).prop("id", id);
            $(field).closest(".form-group").find("label").prop("for", id);
            $(field).closest(".form-group").find("span[data-valmsg-for]").attr("data-valmsg-for", name);
        });
}


function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

$('.page-link').off("click");
$('.page-link').click(function (e) {
    $('input[name="PageOptions.CurrentPage"]').val($(this).data("page"));
    $("form.filterForm").submit();
    e.preventDefault();
}); 

function reinitSearchContractFields(text, className) {
    var isFirstElem = true;
    $(".m-contract-filter__input").each(function (idx, elem) {
        if (!$(elem).hasClass("d-none") && !$(elem).hasClass(className)) {
            $(elem).addClass("d-none");
        }
        if ($(elem).hasClass("d-none") && $(elem).hasClass(className)) {
            $(elem).removeClass("d-none");
            if (isFirstElem) {
                isFirstElem = false;
                $(elem).select();
            }
            $(".m-contract-filter-fields").append($(elem));
        }
    });
    $(".m-contract-filter-fields").append($(".input-group-append"));
    $(".m-contract-filter__select-type-btn").text(text).data("class", className);
}

function fixBootstrapSelectHighlight(form) {
    form.find("select").each(function (idx, elem) {
        var id = $(elem).prop("id");
        var name = $(elem).prop("name");
        var errorSpan = form.find("span[data-valmsg-for='" + name + "']");
        if (errorSpan.hasClass("field-validation-error")) {
            form.find("button[data-id='" + id + "']").addClass("input-validation-error");
        } else {
            form.find("button[data-id='" + id + "']").removeClass("input-validation-error");
        }
    });
}

function fixBootstrapSelectHighlightOnChange(select) {
    var isValid = select.valid();
    var id = select.prop("id");
    if (!isValid) {
        select.closest("form").find("button[data-id='" + id + "']").addClass("input-validation-error");
    } else {

        select.closest("form").find("button[data-id='" + id + "']").removeClass("input-validation-error");
    }
}

function refreshValidationForm(form) {
    form
        .removeData("validator")
        .removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    form.validate();
}

function clearValidationError(elem) {
    var spanError = $("span[data-valmsg-for='" + elem.attr("name") + "']");
    spanError.empty().removeClass("field-validation-error").addClass("field-validation-valid");
    elem.removeClass("input-validation-error");
}

function elementToogleHide(event) {
    var elementArrow = $(this);
    var isDisplay = isExpandElemntArrow(elementArrow);
    //свернуть
    if (isDisplay) {
        elementArrow.html('∨');
        event.data.addClass("toggle-hide");
    }
    //развернуть
    else {
        elementArrow.html('∧');
        event.data.removeClass("toggle-hide");
    }
    //event.data.toggle(!isDisplay);
    event.preventDefault();
}

function isExpandElemntArrow(elemntArrow) {
    if (elemntArrow.html() === '∧')
        return true;
    return false;
}

$(".m-contract-filter__type").on("click", function () {
    var text = $(this).text();
    var className = $(this).data("class");

    reinitSearchContractFields(text, className);
});

$(".filterForm").on("submit", function () {
    var className = $(".m-contract-filter__select-type-btn").data("class");
    $(".m-contract-filter__input").each(function (idx, elem) {
        if (!$(elem).hasClass(className)) {
            $(elem).val("");
        }
    });
});

var className = $(".m-contract-filter__select-type-btn").data("class");
var text = $(".m-contract-filter__select-type-btn").text();
reinitSearchContractFields(text, className);

$(".m-contract-filter__cancel").on("click", function () {
    $(".m-contract-filter__input").val("");
    $(this).closest("form").submit();
});

$('.idCheckbox').click(function (e) {
    var id = +$(this).data('id');
    $.ajax({
        type: 'POST',
        url: window.location.origin + '/Contract/CheckIdToSession',
        data: { id, isCheck: $(this).prop('checked') }
    });
});

$('.addselect').click(function (e) {
    var filterOptions = $(".filterForm").find("input, select, textarea").filter(function (idx, elem) {
        return /^FilterOptions\./.test($(elem).attr("name"));
    });
    var data = {};
    filterOptions.each(function (idx, elem) {
        data[$(elem).attr("name")] = $(elem).val();
    });
    
    $.ajax({
        type: 'POST',
        url: window.location.origin + '/Contract/AddSelectedAndFilteredIdsToSession',
        data: data,
        success: function (status) {
            if (status == 0) {
                $(".info").html("Договоры успешно добавлены в мастер массовых отчетов");
                $(".info").addClass("alert alert-success");
            }
            if (status == -1) {
                $(".info").html("Договоры для добавления в мастер массовых отчетов отсутствуют");
                $(".info").addClass("alert alert-danger");
            }
        }
    });
});

$("form#ContractForm").on("submit", function (e) {
    $("button[data-id], .bootstrap-select").removeClass("input-validation-error");
    $(this).find("input[id$='_TotalArea']").each(function (idx, elem) {
        $(elem).val($(elem).val().replace('.', ','));
    });
    var isFormValid = $(this).valid();

    if (!isFormValid) {
        fixBootstrapSelectHighlight($(this));

        $(".toggle-hide").each(function (idx, elem) {
            if ($(elem).find(".field-validation-error").length > 0) {
                var toggler = $(elem).closest(".card").find(".card-header .contract-toggler").first();
                if (!isExpandElemntArrow(toggler)) {
                    toggler.click();
                }
            }
        });
        $([document.documentElement, document.body]).animate({
            scrollTop: $(".input-validation-error").first().offset().top - 35
        }, 1000);

        e.preventDefault();
    }
});

$("form#ContractForm, form#personModalForm").on("change", "select", function () {
    fixBootstrapSelectHighlightOnChange($(this));
});

$('.contract-toggler').each(function (idx, e) {
    $(e).on('click', $('#' + $(e).data("for")), elementToogleHide);
});

// Side 12
$(".m-contract__add-side-12-btn").on("click", function (e) {
    $(this).addClass("d-none");
    $(".side-12-container").removeClass("d-none").find("input, select, textarea")
        .valid();   
    e.preventDefault();
});

$(".m-contract__remove-side-12-btn").on("click", function (e) {
    $(".m-contract__add-side-12-btn").removeClass("d-none");
    $(".side-12-container").find("input, select, textarea").val("");
    $("#Contract_ApartmentSide12_Part").val("1");
    $("#Contract_ApartmentSide12_IdApartmentType").val("1");
    $(".side-12-container .m-evaluator-hidden-wrapper").empty();
    $(".side-12-container input[name$='.EvaluatorTitle']").val("");
    $(".side-12-container").find("select").selectpicker("refresh");
    $(".side-12-container").addClass("d-none");
    e.preventDefault();
});


// Elevator
$(".m-evaluator-clear").on("click", function (e) {
    var modal = $("#evaluatorModal");
    modal.find("select, input, textarea").val("").selectpicker("refresh");
});

$(".m-evaluator-btn").on("click", function (e) {
    var modal = $("#evaluatorModal");
    modal.find("select, input, textarea").val("");
    var inputsWrapper = $(this).closest(".m-evaluator").find(".m-evaluator-hidden-wrapper");
    inputsWrapper.find("input[type='hidden']").each(function(idx, elem) {
        var value = $(elem).val();
        var name = $(elem).attr("name");
        var nameParts = name.split('.');
        var fieldName = nameParts[nameParts.length - 1];
        modal.find("[name='Evaluator." + fieldName + "']").val(value);
    });
    modal.find(".m-evaluator-submit").off("click").on("click", function (ev) {
        var apartmentSide = inputsWrapper.closest(".m-evaluator").data("side");
        var title = "";
        modal.find("select, input, textarea").each(function (idx, elem) {
            var value = $(elem).val();
            var name = $(elem).attr("name");
            var nameParts = name.split('.');
            var fieldName = nameParts[nameParts.length - 1];
            var inputFieldName = "Contract." + apartmentSide + ".ApartmentEvaluations[0]." + fieldName;
            var inputFieldId = inputFieldName.replace(/[.]/g, "_");
            var input = inputsWrapper.find("[name='"+inputFieldName+"']");
            if (input.length === 0) {
                inputsWrapper.append("<input id='" + inputFieldId + "' name='" + inputFieldName + "' type='hidden' value='" + value + "'>");
            } else {
                input.val(value);
            }
            if (fieldName === "IdEvaluator" && value !== "") {
                title += $(elem).find("option[value='" + value + "']").text();
            }
            if (fieldName === "EvaluationPrice" && value !== "") {
                if (title !== "") {
                    title += ": ";
                }
                title += value + " руб.";
            }
        });
        var idAparmtnetEvaluatorName = "Contract." + apartmentSide + ".ApartmentEvaluations[0].IdApartmentEvaluation";
        var idAparmtnetEvaluatorId = idAparmtnetEvaluatorName.replace(/[.]/g, "_");
        if (inputsWrapper.find("[name='" + idAparmtnetEvaluatorName + "']").length === 0) {
            inputsWrapper.append("<input id='" + idAparmtnetEvaluatorId + "' name='" + idAparmtnetEvaluatorName + "' type='hidden' value='0'>");
        }
        var idAparmtnetName = "Contract." + apartmentSide + ".ApartmentEvaluations[0].IdApartment";
        var idAparmtnetId = idAparmtnetName.replace(/[.]/g, "_");
        if (inputsWrapper.find("[name='" + idAparmtnetName + "']").length === 0) {
            inputsWrapper.append("<input id='" + idAparmtnetId + "' name='" + idAparmtnetName + "' type='hidden' value='0'>");
        }
        inputsWrapper.closest(".m-evaluator").find("input[name$='.EvaluatorTitle']").val(title);
        modal.modal('hide');
    });
    modal.modal('show');
});

$("#evaluatorModal").on("show.bs.modal", function () {
    $(this).find("select").selectpicker("refresh");
});

// Land
$(".m-land-clear").on("click", function (e) {
    var modal = $("#landModal");
    modal.find("select, input, textarea").val("").selectpicker("refresh");
});

$(".m-land-btn").on("click", function (e) {
    var modal = $("#landModal");
    modal.find("select, input, textarea").val("");
    var inputsWrapper = $(this).closest(".m-land").find(".m-land-hidden-wrapper");
    inputsWrapper.find("input[type='hidden']").each(function (idx, elem) {
        var value = $(elem).val();
        var name = $(elem).attr("name");
        var nameParts = name.split('.');
        var fieldName = nameParts[nameParts.length - 1];
        modal.find("[name='Land." + fieldName + "']").val(value);
    });
    modal.find(".m-land-submit").off("click").on("click", function (ev) {
        var apartmentSide = inputsWrapper.closest(".m-land").data("side");
        var title = "";

        modal.find("select, input, textarea").each(function (idx, elem) {
            var value = $(elem).val();
            var name = $(elem).attr("name");
            var nameParts = name.split('.');
            var fieldName = nameParts[nameParts.length - 1];
            var inputFieldName = "Contract." + apartmentSide + ".Land[0]." + fieldName;
            var inputFieldId = inputFieldName.replace(/[.]/g, "_");
            var input = inputsWrapper.find("[name='" + inputFieldName + "']");
            if (fieldName === "TotalArea" && value !== "") {
                value = value.replace('.', ',');
            }
            if (input.length === 0) {
                inputsWrapper.append("<input id='" + inputFieldId + "' name='" + inputFieldName + "' type='hidden' value='" + value + "'>");
            } else {
                input.val(value);
            }
            if (fieldName === "IdStreet" && value !== "") {
                title += $(elem).find("option[value='" + value + "']").text();
            }
            if (fieldName === "House" && value !== "") {
                if (title !== "") {
                    title += ", ";
                }
                title += value;
            }
        });
        var idAparmtnetLandName = "Contract." + apartmentSide + ".Land[0].IdLand";
        var idAparmtnetLandId = idAparmtnetLandName.replace(/[.]/g, "_");
        if (inputsWrapper.find("[name='" + idAparmtnetLandName + "']").length === 0) {
            inputsWrapper.append("<input id='" + idAparmtnetLandId + "' name='" + idAparmtnetLandName + "' type='hidden' value='0'>");
        }
        var idAparmtnetName = "Contract." + apartmentSide + ".Land[0].IdApartment";
        var idAparmtnetId = idAparmtnetName.replace(/[.]/g, "_");
        if (inputsWrapper.find("[name='" + idAparmtnetName + "']").length === 0) {
            inputsWrapper.append("<input id='" + idAparmtnetId + "' name='" + idAparmtnetName + "' type='hidden' value='0'>");
        }
        inputsWrapper.closest(".m-land").find("input[name$='.LandAddress']").val(title);
        modal.modal('hide');
    });
    modal.modal('show');
});

$("#landModal").on("show.bs.modal", function () {
    $(this).find("select").selectpicker("refresh");
});

//Additional
$(".m-additional__osnovanie select").on("change", function () {
    var id = $(this).val();
    var text = $(this).find("option[value='" + id + "']").text();
    $(".m-additional__osnovanie input[type='hidden']").val(text);
});

//Statuses
function addEmtpyStatusRow() {
    var modalBody = $("#statusesModal .modal-body");
    if (modalBody.find(".m-status-row").length > 0) {
        modalBody.prepend("<hr/>");
    }
    modalBody.prepend(statusRowTemplate.clone(true));
    var lastInsertedItem = $(modalBody.find(".m-status-row")[0]);
    lastInsertedItem.find("input, select, textarea").each(function (idx, elem) {
        var elemName = $(elem).attr("name");
        var spanError = $(elem).closest(".form-group").find("span[data-valmsg-for='" + elemName + "']");
        var newId = $(elem).attr("id") + "_" + uuidv4();
        var newName = newId.replace(/_/g, ".");
        $(elem).attr("id", newId);
        $(elem).attr("name", newName);
        spanError.attr("data-valmsg-for", newName);
    });

    return lastInsertedItem;
}

var statusRowTemplate = undefined;

$(".m-statuses-change-btn").on("click", function () {
    if (statusRowTemplate === undefined) {
        statusRowTemplate = $("#statusesModal .m-status-row").clone(true);
    }
    $("#statusesModal .m-status-row, #statusesModal .modal-body hr").remove();
    
    $(".m-statuses-hidden-wrapper .m-status-item").each(function (idx, elem) {
        var item = addEmtpyStatusRow();
        $(elem).find("input[type='hidden']").each(function (idxInput, elemInput) {
            var value = $(elemInput).val();
            var name = $(elemInput).prop("name");
            var nameParts = name.split(".");
            var modalElemName = "Status." + nameParts[nameParts.length - 1];
            var elem = item.find("[name^='" + modalElemName + "']");
            elem.val(value);
        });
        refreshValidationForm($("#statusesForm"));
    });

    var modal = $("#statusesModal");
    modal.modal('show');
});

$(".m-status-add-btn").on("click", function (e) {
    var lastInsertedItem = addEmtpyStatusRow();
    lastInsertedItem.find("select").selectpicker("refresh");
    refreshValidationForm($("#statusesForm"));
    e.preventDefault();
});

$(".m-status-delete-btn").on("click", function (e) {
    var row = $(this).closest(".m-status-row");
    if ($("#statusesModal .m-status-row").length > 1) {
        if (row.index(".m-status-row") === $("#statusesModal .m-status-row").length - 1) {
            row.prev("hr").remove();
        } else {
            row.next("hr").remove();
        }
    }
    row.remove();
    e.preventDefault();
});

$(".m-status-submit").on("click", function (e) {
    if ($(this).closest("form").valid()) {
        var modal = $("#statusesModal");
        $(".m-statuses-hidden-wrapper .m-status-item").remove();
        $("#statusesModal .m-status-row").each(function (elemIdx, elem) {
            $(".m-statuses-hidden-wrapper").prepend("<div class='m-status-item'></div>");
            var insertedItem = $(".m-statuses-hidden-wrapper").find(".m-status-item").first();
            $(elem).find("input, select, textarea").each(function (modalIdx, modalElem) {
                var val = $(modalElem).val();
                var name = $(modalElem).attr("name");
                var nameParts = name.split(".");
                var insertName = "Contract.ContractStatusHistory[" + elemIdx + "]." + nameParts[nameParts.length - 2];
                var insertId = insertName.replace(/[\.\[\]]/g, "_");
                insertedItem.append("<input type='hidden' name='" + insertName + "' id='" + insertId + "' value='" + val+"'>");
            });
        });
        var processStatus = $("#statusesModal .m-status-row").first().find("select[name^='Status.IdProcessStatus']");
        if (processStatus.length === 0) {
            $("#ProcessStatusTitle").val("");
        } else {
            processStatusId = processStatus.val();
            var processStatusOption = processStatus.find("option[value='" + processStatusId + "']");
            processStatusName = processStatusOption.text();
            processStatusStep = processStatusOption.data("step");
            $("#ProcessStatusTitle").val(processStatusStep + ") " + processStatusName);
        }
        modal.modal('hide');
    }
});

$("#statusesModal").on("show.bs.modal", function () {
    $(this).find("select").selectpicker("refresh");
});

// PersonModal
$('body').on('click', '.person-open-btn, .person-edit-btn', function (e) {
    var personElem = $(this).closest('.list-group-item');
    var modal = $("#personModal");
    modal.data("index", personElem.index());
    personElem.find("input, select")
        .filter(function (idx, elem) { return !$(elem).hasClass("m-input--disable-alwayes") })
        .each(function (idx, elem) {
        var nameParts = $(elem).attr("name").split(".");
        var name = nameParts[nameParts.length - 1];
        modal.find("[name='Person." + name + "']").val($(elem).val());
    });

    modal.modal('show');
    e.preventDefault();
});

$("#personModal").on("click", "#savePersonModalBtn", function (e) {
    var form = $("#personModalForm");
    var isValid = form.valid();

    if (isValid) {
        if (isCustomDocumentIssuedBy) {
            if (!addCustomDocumentIssued())
                return;
        }
        personCorrectSnp(form);
        var index = $(this).closest("#personModal").data("index");
        var personElem = $("#PeopleList").find(".list-group-item")[index];

        var fields = $(personElem).find("input, select, textarea");
        fields.each(function (idx, elem) {
            var nameParts = $(elem).attr("name").split(".");
            var name = nameParts[nameParts.length - 1];
            formElem = form.find("[name='Person." + name + "']");
            if (formElem.length > 0) {
                var value = formElem.val();
                $(elem).val(value);
                if (elem.tagName === "SELECT") {
                    $(elem).selectpicker("refresh");
                }
            }
        });

        $(personElem).removeData("in-process");
        $("#personModal").modal('hide');
    } else {
        fixBootstrapSelectHighlight(form);
        $([document.documentElement, document.body]).animate({
            scrollTop: form.find(".input-validation-error").first().offset().top - 35
        }, 1000);
    }
});

$('body').on("click", ".person-delete-btn", function (e) {
    var container = $("#PeopleList");
    var personElem = $(this).closest('.list-group-item');
    personElem.remove();
    if (container.find(".list-group-item").length === 1) {
        container.find(".list-group-item.rr-list-group-item-empty").show();
    }
    var namePropRegex = /(People)\[\d+\]/;
    var idPropRegex = /(People)_\d+__/;
    var people = $("#PeopleList > .list-group-item").filter(function (idx, elem) { return !$(elem).hasClass("rr-list-group-item-empty"); });
    people.each(function (idx, elem) {
        updateControl(idx, elem, namePropRegex, idPropRegex);
    });

    e.preventDefault();
});

function personCorrectSnp(form) {
    $(form).find("#Person_Family, #Person_Name, #Person_Father").each(function (idx, elem) {
        var value = $(elem).val();
        if (value.length > 0) {
            value = value[0].toUpperCase() + value.substring(1);
            $(elem).val(value);
        }
    });
}

$("#personModal").on("show.bs.modal", function () {
    $(this).find("select").selectpicker("refresh");
});

$("#personModal").on("hide.bs.modal", function () {
    $(this).find(".input-validation-error").removeClass("input-validation-error").addClass("valid");
    $(this).find(".field-validation-error").removeClass("field-validation-error").addClass("field-validation-valid").text("");
    $("#cancelDocumentIssuedBtn").click();

    $("#PeopleList").find(".list-group-item").filter(function (idx, elem) { return $(elem).data("in-process") }).remove();
});

var isCustomDocumentIssuedBy = false;

$("#addDocumentIssuedBtn").on('click', function (e) {
    $("#addDocumentIssuedBtn").hide();
    $("#cancelDocumentIssuedBtn").show();
    $("#CustomDocumentIssued").show();
    $("#Person_IdDocumentIssued").closest(".bootstrap-select").hide();
    isCustomDocumentIssuedBy = true;
    e.preventDefault();
});

$("#cancelDocumentIssuedBtn").on('click', function (e) {
    $("#addDocumentIssuedBtn").show();
    $("#cancelDocumentIssuedBtn").hide();
    $("#CustomDocumentIssued").val("").hide();
    $("#Person_IdDocumentIssued").closest(".bootstrap-select").show();
    isCustomDocumentIssuedBy = false;
    var customDocumentIssued = $("#CustomDocumentIssued").closest(".form-group");
    customDocumentIssued.find(".input-validation-error").removeClass("input-validation-error").addClass("valid");
    customDocumentIssued.find(".field-validation-error").removeClass("field-validation-error").addClass("field-validation-valid").text("");
    e.preventDefault();
});

function addCustomDocumentIssued() {
    let personDocumentIssuedElem = $("#Person_IdDocumentIssued");
    let customDocumentIssued = $("#CustomDocumentIssued").val();
    if (!$("#CustomDocumentIssued").valid()) return false;
    let code = 0;
    $.ajax({
        type: 'POST',
        url: window.location.origin + '/Contract/AddDocumentIssued',
        data: { documentIssuedName: customDocumentIssued },
        async: false,
        success: function (id) {
            code = id;
            if (id > 0) {
                personDocumentIssuedElem.append("<option value='" + id + "'>" + customDocumentIssued + "</option>");
                $("#cancelDocumentIssuedBtn").click();
                personDocumentIssuedElem.val(id).selectpicker("refresh");
            } else
                if (id === -3) {
                    $("#cancelDocumentIssuedBtn").click();
                    var duplicateOption = personDocumentIssuedElem.find("option").filter(function (idx, elem) {
                        return $(elem).text() === customDocumentIssued;
                    });
                    var optionId = 0;
                    if (duplicateOption.length > 0) {
                        optionId = duplicateOption.prop("value");
                    } else {
                        alert('Произошла ошибка при сохранении органа, выдающего документы, удостоверяющие личность');
                    }
                    personDocumentIssuedElem.val(optionId).selectpicker("refresh");
                    code = optionId;
                } else {
                    alert('Произошла ошибка при сохранении органа, выдающего документы, удостоверяющие личность');
                    return false;
                }
        }
    });
    return code > 0;
}

$("#personAdd").on("click", function (e) {
    let action = $('#ContractForm').data('action');
    $.ajax({
        type: 'POST',
        url: window.location.origin + '/Contract/AddPerson',
        data: { action },
        success: function (elem) {
            $("#PeopleList").find(".list-group-item.rr-list-group-item-empty").hide();
            let personToggle = $('.contract-toggler[data-for="PeopleList"]');
            if (!isExpandElemntArrow(personToggle)) // развернуть при добавлении, если было свернуто 
                personToggle.click();
            $("#PeopleList").append(elem).find("select").selectpicker("refresh");
            var namePropRegex = /(People)\[\d+\]/;
            var idPropRegex = /(People)_\d+__/;
            var people = $("#PeopleList > .list-group-item").filter(function (idx, elem) { return !$(elem).hasClass("rr-list-group-item-empty"); });
            people.each(function (idx, elem) {
                updateControl(idx, elem, namePropRegex, idPropRegex);
            });
            $("#PeopleList").find(".list-group-item").last().data("in-process", true).find("input[name$='Sex']").val("");
            $("#PeopleList").find(".list-group-item").last().find(".person-edit-btn").click();
        }
    });
    e.preventDefault();
});

// AccountModal
$('body').on('click', '.account-open-btn, .account-edit-btn', function (e) {
    var accountElem = $(this).closest('.list-group-item');
    var modal = $("#accountModal");
    modal.data("index", accountElem.index());
    modal.find("input, select, textarea").val("");
    accountElem.find("input, select")
        .filter(function (idx, elem) { return !$(elem).hasClass("m-input--disable-alwayes"); })
        .each(function (idx, elem) {
            var nameParts = $(elem).attr("name").split(".");
            var name = nameParts[nameParts.length - 1];
            modal.find("[name='Account." + name + "']").val($(elem).val());

            if (name === "Bank") {
                var bank = $(elem).val();
                var corrAccount = "";
                var bankParts = bank.split(", кор/счет ");
                if (bankParts.length > 1) {
                    corrAccount = bankParts[bankParts.length - 1];
                    bank = "";
                    for (var i = 0; i < bankParts.length - 1; i++) {
                        if (bank !== "") {
                            bank += ", кор/счет";
                        }
                        bank += bankParts[i];
                    }
                    modal.find("[name='Account.BankCorAccount']").val(corrAccount);
                }
                var bik = "";
                bankParts = bank.split(", БИК ");
                if (bankParts.length > 1) {
                    bik = bankParts[bankParts.length - 1];
                    bank = "";
                    for (var i = 0; i < bankParts.length - 1; i++) {
                        if (bank !== "") {
                            bank += ", БИК ";
                        }
                        bank += bankParts[i];
                    }
                    modal.find("[name='Account.BankBik']").val(bik);
                }
                modal.find("[name='Account.BankName']").val(bank);
            }
        });

    modal.modal('show');
    e.preventDefault();
});

$("#accountModal").on("click", "#saveAccountModalBtn", function (e) {
    var form = $("#accountModalForm");
    accountCorrectData(form);
    var index = $(this).closest("#accountModal").data("index");
    var accountElem = $("#AccountList").find(".list-group-item")[index];

    var fields = $(accountElem).find("input, select, textarea");
    fields.each(function (idx, elem) {
        var nameParts = $(elem).attr("name").split(".");
        var name = nameParts[nameParts.length - 1];
        formElem = form.find("[name='Account." + name + "']");
        if (formElem.length > 0) {
            var value = formElem.val();
            $(elem).val(value);
        }
    });

    $(accountElem).removeData("in-process");
    $("#accountModal").modal('hide');
});

$('body').on("click", ".account-delete-btn", function (e) {
    var container = $("#AccountList");
    var accountElem = $(this).closest('.list-group-item');
    accountElem.remove();
    if (container.find(".list-group-item").length === 1) {
        container.find(".list-group-item.rr-list-group-item-empty").show();
    }
    var namePropRegex = /(BankInfos)\[\d+\]/;
    var idPropRegex = /(BankInfos)_\d+__/;
    var accounts = $("#AccountList > .list-group-item").filter(function (idx, elem) { return !$(elem).hasClass("rr-list-group-item-empty"); });
    accounts.each(function (idx, elem) {
        updateControl(idx, elem, namePropRegex, idPropRegex);
    });

    e.preventDefault();
});

function accountCorrectData(form) {
    var holderElem = $(form).find("#Account_AccountHolder");
    var holderValue = $(holderElem).val();
    if (holderValue.length > 0) {
        valueParts = holderValue.split(' ');
        var resultValue = "";
        for (var i = 0; i < valueParts.length; i++) {
            if (valueParts[i].length === 0) continue;
            if (resultValue != "") {
                resultValue += " ";
            }
            resultValue += valueParts[i][0].toUpperCase() + valueParts[i].substring(1);
        }
        $(holderElem).val(resultValue);
    }

    var sumElem = $(form).find("#Account_Sum");
    $(sumElem).val($(sumElem).val().replace('.', ','));

    var bankElem = $(form).find("#Account_Bank");
    var bankName = $(form).find("#Account_BankName").val();
    var bankBik = $(form).find("#Account_BankBik").val();
    var bankCorAccount = $(form).find("#Account_BankCorAccount").val();
    var result = "";
    if (bankName !== "" && bankName !== null) {
        result += bankName;
    }
    if (bankBik !== "" && bankBik !== null) {
        if (result !== "") {
            result += ", ";
        }
        result += "БИК " + bankBik;
    }
    if (bankCorAccount !== "" && bankCorAccount !== null) {
        if (result !== "") {
            result += ", ";
        }
        result += "кор/счет " + bankCorAccount;
    }
    bankElem.val(result);
}

$("#accountModal").on("hide.bs.modal", function () {
    $("#AccountList").find(".list-group-item").filter(function (idx, elem) { return $(elem).data("in-process"); }).remove();
});

$("#accountAdd").on("click", function (e) {
    let action = $('#ContractForm').data('action');
    $.ajax({
        type: 'POST',
        url: window.location.origin + '/Contract/AddAccount',
        data: { action },
        success: function (elem) {
            $("#AccountList").find(".list-group-item.rr-list-group-item-empty").hide();
            $("#AccountList").append(elem);
            var namePropRegex = /(BankInfos)\[\d+\]/;
            var idPropRegex = /(BankInfos)_\d+__/;
            var accounts = $("#AccountList > .list-group-item").filter(function (idx, elem) { return !$(elem).hasClass("rr-list-group-item-empty"); });
            accounts.each(function (idx, elem) {
                updateControl(idx, elem, namePropRegex, idPropRegex);
            });
            $("#AccountList").find(".list-group-item").last().data("in-process", true);
            $("#AccountList").find(".list-group-item").last().find(".account-edit-btn").click();
        }
    });
    e.preventDefault();
});
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

$("form#ContractForm").on("change", "select", function () {
    var isValid = $(this).valid();
    var id = $(this).prop("id");
    if (!isValid) {
        $("button[data-id='" + id + "']").addClass("input-validation-error");
    } else {

        $("button[data-id='" + id + "']").removeClass("input-validation-error");
    }
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
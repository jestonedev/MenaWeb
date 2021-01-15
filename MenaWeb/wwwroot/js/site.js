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
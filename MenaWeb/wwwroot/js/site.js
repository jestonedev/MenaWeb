$('.page-link').off("click");
$('.page-link').click(function (e) {
    $('input[name="PageOptions.CurrentPage"]').val($(this).data("page"));
    $("form.filterForm").submit();
    e.preventDefault();
}); 

$(".m-contract-filter__type").on("click", function () {
    var text = $(this).text();
    var name = $(this).data("name");
    var id = $(this).data("id");
    var placeholder = $(this).data("placeholder");
    $(".m-contract-filter__select-type-btn").text(text);
    $(".m-contract-filter__input").attr("id", id).attr("name", name).attr("placeholder", placeholder).select();
});

$(".m-contract-filter__cancel").on("click", function () {
    $(".m-contract-filter__input").val("");
    $(".m-contract-filter__select-type-btn").text("По адресу");
    $(".m-contract-filter__input").attr("placeholder", "Введите адрес помещения");
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
                $(".info").html("Договоры успешно добавлены в мастер массовых операций");
                $(".info").addClass("alert alert-success");
            }
            if (status == -1) {
                $(".info").html("Договоры для добавления в мастер массовых операций отсутствуют");
                $(".info").addClass("alert alert-danger");
            }
        }
    });
});
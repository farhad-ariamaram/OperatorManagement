//متد مرتب سازی صفحه لیست تراکنش ها
function submitFilterForm(sort) {
    if (sort != null) {
        $("#sortType").val(sort);
    } else {
        $("#search").val($("#searchAlt").val());
    }
    $("#filterFormId").submit();
}

//متد تغییر صفحه لیست تراکنش ها
function submitFilterFormPagination(pageId) {
    $("#pageId").val(pageId);
    $("#filterFormId").submit();
}

//متد تغییر لیبل فعال/غیرفعال در صفحه ویرایش و اضافه کردن سیمکارت
function changeIsActiveLabel() {
    var labelEl = $("#isActiveLabel");
    var checkboxEl = $("#IsActive");
    if (checkboxEl.is(':checked')) {
        labelEl.html("فعال");
    } else {
        labelEl.html("غیرفعال");
    }
}

/*imported (not mine)*/

//گرفتن مقدار یک پرامتر از QueryString
function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
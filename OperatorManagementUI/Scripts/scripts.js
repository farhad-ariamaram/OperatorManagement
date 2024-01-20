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

//پر کردن دراپ دان شماره مقصد در تماس
function fillToId() {
    $("#toSimId").removeAttr("disabled");
    $("#toSimId").empty();
    $.ajax({
        cache: false,
        url: '/Home/GetSimsForDropdown',
        type: 'POST',
        data: {
            "fromSimId": $('#fromSimId').find(":selected").val()
        },
        success: function (data) {
            $("#toSimId").append(
                '<option value="0">انتخاب کنید</option>'
            );
            $.each(data,
                function () {
                    $("#toSimId").append(
                        '<option value="' + this.Id + '">' + this.Number + '</option>'
                    );
                });
        }
    });

}

//پر کردن دراپ دان شماره مقصد در ارسال پیامک
function smsFillToId() {
    $("#sms_toSimId").removeAttr("disabled");
    $("#sms_toSimId").empty();
    $.ajax({
        cache: false,
        url: '/Home/GetSimsForDropdown',
        type: 'POST',
        data: {
            "fromSimId": $('#sms_fromSimId').find(":selected").val()
        },
        success: function (data) {
            $("#sms_toSimId").append(
                '<option value="0">انتخاب کنید</option>'
            );
            $.each(data,
                function () {
                    $("#sms_toSimId").append(
                        '<option value="' + this.Id + '">' + this.Number + '</option>'
                    );
                });
        }
    });

}

//گرفتن مقدار یک پرامتر از QueryString
function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

//توابع نمایش پیغام های خطا برقراری تماس
function showMakeCallSuccessToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#callSuccessToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showMakeCallFailedInssufienceBalanceToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#callFailedInsuffienceBalanceToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showMakeCallFailedInactiveFromSimcardToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#callFailedInactiveFromSimcardToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showMakeCallFailedInactiveToSimcardToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#callFailedInactiveToSimcardToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showMakeCallFailedUnhandledToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#callFailedUnhandledToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showInvalidDurationToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#invalidDurationToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showSendSmsSuccessToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#smsSuccessToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showSendSmsFailedInssufienceBalanceToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#smsFailedInsuffienceBalanceToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showSendSmsFailedInactiveFromSimcardToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#smsFailedInactiveFromSimcardToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showSendSmsFailedInactiveToSimcardToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#smsFailedInactiveToSimcardToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showSendSmsFailedUnhandledToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#smsFailedUnhandledToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showInvalidFromToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#InvalidFromToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}

function showInvalidToToast() {
    var toastElList = [].slice.call(document.querySelectorAll('#InvalidToToast'))
    var toastList = toastElList.map(function (toastEl) {
        return new bootstrap.Toast(toastEl);
    });
    toastList.forEach(toast => toast.show());
}
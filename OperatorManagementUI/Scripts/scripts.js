function submitFilterForm(sort) {
    if (sort != null) {
        $("#sortType").val(sort);
    } else {
        $("#search").val($("#searchAlt").val());
    }
    $("#filterFormId").submit();
}


/*imported (not mine)*/
function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}
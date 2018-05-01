$(document).ready(function () {
    get_warnings();
});

var get_warnings = function () {
    $.get("/api/user/repository/warnings",
        function (data) {
            $.each(data, function (ind, val) {

                //TODO: add class for warnings

                $("").append(val);
                $("").append("<br>");
            })
        })
}
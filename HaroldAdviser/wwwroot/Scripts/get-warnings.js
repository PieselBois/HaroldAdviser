$(document).ready(function () {
    get_warnings();
});

var get_warnings = function() {
    $.get("/api/user/repository/warnings",
        function(data) {
            $.each(data,
                function(ind, val) {

                    //TODO: add class for warnings

                    $(".warnings").append("<span>" + val.kind + val.file + val.lines + val.message + "</span>");
                    $(".warnings").append("<br>");
                });
        });
};
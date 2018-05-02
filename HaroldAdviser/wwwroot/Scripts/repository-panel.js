$(document).ready(function () {
    load_active_repositories();
});

var load_active_repositories = function () {
    $.get("/api/user/repository/",
        function (data) {
            $.each(data,
                function (ind, val) {
                    if (val.active) {
                        $(".repo-container").append("<a href='/User/Repository/" + val.id + "'>" + val.name + "</a>");
                        $(".repo-container").append("<br>");
                    }
                });
        });
};
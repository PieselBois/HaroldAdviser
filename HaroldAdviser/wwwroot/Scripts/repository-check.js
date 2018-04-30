$(document).ready(function () {
    load_repositories();
});

var load_repositories = function () {
    $.get("/api/user/repository/",
        function (data) {
            $.each(data,
                function (ind, val) {
                    $(".all-repos").append("<input class='repo-check' type='checkbox' " + (val.active ? "checked " : "") + "data-check-id='" + val.id + "'>");
                    $(".all-repos").append("<span>" + val.url + "</span>");
                    $(".all-repos").append("<br>");
                });
            //TODO: fobid fast clicking on checkbox (maybe add some blocking animation)
            $(".repo-check").click(function (eventObject) {
                var target = $(eventObject.target);
                $.post("/User/Repository/Check/" + target.data("check-id"));
            });
        });
};

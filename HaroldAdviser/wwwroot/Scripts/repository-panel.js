﻿$(document).ready(function () {
    load_repositories();
});

var load_repositories = function () {
    $.get("/api/user/repository/",
        function (data) {
            $.each(data,
                function (ind, val) {
                    if (val.active) {
                        $(".repo-container").append("<a href='/user/repository/" + val.id + "'>" + val.url + "</a>");
                        $(".repo-container").append("<br>");
                    }
                });
        });
};
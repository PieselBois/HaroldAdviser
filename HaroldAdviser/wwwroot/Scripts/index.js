//$(document).ready(function () {
//    $("#register").click(register_click);
//    $("#sign-in").click(signin_click);
//    $("#login-form-submit-btn").click(go_login);
//    $("#close").click(close_login_form);
//    $(document).mouseup(function (e) {
//        var div = $(".login-box");
//        if (!div.is(e.target)
//            && div.has(e.target).length === 0) {
//            close_login_form();
//        }
//    });
//});

//var register_click = function (eventObject) {
//    //$(".navbar-toggler").click();
//    $("#login-form-submit-btn").val("register");
//    $("#login-form").addClass("open");
//    $("#confirm").addClass("confirm-password-open");
//};

//var signin_click = function (eventObject) {
//    //$(".navbar-toggler").click();
//    $("#login-form-submit-btn").val("login");
//    $("#login-form").addClass("open");
//    $("#confirm").removeClass("confirm-password-open");
//};

//var go_login = function (eventObject) {
//    var target = $(eventObject.target);
//    var url = target.val() == "register"
//        ? "/Account/Register/"
//        : "/Account/Login";

//    $.post(url,
//        {
//            Login: $("#login-field").val(),
//            Password: $("#passwd-field").val()
//        },
//        function (data) {
//            alert(data);
//        });
//};

//var close_login_form = function (eventObject) {
//    $("#login-form").removeClass("open");
//};
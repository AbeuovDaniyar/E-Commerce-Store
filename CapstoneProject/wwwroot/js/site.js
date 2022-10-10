// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    //-- Click on QUANTITY
    $(".btn-minus").on("click", function () {
        var now = $(".section > div > input").val();
        if ($.isNumeric(now)) {
            if (parseInt(now) - 1 > 0) { now--; }
            $(".section > div > input").val(now);
        } else {
            $(".section > div > input").val("1");
        }
    })
    $(".btn-plus").on("click", function () {
        var now = $(".section > div > input").val();
        if ($.isNumeric(now)) {
            $(".section > div > input").val(parseInt(now) + 1);
        } else {
            $(".section > div > input").val("1");
        }
    })
})

$(function () {
    $('#btnStart').unbind('click');
    $('#btnStart').on('click', function () {
        $.ajax({
            url: "/cart/UpdateCart",
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            data: JSON.stringify({
                quantity: $("#quantityVal").val()
            }),
            async: false
        });
    });
});
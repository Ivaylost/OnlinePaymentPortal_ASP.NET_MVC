// Write your JavaScript code.
$(document).ready(function () {

    $('.client-button').click(function () {

        var url = $('#ClientModal').data('url');
        $.get(url, function (data) {
            $("#ClientModal").html(data);
            $("#ClientModal").modal('show');
        });
    });
});

$(document).ready(function () {

    $('.user-button').click(function () {

        var url = $('#UserModal').data('url');
        $.get(url, function (data) {
            $("#UserModal").html(data);
            $("#UserModal").modal('show');
        });
    });
});

$(document).ready(function () {

    $('.product-button').click(function () {

        var url = $('#myModal').data('url') + "/" + this.id;
        console.log(url);
        $.get(url, function (data) {
            $("#myModal").html(data);
            $("#myModal").modal('show');
        });
    });

});


$(document).ready(function () {

    $('.balance-button').click(function () {

        var url = $('#BalanceModal').data('url') + "/" + this.id;
        console.log(url);
        $.get(url, function (data) {
            $("#BalanceModal").html(data);
            $("#BalanceModal").modal('show');
        });
    });

});


$(document).ready(function () {

    $('.account-button').click(function () {

        var url = $('#AccountModal').data('url') + "/" + this.id;
        console.log(url);
        $.get(url, function (data) {
            $("#AccountModal").html(data);
            $("#AccountModal").modal('show');
        });
    });

});

$(document).ready(function () {

    $('.payment-button').click(function () {

        var url = $('#PaymentModal').data('url') + "/" + this.id;
        console.log(url);
        $.get(url, function (data) {
            $("#PaymentModal").html(data);
            $("#PaymentModal").modal('show');
        });
    });

});

$(document).ready(function () {

    $('.complete-button').click(function () {

        var url = $('#CompleteModal').data('url') + "/" + this.id;
        console.log(url);
        $.get(url, function (data) {
            $("#CompleteModal").html(data);
            $("#CompleteModal").modal('show');
        });
    });

});

$(document).ready(function () {

    $('.edit-transaction-button').click(function () {

        var url = $('#EditTransactionModal').data('url') + "/" + this.id;
        console.log(url);
        $.get(url, function (data) {
            $("#EditTransactionModal").html(data);
            $("#EditTransactionModal").modal('show');
        });
    });

});

$(document).ready(function () {

    $('.transaction-button').click(function () {

        var url = $('#TransactionModal').data('url');
        $.get(url, function (data) {
            $("#TransactionModal").html(data);
            $("#TransactionModal").modal('show');
        });
    });
});


$(document).ready(function () {

    $('.edit-button').click(function () {

        var url = $('#NickNameModal').data('url') + "/" + this.id;
        console.log(url);
        $.get(url, function (data) {
            $("#NickNameModal").html(data);
            $("#NickNameModal").modal('show');
        });
    });

});



$(function () {
    var placeholderElement = $('#modal-placeholder');
    $('button[data-toggle="modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            placeholderElement.find('.modal').modal('show');
        });
    });
});

//$('#register-user').submit(function (ev) {
//    ev.preventDefault();
//    console.log("test3");
//    var $this = $(this);
//    var url = $this.attr('action');
//    var dataToSend = $this.serialize();
//    if (!$this.valid()) {
//        toastr.options = {
//            "debug": false,
//            "positionClass": "toast-top-full-width",
//            "onclick": null,
//            "fadeIn": 300,
//            "fadeOut": 2000,
//            "timeOut": 3000,
//            "extendedTimeOut": 3000,
//            "closeButton": true
//        }
//        toastr.error("Data not Valid!");
//        return false;
//    };

//    $.post(url, dataToSend).done(function (response) {
//        toastr.options = {
//            "debug": false,
//            "positionClass": "toast-top-full-width",
//            "onclick": null,
//            "fadeIn": 300,
//            "fadeOut": 2000,
//            "timeOut": 3000,
//            "extendedTimeOut": 3000,
//            "closeButton": true
//        }

//        //TODO
//        toastr.success(response);

//        $('input[type="text"],textarea').val('');
//        $('#register-user').trigger("reset");
//        $.get("/Administration/Home/ShowAllUsers")
//            .done(function (data) {
//                $('#target-users-list-table').empty();

//                $('#target-users-list-table').append(data);
//            })
//            .fail(function (failResponse) {
//                toastr.options = {
//                    "debug": false,
//                    "positionClass": "toast-top-full-width",
//                    "onclick": null,
//                    "fadeIn": 300,
//                    "fadeOut": 1000,
//                    "timeOut": 3000,
//                    "extendedTimeOut": 3000,
//                    "closeButton": true
//                }
//                toastr.error(response.responseText);
//                console.log(response.responseText)

//                $('input[type="text"],textarea').val('');
//                $('#register-user').trigger("reset");
//            });

//    }).fail(function (response) {
//        toastr.options = {
//            "debug": false,
//            "positionClass": "toast-top-full-width",
//            "onclick": null,
//            "fadeIn": 300,
//            "fadeOut": 1000,
//            "timeOut": 3000,
//            "extendedTimeOut": 3000,
//            "closeButton": true
//        }
//        toastr.error(response.responseText);
//        console.log("test3");
//        console.log(response.responseText)
//        $('input[type="text"],textarea').val('');
//        $('#register-user').trigger("reset");
//    });
//});



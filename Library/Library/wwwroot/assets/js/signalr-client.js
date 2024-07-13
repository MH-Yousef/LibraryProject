//const { request } = require("undici-types");

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();
$(document).ready(function () {

    connection.start().then(function () {
        console.log("SignalR connected");
    }).catch(function (err) {
        return console.error(err.toString());
    });

});
connection.on("ReceiveFriendRequest", function (SenderId, fullName, Image) {
    showToastSend("New friend requset from ", fullName);
    var requestHtml = `
                    <div href="#" class="media new-message" style="align-items: center;">
                        <div class="media-left" style="margin: 10px;">
                            <img src="~/Images/Profile/${Image}" alt="user" class="rounded-circle thumb-lg">
                            <span class="round-10 bg-success"></span>
                        </div>
                        <div class="media-body mdbody">
                            <div class="d-inline-block">
                                <h6>Muhammed</h6>
                            </div>
                            <div class="btn-group" role="group" aria-label="Friend request actions" style="margin-right: 10px;">
                                <button class="btn btn-danger waves-effect waves-light reject-friend-btn" data-user-id="${SenderId}" data-sender="${fullName}" style="padding: 5px; font-size: 11px; line-height: 19px; margin-right: 8px;">Reject</button>
                                <button class="btn btn-success waves-effect waves-light accept-friend-btn" data-user-id="${SenderId}" data-sender="${fullName}" style="padding: 5px; font-size: 11px; line-height: 19px;">Accept</button>
                            </div>
                        </div>
                    </div>`;
    $('#notification-list').append(requestHtml);
});

connection.on("ReceiveFriendRequestAccepted", function (fullName) {
    toastr.success(fullName, "Friend Request Accepted");
});

connection.on("ReceiveFriendRequestRejected", function (fullName) {
    toastr.success(fullName, "Friend Request Rejected");
});

connection.on("ReceiveFriendRequestCanceled", function (fullName) {
    toastr.success(fullName, "Friend Request Canceled");
});

$('.add-friend-btn').on('click', function (event) {
    event.preventDefault();
    const targetUserId = $(this).data('receiver');
    var name = $(this).data('user-name');
    const sender = $(this).data('sender');
    var image = $(this).data('image');

    console.log('Target user name: ' + name);
    console.log('Receiver: ' + targetUserId);
    console.log('Sender: ' + sender);

    $.ajax({
        url: '/Notification/SendRequest',
        type: 'POST',
        data: { TargetUserId: targetUserId, SenderId: sender, fullName: name, Image: image },
        success: function (data) {
            showToastSend(data.title, data.message, data.date);
            console.log("dd");
        },
        error: function (xhr, status, error) {
            console.error('Failed to send friend request: ' + error);
        }
    });
});


function showToastSend(title, message) {
    toastr.success(message, title);
}

toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut",
    "iconClass": "toast-custom-icon",  // İkon sınıfı ekliyoruz
    "titleClass": "toast-header",      // Başlık sınıfı ekliyoruz
    "messageClass": "toast-message"   // Mesaj sınıfı ekliyoruz
};


// Accept friend request
$(document).on('click', '.accept-friend-btn', function (event) {
    event.preventDefault();
    const requestingUserId = $(this).data('user-id');
    const message = "Friend request accepted";

    $.ajax({
        url: '/Notification/AcceptRequest',
        type: 'POST',
        data: { TargetUserId: requestingUserId, Message: message },
        success: function () {
            toastr.success("Friend request accepted", "Success");
        },
        error: function (xhr, status, error) {
            console.error('Failed to accept friend request: ' + error);
        }
    });
});

// Cancel friend request
$(document).on('click', '.cancel-friend-btn', function (event) {
    event.preventDefault();
    const requestingUserId = $(this).data('receiver');
    const message = "Friend request canceled";

    $.ajax({
        url: '/Notification/CancelRequest',
        type: 'POST',
        data: { TargetUserId: requestingUserId, Message: message },
        success: function () {
            toastr.success("Friend request canceled", "Success");
        },
        error: function (xhr, status, error) {
            console.error('Failed to cancel friend request: ' + error);
        }
    });
});

// reject friend request
$(document).on('click', '.reject-friend-btn', function (event) {
    event.preventDefault();
    const requestingUserId = $(this).data('user-id');
    const message = "Friend request rejected";

    $.ajax({
        url: '/Notification/RejectRequest',
        type: 'POST',
        data: { TargetUserId: requestingUserId, Message: message },
        success: function () {
            showToastSend("Success", "Friend request rejected");
        },
        error: function (xhr, status, error) {
            console.error('Failed to reject friend request: ' + error);
        }
    });
});


$(document).ready(function() {
    $.ajax({
        url: "/TaskLists/CheckLists",
        success: function(result) {
            $("#checks").html(result);
        },
        error: function() {
            alert("Something goes wrong");
        }
    });
});
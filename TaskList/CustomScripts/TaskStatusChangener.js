$(document).ready(function() {
    $(".ActiveCheck").change(function() {
        var self = $(this);
        //var id = self.attr("id");
        var id = $(".ListId").attr("value");
        var value = self.prop("checked");

        $.ajax({
            url: "/TaskLists/ChangeTaskCompleteStatus",
            data: {
                id: id,
                value: value
            },
            type: "POST",
            success: function(result) {
                $("#tasks").html(result);
            }
        });
    });
})
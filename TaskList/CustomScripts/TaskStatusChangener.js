$(document).ready(function() {
    $(".ActiveCheck").change(function() {
        var self = $(this);
        var id = self.attr("id");
        var listId = $(".ListId").attr("value");
        //alert(id);
        var value = self.prop("checked");

        $.ajax({
            url: "/TaskLists/ChangeTaskCompleteStatus",
            data: {
                id: id,
                value: value,
                listId: listId
            },
            type: "POST",
            success: function(result) {
                $("#tasks").html(result);
            }
        });
    });
})
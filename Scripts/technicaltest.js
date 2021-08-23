$('#form-id').submit(function () {
    if ($(this).valid()) {
        $("#submit-button").attr("disabled", true).val("Uploading...");
    }
});
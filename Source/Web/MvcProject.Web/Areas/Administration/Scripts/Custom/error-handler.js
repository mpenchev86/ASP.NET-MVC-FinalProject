var errorHandler = (function () {
    function handler(e) {
        if (e.errors) {
            var message = "Errors:\n";
            $.each(e.errors, function handleErrors(key, value) {
                if ('errors' in value) {
                    $.each(value.errors, function concatErrorMessages() {
                        message += this + "\n";
                    });
                }
            });
            alert(message);
        }
    }

    return {
        handler: handler
    }
}());
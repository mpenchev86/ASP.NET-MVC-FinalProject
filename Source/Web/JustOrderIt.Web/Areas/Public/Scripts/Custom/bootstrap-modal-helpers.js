var bootstrapModalHelpers = (function BootstrapHelpers() {
    //// When 'data-toggle' attribute is not used, the modal window should be manually hidden when the page loads.
    //// Otherwise, the backdrop is initially active.
    function hideOnLoad(wrapperSelector) {
        $(window).load(function onLoad() {
            $(wrapperSelector).modal('hide');
        });
    }

    //// By default, bootstrap v3.3 modals cannot be reused. This helper empties the modal template's content.
    function clearContent(event, contentSelector) {
        //// source: http://stackoverflow.com/a/31018179/4491770
        $('body').on(event, '.modal', function (e) {
            $(e.target).removeData("bs.modal").find(contentSelector).empty();
        });
    }

    //// Used to prevent duplication of the ajax request caused by a mismatch between the bootstrap and unobtrusive
    //// api when 'data-toggle="modal"' and 'data-target="[selector]"' bootstrap attributes are set on the
    //// MVC Ajax.ActionLink helper
    function attachToggle(wrapperSelector, triggerSelector) {
        $(document).on('click', triggerSelector, function onClick(e) {
            $(wrapperSelector).modal('toggle');
        });
    }

    function setContent(wrapperSelector, contentSelector, data) {
        $(wrapperSelector).find(contentSelector).html(data);
    }

    function initializeLargeImageModal() {
        // Get the modal
        var modal = document.getElementById('large-image-modal');
        // Get the image and insert it inside the modal - use its "alt" text as a caption
        var img = document.getElementById('image-in-focus');
        var modalImg = document.getElementById("large-image");


        img.onclick = function () {
            // Disables browser scrolling while jQuery UI modal dialog is open
            $('body').css('overflow', 'hidden');
            modal.style.display = "block";
            modalImg.src = $('#modal-link').attr('href');
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
                // Enables browser scrolling when modal dialog is closed
                $('body').css('overflow', 'scroll');
            }
        }
    }

    return {
        hideOnLoad: hideOnLoad,
        clearContent: clearContent,
        attachToggle: attachToggle,
        setContent: setContent,
        initializeLargeImageModal: initializeLargeImageModal
    };
}());
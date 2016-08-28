var productImagesUpload = (function () {
    // Event handlers.
    function kendoUpload_onSuccess(e, productViewModel) {
        if (e.operation === 'upload') {
            $.merge(productViewModel.Images, e.response.productImages);
        }
        if (e.operation === 'remove') {
            var removedImagesIds = e.response.removedImagesIds;
            var viewModelImageIds = $.map(productViewModel.Images, function (val, i) {
                return val.Id;
            });

            $.each(removedImagesIds, function (index, val) {
                var ind = viewModelImageIds.indexOf(val);
                viewModelImageIds.splice(ind, 1)
                productViewModel.Images.splice(ind, 1);
            });
        }
    }

    function kendoUpload_onUpload(e) {
    }

    function kendoUpload_onRemove(e) {
        imageIds = $.map(e.files, function getImagesIds(val, index) {
            return val.imageId;
        });
        // key-value pairs sent to the server as additional information
        e.data = { imageIds: imageIds };
    }

    function kendoUpload_onError(e) {
        // Array with information about the files being uploaded
        var files = e.files;
        if (e.operation == "upload") {
            alert("Failed to upload " + files.length + " files");
        }
        if (e.operation == "remove") {
            alert("Failed to remove " + files.length + " files");
        }
    }

    // Data Workers
    function convertToKendoUploadFiles(images) {
        var initialFiles = [];
        if (images) {
            var len = images.length;
            for (var i = 0; i < len; i++) {
                initialFiles.push({
                    name: images[i].OriginalFileName,
                    url: images[i].UrlPath,
                    extension: images[i].FileExtension,
                    imageId: images[i].IdEncoded
                });
            }
        }

        return initialFiles;
    }

    // 
    function prepareInitialFiles(initialFiles, uploadedImageTemplate) {
        $('.k-file.k-file-success>.k-filename').each(function replaceContent() {
            var span = $(this);
            var title = span.attr("title");
            $.each(initialFiles, function (index, val) {
                if (val.name === title) {
                    span.replaceWith(uploadedImageTemplate({ name: val.name, url: val.url + "_tmbl" + val.extension }));
                }
            });
        });
    }

    function insertSaveTip() {
        // Append save tip to the upload widget.
        var saveTip =
            '<div class="save-upload-tip">' +
                '<span class="glyphicon glyphicon-exclamation-sign"></span>' +
                '<em> After you select images for upload, click the Upload button that will appear. When the upload is complete, click \'Save\' to save the entire form. Otherwise, the associations between product and files will be lost.' +
                '<em>' +
            '<div>';
        $('.k-widget.k-upload').after(saveTip);
    }

    return {
        kendoUpload_onSuccess,
        kendoUpload_onUpload,
        kendoUpload_onRemove,
        kendoUpload_onError,
        convertToKendoUploadFiles,
        prepareInitialFiles,
        insertSaveTip
    }
}());
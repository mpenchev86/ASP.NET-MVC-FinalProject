var productImagesUpload = (function () {
    function initialize(args, mainImageDropdown, saveTip, imageSizeSuffix) {
        isModelDirty = args.model.dirty;
        var initialFiles = convertToKendoUploadFiles(args.model.Images);
        
        $("#productImages").kendoUpload({
            async: {
                saveUrl: "/Administration/Products/SaveImages",
                saveField: "productImages",
                removeUrl: "/Administration/Products/RemoveImages",
                autoUpload: false,
                batch: false
            },
            multiple: true,
            files: initialFiles,
            success: function (e) {
                console.log(e);
                console.log('----------------------------------------------');
                console.log(args.model);
                onSuccess(e, args.model);
                // Reattaches the image DOM elements when the viewmodel's images collection has changed.
                attachImagesToDom(convertToKendoUploadFiles(args.model.Images), imageSizeSuffix, uploadedImageTemplate);

                // Depends on product-main-image-dropdown.js
                productMainImageDropDown.setDropDownData(mainImageDropdown, args.model.Images);

                // Forces the state of the grid model to dirty, so that an update request will occur.
                args.model.dirty = true;

                // It signals the grid's popup window Deactivate event to refresh the grid, because data has been changed.
                isModelDirty = true;
            },
            remove: function (e) {
                onRemove(e, args.model.Id);
            },
            error: function (e) {
                onError(e);
            }
        });

        var uploadedImageTemplate = kendo.template($('#uploaded-image-template').html());

        // Executed when the grid popup window activates.
        attachImagesToDom(initialFiles, imageSizeSuffix, uploadedImageTemplate);
        insertSaveTip(saveTip);
    }

    // Event handlers.
    function onSuccess(e, viewModel) {
        //console.log(e);
        //console.log('----------------------------------------------');
        //console.log(viewModel);
        if (e.operation == 'upload') {
            $.merge(viewModel.Images, e.response.productImages);
        }
        if (e.operation == 'remove') {
            var removedImagesIds = e.response.removedImagesIds;
            if (removedImagesIds.length !== 0) {
                var viewModelImageIds = $.map(viewModel.Images, function (val, i) {
                    return val.Id;
                });

                // Clears the product's viewmodel of the images that had been deleted.
                $.each(removedImagesIds, function (index, val) {
                    var ind = viewModelImageIds.indexOf(val);
                    viewModelImageIds.splice(ind, 1);
                    viewModel.Images.splice(ind, 1);
                });
            }
        }
    }

    function onRemove(e, productId) {
        images = $.map(e.files, function getImagesIds(val, index) {
            return {
                ImageId: val.imageId,
                IsMainImage: val.isMainImage,
                ProductId: productId
            };
        });
        // key-value pairs sent to the server as additional information
        //e.data = {};
        //e.data["images"] = JSON.stringify(images);
        e.data = { images: JSON.stringify(images) };
    }

    function onError(e) {
        // Array with information about the files being uploaded
        var files = e.files;
        if (e.operation === "upload") {
            alert("Failed to upload " + files.length + " files");
        }
        if (e.operation === "remove") {
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
                    imageId: images[i].IdEncoded,
                    isMainImage: images[i].IsMainImage
                });
            }
        }

        return initialFiles;
    }

    function attachImagesToDom(initialFiles, imageSizeSuffix, uploadedImageTemplate) {
        $('.k-file.k-file-success>.k-filename').each(function replaceContent() {
            var span = $(this);
            var title = span.attr("title");
            $.each(initialFiles, function (index, val) {
                if (val.name === title) {
                    span.replaceWith(uploadedImageTemplate({ name: val.name, url: val.url + imageSizeSuffix + val.extension }));
                }
            });
        });
    }

    function insertSaveTip(saveTip) {
        $('.k-widget.k-upload').after(saveTip);
    }

    function isGridModelIsDirty() {
        return isModelDirty;
    }

    return {
        initialize: initialize,
        isGridModelIsDirty: isGridModelIsDirty
    };
}());
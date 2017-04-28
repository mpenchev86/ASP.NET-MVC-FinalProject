var productImagesUpload = (function () {
    var isModelDirty = false;

    function initialize(args, mainImageDropdown, saveTip, imageSizeSuffix) {
        isModelDirty = args.model.dirty;
        var initialFiles = GetInitialFilesFromModel(args.model.Images);

        var uploadedImageTemplate = kendo.template($('#uploaded-image-template').html());

        $("#productImages").kendoUpload({
            async: {
                saveUrl: "/Administration/Products/SaveImages",
                saveField: "productImages",
                removeUrl: "/Administration/Products/RemoveImages",
                autoUpload: false,
                batch: false
            },
            localization: {
                retry: "probvai pak, ti mojesh"
            },
            multiple: true,
            files: initialFiles,
            success: function (e) {
                onSuccess(e, args.model);

                // Reattaches the image DOM elements when the viewmodel's images collection has changed.
                attachImagesToDom(e.sender.options.files, imageSizeSuffix, uploadedImageTemplate);

                // Depends on product-main-image-dropdown.js
                productMainImageDropDown.setDropDownData(mainImageDropdown, args.model.Images);

                // Forces the state of the grid model to dirty, so that an update request will occur.
                args.model.dirty = true;

                // It signals the grid's popup window Deactivate event to refresh the grid, because data has been changed.
                isModelDirty = args.model.dirty;
            },
            upload: function (e) {
                onUpload(e);
            },
            remove: function (e) {
                onRemove(e, args.model.Id);
            },
            error: function (e) {
                onError(e);
            }
        });

        // Executed when the grid popup window activates.
        attachImagesToDom(initialFiles, imageSizeSuffix, uploadedImageTemplate);
        insertSaveTip(saveTip);
    }

    // Event handlers.
    function onSuccess(e, viewModel) {
        if (e.operation === 'upload') {
            var tempViewModelFile = e.response.savedProductImages[0];
            viewModel.Images.push(tempViewModelFile);

            var tempKendoFile = convertResponseFile(e.response.savedProductImages[0], e.files[0].uid);
            e.sender.options.files.push(tempKendoFile);
        }

        if (e.operation === 'remove') {
            //// --------For Multiple Files--------
            //var removedImagesIds = e.response.removedImagesIds;
            //if (removedImagesIds.length !== 0) {
            //    var viewModelImageIds = $.map(viewModel.Images, function (val, i) {
            //        return val.Id;
            //    });

            //    // Clears the product's viewmodel of the images that had been deleted.
            //    $.each(removedImagesIds, function (index, val) {
            //        var ind = viewModelImageIds.indexOf(val);
            //        viewModelImageIds.splice(ind, 1);
            //        viewModel.Images.splice(ind, 1);
            //    });
            //}
            //// ------------------------------------

            // ----For Single File----
            var removedImageId = e.response.removedImagesIds[0];

            var viewModelImageIds = $.map(viewModel.Images, function (val, i) {
                return val.Id;
            });

            var index = viewModelImageIds.indexOf(removedImageId);
            viewModelImageIds.splice(index, 1);
            viewModel.Images.splice(index, 1);
        }
    }

    function onUpload(e) {
    }

    function onRemove(e, productId) {
        // -------For Multiple File-------
        var images = $.map(e.files, function getImagesIds(val, index) {
            return {
                IsMainImage: val.isMainImage,
                ProductId: productId,
                ImageUid: val.uid,
                ImageId: val.imageId
            };
        });

        $.each(images, function findFilesWithSameUid(index, value) {
            $.each(e.sender.options.files, function (i, val) {
                if (value.ImageUid == val.uid) {
                    value.ImageId = val.imageId;
                }
            });
        });

        e.data = { images: JSON.stringify(images) };

        //// -------For single file-------
        //var image = {
        //    ProductId: productId,
        //    ImageUid: e.files[0].uid,
        //    ImageId: e.files[0].imageId
        //}

        //$.each(e.sender.options.files, function (i, val) {
        //    if (image.ImageUid == val.uid) {
        //        image.ImageId = val.imageId;
        //    }
        //});

        //e.data = { images: JSON.stringify(image) };
        //// ------------------------------
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
    function GetInitialFilesFromModel(images) {
        var initialFiles = [];
        if (images) {
            var len = images.length;
            for (var i = 0; i < len; i++) {
                initialFiles.push({
                    name: images[i].OriginalFileName,
                    url: images[i].UrlPath,
                    extension: images[i].FileExtension,
                    imageId: images[i].IdEncoded,
                    isMainImage: images[i].IsMainImage,
                    uid: images[i].ImageUid
                });
            }
        }

        return initialFiles;
    }

    // Convert single Model.Image to kendo image
    function convertResponseFile(image, uid) {
        return {
            name: image.OriginalFileName,
            url: image.UrlPath,
            extension: image.FileExtension,
            imageId: image.IdEncoded,
            isMainImage: image.IsMainImage,
            uid: uid
        };
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
var gridDetailsHelpers = (function () {
    function setDataSource(gridName, data, pageSize) {
        var detailGrid = $(gridName).data("kendoGrid"),
            dataSource = new kendo.data.DataSource({ data: data, page: 1, pageSize: pageSize });

        // This function is in datetime-handlers.js
        datetimeHandlers.normalizeDateProperties(data);

        // Sets the dataSource to the selected grid
        dataSource.fetch(function fetchGridDataSource() {
            detailGrid.setDataSource(dataSource);
        });

        // Refreshes the grid dataSource so that paging works correctly
        detailGrid.dataSource.read();
    }

    function populateProductDescription(productId, description, propertiesGridPageSize) {
        if (description) {
            datetimeHandlers.normalizeDateProperties(description);
            if (description.Id) {
                $('#description-id_' + productId).text(description.Id);
            }

            if (description.Content) {
                $('div#description-content_' + productId).text(description.Content);
            }

            if (description.CreatedOn) {
                $('div#description-createdOn_' + productId).text(description.CreatedOn);
            }

            if (description.ModifiedOn) {
                $('div#description-modifiedOn_' + productId).text(description.ModifiedOn);
            }

            if (description.Properties) {
                setDataSource('#properties-grid_' + productId, description.Properties, propertiesGridPageSize);
            }
        }
    }

    function populateProductSeller(productId, seller) {
        if (seller) {
            if (seller.Id) {
                $('#seller-id_' + productId).text(seller.Id);
            }

            if (seller.Name) {
                $('div#seller-name_' + productId).text(seller.Name);
            }
        }
    }

    return {
        setDataSource: setDataSource,
        populateProductDescription: populateProductDescription,
        populateProductSeller: populateProductSeller,
    };
}());
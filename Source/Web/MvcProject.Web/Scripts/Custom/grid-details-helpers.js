var gridDetailsHelpers = (function () {
    function setDataSource(gridName, data, pageSize) {
        var detailGrid = $(gridName).data("kendoGrid");
        // This function is in Scripts/Custom/datetime-handlers.js
        //dataHandler(data);
        datetimeHandlers.normalizeDateProperties(data);

        var dataSource = new kendo.data.DataSource({
            data: data,
            page: 1,
            pageSize: pageSize
        });

        // Sets the dataSource to the selected grid
        dataSource.fetch(function () {
            detailGrid.setDataSource(dataSource);
        });

        // Refreshes the grid dataSource so that paging correct
        detailGrid.dataSource.read();
    }

    function setProductDescription(productId, description, propertiesGridPageSize) {
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

        //$('#tabStrip_' + id + '-1').ready(function myfunction() {
        //    console.log($('#tabStrip_' + id + '-1').html());
        //    console.log($('#description-id').text());
        //});
    }

    return {
        setDataSource: setDataSource,
        setProductDescription: setProductDescription
    }
}());
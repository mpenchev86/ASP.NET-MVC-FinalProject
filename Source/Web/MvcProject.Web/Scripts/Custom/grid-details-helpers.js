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

    function populateProductStatistics(productId, statistics) {
        if (statistics) {
            datetimeHandlers.normalizeDateProperties(statistics);
            if (statistics.Id) {
                $('#statistics-id_' + productId).text(statistics.Id);
            }

            if (statistics.Content) {
                $('div#statistics-allTimesItemsBought_' + productId).text(statistics.AllTimesItemsBought);
            }

            if (statistics.ModifiedOn) {
                $('div#statistics-overAllRating_' + productId).text(statistics.OverAllRating);
            }

            if (statistics.CreatedOn) {
                $('div#statistics-createdOn_' + productId).text(statistics.CreatedOn);
            }

            if (statistics.ModifiedOn) {
                $('div#statistics-modifiedOn_' + productId).text(statistics.ModifiedOn);
            }
        }
    }

    return {
        setDataSource: setDataSource,
        populateProductDescription: populateProductDescription,
        populateProductStatistics: populateProductStatistics
    };
}());
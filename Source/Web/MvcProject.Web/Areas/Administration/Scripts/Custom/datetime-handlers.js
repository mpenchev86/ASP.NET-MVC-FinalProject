var datetimeHandlers = (function () {
    function normalizeDateProperties(data) {
        if (data && typeof(data) === 'object') {
            for (var i in data) {
                normalizeDateProperties(data[i]);
            }

            if (data.CreatedOn) {
                data.CreatedOn = kendo.toString(kendo.parseDate(data.CreatedOn, "G"));
            }

            if (data.ModifiedOn) {
                data.ModifiedOn = kendo.toString(kendo.parseDate(data.ModifiedOn, "G"));
            }

            if (data.DeletedOn) {
                data.DeletedOn = kendo.toString(kendo.parseDate(data.DeletedOn, "G"));
            }
        }
    }

    return {
        normalizeDateProperties: normalizeDateProperties
    }
}());

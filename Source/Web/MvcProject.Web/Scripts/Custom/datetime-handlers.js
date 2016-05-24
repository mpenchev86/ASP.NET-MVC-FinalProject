var datetimeHandlers = (function myfunction() {
    //function normalizeComplexPropertiesDates(data, complexProperties) {
    //    var entityName;
    //    if (data) {
    //        if (complexProperties.length > 0) {
    //            for (var i = 0; i < complexProperties.length; i++) {
    //                entityName = complexProperties[i];
    //                if (data[entityName]) {
    //                    data[entityName].CreatedOn = data[entityName] ? kendo.toString(kendo.parseDate(data[entityName].CreatedOn, "G")) : "";
    //                    data[entityName].ModifiedOn = data[entityName] ? kendo.toString(kendo.parseDate(data[entityName].ModifiedOn, "G")) : "";
    //                    data[entityName].DeletedOn = data[entityName] ? kendo.toString(kendo.parseDate(data[entityName].DeletedOn, "G")) : "";
    //                }
    //            }
    //        }
    //    }
    //}

    function normalizeDateProperties(data) {
        if (data && typeof(data) === 'object') {
            for (var i in data) {
                normalizeDateProperties(data[i]);
            }

            if (data.CreatedOn) {
                data.CreatedOn = data ? kendo.toString(kendo.parseDate(data.CreatedOn, "G")) : "";
            }

            if (data.ModifiedOn) {
                data.ModifiedOn = data ? kendo.toString(kendo.parseDate(data.ModifiedOn, "G")) : "";
            }

            if (data.DeletedOn) {
                data.DeletedOn = data ? kendo.toString(kendo.parseDate(data.DeletedOn, "G")) : "";
            }
        }
    }

    return {
        //normalizeComplexPropertiesDates: normalizeComplexPropertiesDates,
        normalizeDateProperties: normalizeDateProperties
    }
}());

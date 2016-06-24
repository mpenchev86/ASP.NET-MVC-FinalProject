var collectionHelpers = (function () {
    function idsCollection(collection) {
        if (collection.length == 0) {
            return "";
        }

        var template = "";

        for (var i = 0; i < collection.length - 1; i++) {
            template = template + collection[i] + ", ";
        }

        template = template + collection[collection.length - 1];

        return template;
    }

    function objectIdsCollection(collection) {
        if (collection.length == 0) {
            return "";
        }

        var template = "";

        for (var i = 0; i < collection.length; i++) {
            template = template +
                "<p>" +
                "ID: " + collection[i].Id +
                "; " + "Content: " + collection[i].Content +
                "; " + collection[i].UserId +
                "</p>"
            ;
        }

        return template;
    }

    return {
        idsCollection: idsCollection,
        objectIdsCollection: objectIdsCollection
    }
}())
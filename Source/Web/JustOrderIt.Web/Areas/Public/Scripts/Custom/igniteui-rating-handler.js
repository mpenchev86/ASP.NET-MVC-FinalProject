var ratingHandler = (function RatingHandler() {
    function configureDisplayWidgets(selectorBase) {
        //for (var i in numberOfWidgets) {
        //    $(selectorBase + i.toString()).igRating("valueHover", 1);
        //}

        //$(selectorBase + "0").igRating({
        //    valueHover: 5
        //});

        $("#igRating0").igRating("option", "valueHover", 5);
        $("#igRating0").igRating("option", "cssVotes", [])
    }

    return {
        configureDisplayWidgets: configureDisplayWidgets
    }
}())
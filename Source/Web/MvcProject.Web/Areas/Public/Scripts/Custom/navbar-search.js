$(document).ready(function onDocumentReady() {
    $('#navbar-search-query').autocomplete({
        minLength: 3,
        source: function autocompleteSource(request, response) {
            $.ajax({
                url: "/Public/Search/SearchAutoComplete",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify({ 'prefix': request.term }),
                //data: { 'prefix' : request.term },
                success: function onAutocompleteSuccess(data) {
                    //console.log(data);
                    //response($.map(data, function (item) {
                    //    return item;
                    //}));
                    response(data);
                    //console.log(request);
                    //console.log(response);
                },
                error: function onAutocompleteError(err) {
                    console.log(err);
                    //console.log(request);
                    //console.log(response);
                }
            });
        },
        //focus: function () {
        //    // prevent value inserted on focus
        //    return false;
        //},
        select: function (event, ui) {
            //console.log(event);
            //console.log(ui);

            //if (event.keyCode == 13) {
            //    //$(this).next("input").focus().select();
            //    $(this).closest('form').submit();
            //}
            
            //var terms = split(this.value);
            //// remove the current input
            //terms.pop();
            //// add the selected item
            //terms.push(ui.item.value);
            //// add placeholder to get the comma-and-space at the end
            //terms.push("");
            //this.value = terms.join(", ");
            //return false;
        }
    });

    $('#navbar-search-query').keypress(function searchInputKeypress(event) {
        if (event.which == 13) {
            event.preventDefault();
            //console.log(this);
            $(this).closest('form').submit();
        }
    });
});

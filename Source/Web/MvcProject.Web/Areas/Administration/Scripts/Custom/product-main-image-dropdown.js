var productMainImageDropDown = (function () {
    function initialize(productViewModel, mainImageDropdown) {
        setDropDownData(mainImageDropdown, productViewModel.Images);
        mainImageDropdown.value(productViewModel.MainImageId);
        mainImageDropdown.refresh();
    }

    function setDropDownData(dropdown, data) {
        var dataSource = new kendo.data.DataSource({
            data: data
        });
        dropdown.setDataSource(dataSource);
    }

    return {
        initialize: initialize,
        setDropDownData: setDropDownData
    };
}());
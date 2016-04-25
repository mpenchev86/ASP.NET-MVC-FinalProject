namespace MvcProject.Web.Infrastructure.HtmlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.UI.Fluent;

    public static class KendoHelpers
    {
        public static GridBuilder<T> FullFeaturedGrid<T>(
            this HtmlHelper helper,
            string controllerName,
            object routeValues,
            Expression<Func<T, object>> modelIdExpression,
            int pageSize,
            bool virtualScroll = false,
            Action<GridColumnFactory<T>> columns = null,
            GridEditMode editMode = GridEditMode.PopUp,
            Action<GridSortSettingsBuilder<T>> sortSettings = null,
            Action<GridFilterableSettingsBuilder> filterSettings = null)
            where T : class
        {
            if (columns == null)
            {
                columns = cols =>
                {
                    cols.AutoGenerate(true);
                    cols.Command(c => c.Edit());
                    cols.Command(c => c.Destroy());
                };
            }

            if (sortSettings == null)
            {
                sortSettings = s => { };
            }

            if (filterSettings == null)
            {
                filterSettings = f => { };
            }

            return helper.Kendo()
                .Grid<T>()
                .Name("grid")
                .Columns(columns)
                .ColumnMenu()
                .Pageable(page => page.Refresh(true))
                .Sortable(sortSettings)
                .Groupable()
                .Scrollable(scrollable => scrollable
                    .Virtual(virtualScroll)
                    .Height(500)/*.Enabled(true)*/)
                .Reorderable(reorderable => reorderable.Columns(true))
                .Resizable(resizable => resizable.Columns(true))
                .Filterable(filterSettings)
                .Editable(edit => edit
                    .Mode(editMode)
                    .Window(w => w
                        .Title(false)
                        .Resizable()))
                .ToolBar(toolbar => toolbar.Create())
                .DataSource(data => data
                    .Ajax()
                    .PageSize(pageSize)
                    .Model(m => m.Id(modelIdExpression))
                    .Read(read => read.Action("Read", controllerName, routeValues))
                    .Create(create => create.Action("Create", controllerName))
                    .Update(update => update.Action("Update", controllerName))
                    .Destroy(destroy => destroy.Action("Destroy", controllerName)));
        }

        public static ListViewBuilder<T> ListViewHelper<T>(
            this HtmlHelper helper,
            string wrapperId,
            string wrapperTagName,
            string templateId,
            string controllerName,
            Expression<Func<T, object>> modelIdExpression,
            int pageSize,
            Action<DataSourceFilterDescriptorFactory<T>> filterSettings = null,
            Action<DataSourceSortDescriptorFactory<T>> sortSettings = null,
            bool isServerOps = true)
            where T : class
        {
            if (filterSettings == null)
            {
                filterSettings = f => { };
            }

            if (sortSettings == null)
            {
                sortSettings = s => { };
            }

            return helper.Kendo()
                .ListView<T>()
                .Name(wrapperId)
                .TagName(wrapperTagName)
                .ClientTemplateId(templateId)
                .Editable()
                .Pageable()
                // These two are for selection
                //.Navigatable()
                //.Selectable()
                .DataSource(source => source
                    .ServerOperation(isServerOps)
                    .Model(m => m.Id(modelIdExpression))
                    .Read(read => read.Action("Read", controllerName))
                    .Create(create => create.Action("Create", controllerName))
                    .Update(update => update.Action("Update", controllerName))
                    .Destroy(destroy => destroy.Action("Destroy", controllerName))
                    .PageSize(pageSize)
                    .Filter(filterSettings)
                    .Sort(sortSettings)
                    .AutoSync(false));
        }
    }
}

namespace MvcProject.Web.Infrastructure.HtmlHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security.AntiXss;
    using System.Web.Util;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.UI.Fluent;

    public static class KendoHelpers
    {
        public static GridBuilder<T> FullFeaturedGrid<T>(
            this HtmlHelper helper,
            string gridName,
            string controllerName,
            object routeValues,
            int pageSize,
            string readAction = "Read",
            string createAction = "Create",
            string updateAction = "Update",
            string destroyAction = "Destroy",
            bool scrollable = true,
            bool virtualScroll = false,
            int height = 500,
            //Expression<Func<T, IComparable>> initialSort = null,
            Action<DataSourceModelDescriptorFactory<T>> model = null,
            Action<GridColumnFactory<T>> columns = null,
            Action<GridToolBarCommandFactory<T>> toolbar = null,
            Action<DataSourceEventBuilder> dataSourceEvents = null,
            Action<GridEventBuilder> gridEvents = null,
            Action<DataSourceAggregateDescriptorFactory<T>> aggregates = null,
            Action<GridSortSettingsBuilder<T>> sortSettings = null,
            Action<GridFilterableSettingsBuilder> filterSettings = null,
            Action<GridEditingSettingsBuilder<T>> editingSettings = null,
            object htmlAttributes = null,
            bool isBatch = false,
            bool isServerOperation = false,
            string readHandler = null,
            string createHandler = null,
            string updateHandler = null,
            string destroyHandler = null)
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

            if (toolbar == null)
            {
                toolbar = t => { t.Create(); };
            }

            if (dataSourceEvents == null)
            {
                dataSourceEvents = e => { };
            }

            if (gridEvents == null)
            {
                gridEvents = g => { };
            }

            if (aggregates == null)
            {
                aggregates = s => { };
            }

            if (sortSettings == null)
            {
                sortSettings = s => { };
            }

            if (filterSettings == null)
            {
                filterSettings = f => { };
            }

            if (editingSettings == null)
            {
                editingSettings = e => { e.Mode(GridEditMode.PopUp); };
            }

            if (htmlAttributes == null)
            {
                htmlAttributes = new { };
            }

            return helper.Kendo()
                .Grid<T>()
                .Name(gridName)
                .HtmlAttributes(htmlAttributes)
                .Columns(columns)
                .ColumnMenu()
                .Pageable(page => page
                    .Refresh(true)
                    .Input(true)
                    .PageSizes(new int[] { 20, 50, 200, 500 }))
                .Sortable(sortSettings)
                .Groupable()
                .Scrollable(scroll => scroll
                    .Enabled(scrollable)
                    .Virtual(virtualScroll)
                    .Height(height))
                .Reorderable(reorderable => reorderable.Columns(true))
                .Resizable(resizable => resizable.Columns(true))
                .Filterable(filterSettings)
                .Editable(editingSettings)
                .Events(gridEvents)
                .ToolBar(toolbar)
                .DataSource(data => data
                    .Ajax()
                    .Batch(isBatch)
                    .ServerOperation(isServerOperation)
                    .ServerOperation(isServerOperation)
                    .Events(dataSourceEvents)
                    .PageSize(pageSize)
                    .Model(model)
                    //.Sort(s => (initialSort != null ? s.Add(initialSort) : s.Add("Id")).Ascending())
                    .Aggregates(aggregates)
                    .Read(read => read.Action(readAction, controllerName, routeValues).Data(readHandler))
                    .Create(create => create.Action(createAction, controllerName, routeValues).Data(createHandler))
                    .Update(update => update.Action(updateAction, controllerName, routeValues).Data(updateHandler))
                    .Destroy(destroy => destroy.Action(destroyAction, controllerName, routeValues).Data(destroyHandler)));
        }

        public static GridBuilder<T> ClientDetailsGrid<T>(
            this HtmlHelper helper,
            string name,
            Action<GridColumnFactory<T>> columns = null,
            bool scrollable = true,
            int height = 400)
            where T : class
        {
            if (columns == null)
            {
                columns = cols =>
                {
                    cols.AutoGenerate(true);
                };
            }

            return helper.Kendo()
                .Grid<T>()
                .Name(name)
                .Columns(columns)
                .Pageable(page => page
                    .Refresh(true)
                    .Input(true))
                .Scrollable(scroll => scroll
                    .Enabled(scrollable)
                    .Virtual(false)
                    .Height(height))
                .Sortable()
                .HtmlAttributes(new { @class = "details-grid" })
                .Resizable(resizable => resizable.Columns(true));
        }

        public static GridBuilder<T> ClientDetailsGridWithDataSource<T>(
            this HtmlHelper helper,
            string name,
            string action,
            string controller,
            object routeValues,
            int pageSize,
            Action<GridColumnFactory<T>> columns = null,
            string readHandler = null)
            where T : class
        {
            if (columns == null)
            {
                columns = cols =>
                {
                    cols.AutoGenerate(true);
                };
            }

            return helper.Kendo()
                .Grid<T>()
                .Name(name)
                .Columns(columns)
                .DataSource(data => data
                    .Ajax()
                    .PageSize(pageSize)
                    .Read(read => read.Action(action, controller, routeValues).Data(readHandler)))
                .Pageable()
                .Resizable(resizable => resizable.Columns(true))
                .Sortable();
        }

        public static ListViewBuilder<T> GenericListViewHelper<T>(
            this HtmlHelper helper,
            string name,
            string wrapperTagName,
            string templateId,
            string controllerName,
            Expression<Func<T, object>> modelIdExpression,
            int pageSize,
            string readAction = "Read",
            string createAction = "Create",
            string updateAction = "Update",
            string destroyAction = "Destroy",
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
                .Name(name)
                .TagName(wrapperTagName)
                .ClientTemplateId(templateId)
                .Editable()
                .Pageable()
                .DataSource(source => source
                    .ServerOperation(isServerOps)
                    .Model(m => m.Id(modelIdExpression))
                    .Read(read => read.Action(readAction, controllerName))
                    .Create(create => create.Action(createAction, controllerName))
                    .Update(update => update.Action(updateAction, controllerName))
                    .Destroy(destroy => destroy.Action(destroyAction, controllerName))
                    .PageSize(pageSize)
                    .Filter(filterSettings)
                    .Sort(sortSettings)
                    .AutoSync(false));
        }

        public static ListViewBuilder<T> SimpleListViewHelper<T>(
            this HtmlHelper helper,
            string name,
            string wrapperTagName,
            string templateId,
            string controllerName,
            Expression<Func<T, object>> modelIdExpression,
            int pageSize,
            string readAction = "Read",
            string createAction = "Create",
            string updateAction = "Update",
            string destroyAction = "Destroy",
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
                .Name(name)
                .TagName(wrapperTagName)
                .ClientTemplateId(templateId)
                .Editable()
                .DataSource(source => source
                    .ServerOperation(isServerOps)
                    .Model(m => m.Id(modelIdExpression))
                    .Read(read => read.Action(readAction, controllerName))
                    .Create(create => create.Action(createAction, controllerName))
                    .Update(update => update.Action(updateAction, controllerName))
                    .Destroy(destroy => destroy.Action(destroyAction, controllerName))
                    .Filter(filterSettings)
                    .Sort(sortSettings)
                    .AutoSync(false));
        }

        public static TabStripBuilder TabStripHelper(
            this HtmlHelper helper,
            string name,
            int selectedIndex,
            Action<TabStripItemFactory> items,
            Action<PopupAnimationBuilder> animation = null)
        {
            if (animation == null)
            {
                animation = a => a.Open(open => open.Fade(FadeDirection.In));
            }

            return helper.Kendo()
                .TabStrip()
                .Name(name)
                .SelectedIndex(selectedIndex)
                .Animation(animation)
                .Items(items);
        }

        // Possible fix for "\u0026#32;" instead of " " in rendered html
        public static IHtmlString ToMvcClientTemplate(this MvcHtmlString mvcString)
        {
            if (HttpEncoder.Current.GetType() == typeof(AntiXssEncoder))
            {
                var initial = mvcString.ToHtmlString();
                var corrected = initial.Replace("\\u0026", "&").Replace("%23", "#").Replace("%3D", "=").Replace("&#32;", " ");
                return new HtmlString(corrected);
            }

            return mvcString;
        }

        public static WindowBuilder ConfirmWindow(
            this HtmlHelper helper,
            string name,
            string title,
            string action,
            string controller,
            object routeValues = null,
            Action<WindowActionsBuilder> actions = null)
        {
            if (actions == null)
            {
                actions = a => a.Minimize().Maximize().Close();
            }

            if (routeValues == null)
            {
                routeValues = new { };
            }

            return helper.Kendo()
                .Window()
                .Name(name)
                .Title(title)
                .LoadContentFrom(action, controller, routeValues)
                .Actions(actions)
                .Animation(true);
        }
    }
}

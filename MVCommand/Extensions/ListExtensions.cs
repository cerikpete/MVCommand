using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCommand.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Maps generic list of items to a list of SelectListItems for use in a drop down list.
        /// </summary>
        /// <typeparam name="T">Object which is being converted to a SelectListItem</typeparam>
        /// <param name="itemsToMap">List of objects to map to a list of SelectListItems</param>
        /// <param name="textProperty">Property on the object being mapped whose value should be placed in the Text property of the SelectListItem</param>
        /// <param name="valueProperty">Property on the object being mapped whose value should be placed in the Value property of the SelectListItem</param>
        /// <param name="isSelected">Function indicating whether or not an item should be set to selected</param>
        /// <param name="defaultItem">Default item for the list</param>
        /// <returns>List of SelectListItems</returns>
        public static IEnumerable<SelectListItem> MapListToListOfSelectItems<T>(this IEnumerable<T> itemsToMap, Func<T, string> textProperty, Func<T, string> valueProperty, Predicate<T> isSelected, SelectListItem defaultItem)
        {
            IList<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var item in itemsToMap)
            {
                SelectListItem selectListItem = new SelectListItem();
                selectListItem.Value = valueProperty(item);
                selectListItem.Text = textProperty(item);

                if (isSelected != null)
                {
                    selectListItem.Selected = isSelected(item);
                }
                selectListItems.Add(selectListItem);
            }

            if (defaultItem != null)
            {
                selectListItems.Insert(0, defaultItem);
            }

            return selectListItems;
        }
    }
}
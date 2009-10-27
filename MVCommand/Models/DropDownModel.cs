using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MVCommand.Extensions;

namespace MVCommand.Models
{
    /// <summary>
    /// Model to wrap the mapping of a list of items to a list of SelectListItems.  Useful so we have a unique object in the ViewDataDictionary for
    /// each drop down list of items.
    /// </summary>
    public class DropDownModel<Model>
    {
        private readonly Predicate<Model> _isSelected;
        private readonly IEnumerable<Model> _itemsToMap;
        private readonly Func<Model, string> _textProperty;
        private readonly Func<Model, string> _valueProperty;
        private readonly SelectListItem _defaultItem;

        public DropDownModel(IEnumerable<Model> itemsToMap, Func<Model, string> textProperty, Func<Model, string> valueProperty, SelectListItem defaultItem) : this(itemsToMap, textProperty, valueProperty, null, defaultItem)
        {
        }

        public DropDownModel(IEnumerable<Model> itemsToMap, Func<Model, string> textProperty, Func<Model, string> valueProperty, Predicate<Model> isSelected, SelectListItem defaultItem)
        {
            _itemsToMap = itemsToMap;
            _textProperty = textProperty;
            _valueProperty = valueProperty;
            _defaultItem = defaultItem;
            _isSelected = isSelected;
        }

        public IEnumerable<SelectListItem> SelectListItems
        {
            get
            {
                return _itemsToMap.MapListToListOfSelectItems(_textProperty, _valueProperty, _isSelected, _defaultItem);
            }
        }
    }
}
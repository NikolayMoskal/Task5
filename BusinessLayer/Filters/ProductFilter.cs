using System;
using BusinessLayer.Models;

namespace BusinessLayer.Filters
{
    public class ProductFilter : Filter
    {
        private readonly ProductModel _templateModel;

        public ProductFilter(ProductModel templateModel)
        {
            _templateModel = templateModel;
        }

        public override bool IsValid(object obj)
        {
            if (!(obj is ProductModel model)) return false;

            if (_templateModel == null) return true;

            if (string.IsNullOrEmpty(_templateModel.Name) && Math.Abs(_templateModel.Price) < 0.1)
                return true;

            if (!string.IsNullOrEmpty(_templateModel.Name) && model.Name == _templateModel.Name)
                return true;

            return _templateModel.Price >= model.Price;
        }
    }
}
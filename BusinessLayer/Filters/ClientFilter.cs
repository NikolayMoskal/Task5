using BusinessLayer.Models;

namespace BusinessLayer.Filters
{
    public class ClientFilter : Filter
    {
        private readonly ClientModel _templateModel;

        public ClientFilter(ClientModel templateModel)
        {
            _templateModel = templateModel;
        }

        public override bool IsValid(object obj)
        {
            if (!(obj is ClientModel model)) return false;

            return _templateModel?.Name == null ||
                   string.IsNullOrEmpty(_templateModel.Name) ||
                   model.Name == _templateModel.Name;
        }
    }
}
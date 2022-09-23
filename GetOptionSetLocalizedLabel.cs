public List<OptionSetDetails> GetOptionSetValueLabel(string entityName, string fieldName)
        {
            var attReq = new RetrieveAttributeRequest();
            attReq.EntityLogicalName = entityName;
            attReq.LogicalName = fieldName;
            attReq.RetrieveAsIfPublished = true;

            var attResponse = (RetrieveAttributeResponse)Service.Execute(attReq);
            var attributeMetadata = (EnumAttributeMetadata)attResponse.AttributeMetadata;

            List<OptionSetDetails> listOptions = new List<OptionSetDetails>();
            foreach (var option in attributeMetadata.OptionSet.Options)
            {
                OptionSetDetails obj = new OptionSetDetails();
                obj.Value = option.Value;
                obj.Text = option.Label.UserLocalizedLabel.Label;
                listOptions.Add(obj);
            }
            return listOptions;
        }


public class OptionSetDetails
    {
        public int? Value { get; set; }
        public string Text { get; set; }
    }

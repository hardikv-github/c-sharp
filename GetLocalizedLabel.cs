public static List<OptionSetDetails> GetOptionSetValueLabel(string entityName, string fieldName)
        {
            RetrieveAttributeRequest attRequest = new RetrieveAttributeRequest();
            attRequest.EntityLogicalName = entityName;
            attRequest.LogicalName = fieldName;
            attRequest.RetrieveAsIfPublished = true;

            RetrieveAttributeResponse attResponse = (RetrieveAttributeResponse)Service.Execute(attRequest);
            EnumAttributeMetadata attributeMetadata = (EnumAttributeMetadata)attResponse.AttributeMetadata;

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

        public static string GetBoolText(string entitySchemaName, string attributeSchemaName, bool value)
        {
            RetrieveAttributeRequest attRequest = new RetrieveAttributeRequest();
            attRequest.EntityLogicalName = entitySchemaName;
            attRequest.LogicalName = attributeSchemaName;
            attRequest.RetrieveAsIfPublished = true;

            RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)Service.Execute(attRequest);
            BooleanAttributeMetadata retrievedBooleanAttributeMetadata = (BooleanAttributeMetadata)retrieveAttributeResponse.AttributeMetadata;
            string boolText = string.Empty;
            if (value)
                boolText = retrievedBooleanAttributeMetadata.OptionSet.TrueOption.Label.UserLocalizedLabel.Label;
            else
                boolText = retrievedBooleanAttributeMetadata.OptionSet.FalseOption.Label.UserLocalizedLabel.Label;
            return boolText;
        }


public class OptionSetDetails
    {
        public int? Value { get; set; }
        public string Text { get; set; }
    }

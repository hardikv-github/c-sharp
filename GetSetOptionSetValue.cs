//Get Option Set Value
int clientTypeData = ((OptionSetValue)opportunityResult[0].Attributes["new_clienttype"]).Value;
string clientTypeFormattedData = opportunityResult[0].FormattedValues["new_clienttype"].ToString();

//Set Option Set Value
objOpportunity["new_clienttype"] = new OptionSetValue(Convert.ToInt32(optionValue));


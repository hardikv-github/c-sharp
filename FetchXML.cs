EntityCollection productsResults = new EntityCollection();
            string fetchQuery = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                      <entity name='productpricelevel'>
                                        <attribute name='productid' />
                                        <attribute name='uomid' />
                                        <attribute name='amount' />
                                        <order attribute='productid' descending='false' />
                                        <filter type='and'>
                                          <condition attribute='pricelevelid' operator='eq'  uitype='pricelevel' value='{559242d0-eb26-ea11-a810-000d3a4df348}' />
                                        </filter>
                                         <link-entity name='product' from='productid' to='productid' link-type='inner' alias='ab'>
                                              <filter type='and'>
                                                <condition attribute='statecode' operator='eq' value='0' />
                                               </filter>
                                                 <attribute name='productnumber' />
                                        </link-entity>
                                      </entity>
                                    </fetch>";
            productsResults = service.RetrieveMultiple(new FetchExpression(fetchQuery));
            string productNumberData = ((AliasedValue)productsResults[0].Attributes["ab.productnumber"]).Value.ToString();


            if (productsResults.Entities.Count > 0)
            {                        
                        for (int i = 0; i < productsResults.Entities.Count(); i++)
                        {
                        }
            }

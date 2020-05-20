 ClientCredentials clientCredentials = new ClientCredentials();
            Uri crmUrl = new Uri("https://stratus.crm.dynamics.com/XRMServices/2011/Organization.svc");
            clientCredentials.UserName.UserName = "akhasia@stratus.hr";
            clientCredentials.UserName.Password = "Prompt@123";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IOrganizationService service = new OrganizationServiceProxy(crmUrl, null, clientCredentials, null);

           
            var fetchAccount = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' aggregate='true' distinct='true'>
                          <entity name='account'>
                            <attribute name='name' alias='name' groupby='true'/>
                         
                            <filter type='and'>
                              <condition attribute='new_pepmrate' operator='not-null' />
                              <condition attribute='new_professionalrate' operator='not-null' />
                              <condition attribute='new_adminrate' operator='not-null' />
                              <condition attribute='customertypecode' operator='eq' value='3' />
                              <condition attribute='new_clientstage' operator='in'>
                                <value>100000002</value>
                                <value>100000001</value>
                                <value>100000003</value>
                              </condition>
                              <condition attribute='statecode' operator='eq' value='0' />
                              <filter type='and'>
                                <condition attribute='new_clientid' operator='not-null' />
                                <condition attribute='new_clientid' operator='not-like' value='%X%' />
                                <condition attribute='new_clientid' operator='not-like' value='%N/A%' />
                              </filter>
                            </filter>
                            <link-entity name='new_accountlineitems' from='new_account' to='accountid' aggregate='true' link-type='outer' alias='ak' >
                                <attribute name='createdon' aggregate='count' alias='RecC' />
                            </link-entity>
                               
                          </entity>
                        </fetch>";


            DataCollection<Entity> accountsResult = service.RetrieveMultiple(new FetchExpression(fetchAccount)).Entities;

            if (accountsResult.Count > 0)
            {
                for (int i = 0; i < accountsResult.Count; i++)
                {

                    string accountName = ((AliasedValue)accountsResult[i].Attributes["name"]).Value.ToString();

                    string accountLineCount = ((AliasedValue)accountsResult[i].Attributes["RecC"]).Value.ToString();

                    Console.WriteLine( accountName  + "||" + accountLineCount);


                    
                }
            }

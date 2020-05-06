using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Net;
using System.ServiceModel.Description;


namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientCredentials clientCredentials = new ClientCredentials();
            Uri crmUrl = new Uri("https://sandstratus.crm.dynamics.com/XRMServices/2011/Organization.svc");
            clientCredentials.UserName.UserName = "akhasia@stratus.hr";
            clientCredentials.UserName.Password = "Prompt@123";
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            IOrganizationService service = new OrganizationServiceProxy(crmUrl, null, clientCredentials, null);


            Guid OpportunityId = new Guid("70b472d7-838e-ea11-a811-000d3a4db239");
            QueryExpression queryOpportunity = new QueryExpression()
            {
                EntityName = "opportunity",
                ColumnSet = new ColumnSet(new string[] { "new_pepmrate", "new_adminrate", "new_professionalrate", "new_numberofemployees", "pricelevelid" }),
                Criteria =
                            {
                                Conditions =
                                    {
                                        new ConditionExpression("opportunityid", ConditionOperator.Equal, OpportunityId),
                                    }
                            }
            };

            DataCollection<Entity> opportunityResult = service.RetrieveMultiple(queryOpportunity).Entities;
            if (opportunityResult.Count > 0)
            {
                if (opportunityResult[0].Attributes.Contains("new_pepmrate") && opportunityResult[0].Attributes.Contains("new_adminrate") &&
                    opportunityResult[0].Attributes.Contains("new_professionalrate") && opportunityResult[0].Attributes.Contains("new_numberofemployees") &&
                    opportunityResult[0].Attributes.Contains("pricelevelid"))
                {
                    Money ratePEPM = (Money)opportunityResult[0].Attributes["new_pepmrate"];
                    Money rateAdmin = (Money)opportunityResult[0].Attributes["new_adminrate"];
                    Money rateProfessional = (Money)opportunityResult[0].Attributes["new_professionalrate"];
                    int dataNoOfEmp = (int)opportunityResult[0].Attributes["new_numberofemployees"];
                    EntityReference dataPriceLevelId = (EntityReference)opportunityResult[0].Attributes["pricelevelid"];

                    string SLAPackageId = dataPriceLevelId.Id.ToString();
                    //CreateOpportunityProduct(SLAPackageId, trace, service, OpportunityId, ratePEPM.ToString());
                }
            }
        }
    }
}



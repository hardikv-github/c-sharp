
public static List<Entity> RetrieveAllRecords(IOrganizationService service, string fetch)
        {
            var moreRecords = false;
            int page = 1;
            var cookie = string.Empty;
            List<Entity> Entities = new List<Entity>();
            do
            {
                var xml = string.Format(fetch, cookie);
                var collection = service.RetrieveMultiple(new FetchExpression(xml));

                if (collection.Entities.Count >= 0)
                {
                    Entities.AddRange(collection.Entities);
                }

                moreRecords = collection.MoreRecords;
                if (moreRecords)
                {
                    page++;
                    cookie = string.Format("paging-cookie='{0}' page='{1}'", System.Security.SecurityElement.Escape(collection.PagingCookie), page);
                }
            } while (moreRecords);

            return Entities;
        }
        
        
        string fetch = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false' {0}>
                              <entity name='imm_searchprofile'>
                                <attribute name='imm_searchprofileid' />
                                <attribute name='imm_title' />
                                <attribute name='ownerid' />
                                <order attribute='imm_title' descending='false' />
                              </entity>
                            </fetch>";

            var searchProfileResult = RetrieveAllRecords(service, fetch);
            Console.WriteLine("Search profile retrieved:: " + searchProfileResult.Count.ToString());

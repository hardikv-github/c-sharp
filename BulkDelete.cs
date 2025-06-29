public static void CallBulkDelete(ServiceClient serviceClient)
{
		List<Entity> listAttach = Fetch_GetAttachment(serviceClient);
		Console.WriteLine(listAttach.Count);
		
		List<EntityReference> recordsToDelete = new List<EntityReference>();
		for (int i = 0; i < listAttach.Count; i++)
		{
				recordsToDelete.Add(new EntityReference( listAttach[i].LogicalName, listAttach[i].Id));
		}
		
		BulkDelete(serviceClient, recordsToDelete);
}

public static void BulkDelete(IOrganizationService service, List<EntityReference> recordsToDelete)
{
    // Initialize ExecuteMultipleRequest
    var multipleRequest = new ExecuteMultipleRequest
    {
        Settings = new ExecuteMultipleSettings
        {
            ContinueOnError = false, // Stop on error
            ReturnResponses = true  // Return responses for each request
        },
        Requests = new OrganizationRequestCollection()
    };

    // Add DeleteRequest for each record
    foreach (var entityRef in recordsToDelete)
    {
        var deleteRequest = new DeleteRequest
        {
            Target = entityRef
        };
        multipleRequest.Requests.Add(deleteRequest);

        // Execute in batches of 1,000 to respect the limit
        if (multipleRequest.Requests.Count == 500)
        {
            ExecuteBatch(service, multipleRequest);
            multipleRequest.Requests.Clear(); // Reset for the next batch
        }
    }

    // Execute any remaining requests
    if (multipleRequest.Requests.Count > 0)
    {
        ExecuteBatch(service, multipleRequest);
    }
}

private static void ExecuteBatch(IOrganizationService service, ExecuteMultipleRequest multipleRequest)
{
    try
    {
        var response = (ExecuteMultipleResponse)service.Execute(multipleRequest);
        foreach (var responseItem in response.Responses)
        {
            if (responseItem.Fault != null)
            {
                Console.WriteLine($"Error deleting record at index {responseItem.RequestIndex}: {responseItem.Fault.Message}");
            }
            else
            {
                Console.WriteLine($"Record deleted successfully at index {responseItem.RequestIndex}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error executing batch: {ex.Message}");
    }
}

public static List<Entity> Fetch_GetAttachment(ServiceClient Service)
{
    string fetch = $@"<fetch>
                      <entity name='activitymimeattachment'>
                        <attribute name='activitymimeattachmentid' />
                        <attribute name='activitymimeattachmentidunique' />
                        <attribute name='attachmentid' />
                        <attribute name='filename' />
                        <link-entity name='email' from='activityid' to='objectid'>
                          <filter type='and'>
                              <condition attribute='modifiedon' operator='on-or-before' value='2022-03-30' />
                            </filter>
                        </link-entity>
                      </entity>
                    </fetch>";

    FetchExpression fetchProduct = new FetchExpression(fetch);

    List<Entity> allRecords = new List<Entity>();
    int pageNumber = 1;
    bool moreRecords = true;

    while (moreRecords)
    {
        // Modify FetchXML to include paging attributes
        var fetchDoc = XDocument.Parse(fetch);
        var fetchElement = fetchDoc.Element("fetch");
        if (fetchElement != null)
        {
            fetchElement.SetAttributeValue("page", pageNumber);
            fetchElement.SetAttributeValue("count", 5000);
        }

        var fetchExpression = new FetchExpression(fetchDoc.ToString());

        try
        {
            var result = Service.RetrieveMultiple(fetchExpression);
            allRecords.AddRange(result.Entities);

            // Check if there are more records
            moreRecords = result.MoreRecords;
            pageNumber++;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving records: {ex.Message}");
            throw;
        }
    }
    return allRecords;
    //return Service.RetrieveMultiple(fetchProduct).Entities.ToList();
}

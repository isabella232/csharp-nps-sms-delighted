# Delightful rest client for c#

The process involves 2 steps:
1.Add the person  
2.Add the response(s) for that person via 

Example

##1. Add the person
```csharp
  var client = new Delighted.Api.Client("yourkey");
  var person = await client.AddPerson(new Person
            {
                Email = model.From.Endpoint + "@fakedomain.com",
                Send = false
            });
```

Important things to note:
•You should pass  send=false  to ensure delightful do not send a survey to the person (if you dont want that)
•You should pass the last_sent_at  parameter as the Unix timestamp of the time when you sent the survey that you're adding in the next step. Setting this value will ensure that our survey throttling feature works correctly for responses that you manually add via the API. Ideally, this value should be when you sent the survey and not when you received the response, but if you only have the time you received the survey available you should pass that value here. This is set to the default date when you create a person in the library
•You should not pass properties in this step if you have any. Pass them in the next step so that they are associated with the survey response.


We'll need to the ID from the above response in the next step.


##2. Add the survey response
```csharp
var dic = new Dictionary<string, string>();
dic.Add("survey_origin", "APIWorld");
var result = await dapi.AddResult(new AddResult()
                {
                    Score = score,
                    Comment = comment,
                    PersonId = person.Id.ToString(),
                    Properties = dic,
                });
```
Things to note:
•You should pass the person ID from step 1 as the  person=  parameter (in this case  person=20093316 )
•You must pass a  score , but the  comment  is optional
•You can pass optional properties to be associated with the response. You can use this to filter responses on the dashboard and create trends.

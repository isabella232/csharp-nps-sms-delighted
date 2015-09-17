using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Delighted.Api.DelightedData;
using SMS_NPS_Delighted.Models;

namespace NPSSMSVoting.Controllers
{
    public class SMSController : ApiController
    {
        [Route("SMS")]
        [HttpPost]
        public async Task<HttpResponseMessage> Index(SMSCallbackModel model) {
            var dapi = new Delighted.Api.Client("yourkey");
            
            var person = await dapi.AddPerson(new Person
            {
                Email = model.From.Endpoint + "@sinch.com",
                Send = false
            });
            if (person != null)
            {

                int score;
                if (model.Message.Substring(0, 2) == "10")
                {
                    score = 10;
                }
                else
                {
                    int.TryParse(model.Message.Substring(0, 1), out score);
                }

                string comment = model.Message.Length > 1 ? model.Message.Substring(score==10 ? 2:1) : "";
                if (model.Message != "0" && score == 0)
                    return new HttpResponseMessage(HttpStatusCode.OK);
                var dic = new Dictionary<string, string>();
                dic.Add("survey_origin", "APIWorld");
                await dapi.AddResult(new AddResult()
                {
                    Score = score,
                    Comment = comment,
                    PersonId = person.Id.ToString(),
                    Properties = dic
                });
            }
            
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

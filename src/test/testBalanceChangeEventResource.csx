#load "prelude.csx"
#load "../wwwroot/BalanceChangeEventResource/run.csx"
#r "Newtonsoft.Json"

using System.Collections;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Hosting;
using Newtonsoft.Json;

Current.Guid = Guid.Empty;
Current.Time = new DateTime(2020, 1, 1);

// Arrange
var storage = new List<BalanceChangedEvent>();
var table = storage.AsQueryable();

accountUriTemplate = "http://account/{0}";

var req = new HttpRequestMessage();
req.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
var id = Current.Guid.ToString();

// Action
var res = Run(req, id, table, log).Result;

// Assert
fa.Should(res.StatusCode.ToString()).Be(HttpStatusCode.NotFound.ToString());

//Arrange
storage.Add(new BalanceChangedEvent());

// Action
res = Run(req, id, table, log).Result;

// Assert
fa.Should(res.StatusCode.ToString()).Be(HttpStatusCode.OK.ToString());

var eventResource = JsonConvert.DeserializeObject<BalanceChangeEventResource>(res.Content.ReadAsStringAsync().Result);
fa.Should(eventResource.AccountUri).Be("http://account/123");
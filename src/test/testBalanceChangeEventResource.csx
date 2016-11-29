#load "prelude.csx"
#load "../wwwroot/BalanceChangeEventResource/run.csx"

using System.Collections;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Hosting;

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
storage.Add(new BalanceChangedEvent()
{
    PartitionKey = "test",
    RowKey = Current.Guid.ToString()
});

// Action
res = Run(req, id, table, log).Result;

// Assert
fa.Should(res.StatusCode.ToString()).Be(HttpStatusCode.OK.ToString());
fa.Should(res.Content.ReadAsStringAsync().Result)
    .Be("{\"AccountUri\":\"http://account/123\",\"Event\":{\"Timestamp\":\"2020-01-01T00:00:00+00:00\",\"AccountId\":\"123\",\"Amount\":3.0,\"PartitionKey\":\"test\",\"RowKey\":\"00000000-0000-0000-0000-000000000000\",\"ETag\":null}}");
#load "../wwwroot/FakeEventStream/run.csx"

using Microsoft.Azure.WebJobs.Extensions;
using Microsoft.Azure.WebJobs.Host;
using System.Web.Http;
using System.Web.Http.Hosting;

var context = Require<AzureFunctionsPackContext>();
var fa = Require<FluentAssertionsPackContext>();
TraceWriter log = new TraceMonitor();

Current.Time = new DateTime(2020, 1, 1);
Current.Guid = Guid.Empty;

var req = new HttpRequestMessage();
req.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());

var res = Run(req, log).Result;

fa
    .Should(res.Content.ReadAsStringAsync().Result)
    .Be("{{\"Events\":[{{\"When\":\"2020-01-01T00:00:00\",\"Id\":\"00000000-0000-0000-0000-000000000000\"}}]}}");

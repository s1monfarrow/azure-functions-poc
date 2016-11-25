#load "../wwwroot/HttpGet/run.csx"

using Microsoft.Azure.WebJobs.Extensions;
using Microsoft.Azure.WebJobs.Host;

var context = Require<AzureFunctionsPackContext>();
var moq = Require<MoqPackContext>();
var fa = Require<FluentAssertionsPackContext>();

// Arrange
TraceWriter log = new TraceMonitor();

var mocked = moq.Mock<IMyServiceClass>();

locator.AddService(typeof(IMyServiceClass), mocked.Object);
locator.AddService(typeof(MyStatusClass), new MyStatusClass());

var req = new HttpRequestMessage() { Content = new StringContent("Hello World") };
// Action
var res = Run(req, log).Result;

// Assert
fa.Should(locator.GetStatus().Status).Be("Processed");
fa.Should(res.Content.ReadAsStringAsync().Result).Be("Hello World");

#load "prelude.csx"
#load "../wwwroot/StagingEventHandler/run.csx"

// Arrange
var storage = new List<StagedDetailedBusinessEvent>();
var table = MocCollector<StagedDetailedBusinessEvent>(storage);

var detailed = new DetailedBusinessEvent
{

};

Current.Guid = Guid.Empty;

var eve = JsonConvert.SerializeObject(detailed);

// Action
Run(eve, table, log);

// Assert
fa.Should(storage.Count()).Be(1);
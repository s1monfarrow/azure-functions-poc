#load "prelude.csx"
#load "../wwwroot/BalanceChangeEventHandler/run.csx"

// Arrange
DetailedBusinessEvent message;

var eve = "{\"EventUri\":\"http://event/abc\"}";

http = new FakeHttp<BalanceChangeEventResource>(new BalanceChangeEventResource());

// Action
Run(eve, out message, log);

// Assert
fa.Should(message.SourceEventUri).Be("http://event/abc");
#load "prelude.csx"
#load "../wwwroot/FakeEventHandler/run.csx"

// Arrange
DetailedBusinessEvent message;

var eve = "{\"EventUri\":\"http://event/abc\"}";

// Action
Run(eve, out message, log);

// Assert
fa.Should(message.SourceEventUri).Be("http://event/abc");
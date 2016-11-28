#load "testStagingEventHandler.csx"



[TestFixture]
public class Tests
{
    [Test]
    public void ScriptcsShouldBeAwesome()
    {
        var scriptcsIsAwesome = true;
        Assert.IsTrue(scriptcsIsAwesome);
    }
}

var nunit = Require<NUnitRunner>();
nunit.RunAllUnitTests();
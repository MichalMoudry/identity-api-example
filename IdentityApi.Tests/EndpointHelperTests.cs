namespace IdentityApi.Tests;

using IdentityApi.Helpers;

/// <summary>
/// Class containing methods for testing <seealso cref="EndpointHelper" />.
/// </summary>
public class EndpointHelperTests
{
    private readonly EndpointHelper _endpointHelper;

    public EndpointHelperTests() => _endpointHelper = new EndpointHelper();

    /// <summary>
    /// Test method for testing CreateErrorMessage() method.
    /// </summary>
    [Fact]
    public void TestCreateErrorMessage()
    {
        var errors = new string[]
        {
            "Test",
            "",
            "Test message"
        };
        _ = _endpointHelper.CreateErrorMessage(errors).Should().Be("Test\n\nTest message");
    }
}
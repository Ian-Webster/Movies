using System.Threading;

namespace Movies.API.Tests.Shared;

public class ApiBase
{
    protected CancellationToken GetCancellationToken() {
        return new CancellationToken();
    }
}
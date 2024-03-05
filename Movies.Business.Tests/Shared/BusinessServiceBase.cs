using System.Threading;

namespace Movies.Business.Tests.Shared;

public class BusinessServiceBase
{
    protected CancellationToken GetCancellationToken() {
        return new CancellationToken();
    }
}
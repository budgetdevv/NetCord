using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace NetCord;

internal static partial class ThrowUtils // Taken from: https://github.com/CommunityToolkit/dotnet/blob/main/src/CommunityToolkit.Diagnostics/ThrowHelper.cs
{
    [DoesNotReturn]
    public static void Throw<TException>() where TException: Exception, new()
    {
        throw new TException();
    }
        
    [DoesNotReturn]
    public static TRet Throw<TException, TRet>() where TException: Exception, new()
    {
        throw new TException();
    }
}

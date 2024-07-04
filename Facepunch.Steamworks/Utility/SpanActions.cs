using System;
using System.Security.Cryptography;

namespace Steamworks;

//TODO:: might remove, ended up defining a custem action in the place i needed it, as it allows for better documentation
public static class SpanActions {
    public delegate void SpanAction<in T1,in T2, in T3>(Span<byte> arg1, T2 arg2, T3 arg3);
    public delegate void SpanAction<in T1,in T2, in T3,in T4>(Span<byte> arg1, T2 arg2, T3 arg3, T4 arg4);
    public delegate void SpanAction<in T1,in T2, in T3,in T4,in T5>(Span<byte> arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    public delegate void Action<in T1,in T2, in T3,in T4,in T5, in T6>(Span<byte> arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);


    public delegate void ReadonlySpanAction<in T1,in T2, in T3>(ReadOnlySpan<byte> arg1, T2 arg2, T3 arg3);
    public delegate void ReadonlySpanAction<in T1,in T2, in T3,in T4>(ReadOnlySpan<byte> arg1, T2 arg2, T3 arg3, T4 arg4);
    public delegate void ReadonlySpanAction<in T1,in T2, in T3,in T4,in T5>(ReadOnlySpan<byte> arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    public delegate void ReadonlySpanAction<in T1,in T2, in T3,in T4,in T5, in T6>(ReadOnlySpan<byte> arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
}

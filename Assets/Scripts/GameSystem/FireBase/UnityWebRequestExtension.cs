using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace FireBase
{
    public static class UnityWebRequestExtension
    {
        public static TaskAwaiter GetAwaiter(this UnityWebRequestAsyncOperation operation)
        {
            var tcs = new TaskCompletionSource<object>();
            operation.completed += asyncOp => tcs.SetResult(null);
            return ((Task)tcs.Task).GetAwaiter();
        }
    }
}
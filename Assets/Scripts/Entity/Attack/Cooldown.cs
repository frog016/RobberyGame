using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Entity.Attack
{
    public class Cooldown
    {
        public bool IsReady { get; private set; }
        public float Time { get; set; }

        private readonly CancellationToken _token;

        public Cooldown(float time, CancellationToken token)
        {
            IsReady = true;
            Time = time;

            _token = token;
        }

        public void Restart()
        {
            if (IsReady == false)
                throw new InvalidOperationException("Cooldown already restarted.");

            RestartAsync().Forget();
        }

        private async UniTaskVoid RestartAsync()
        {
            IsReady = false;
            await UniTask.WaitForSeconds(Time, cancellationToken: _token);
            IsReady = true;
        }
    }
}
using Cysharp.Threading.Tasks;

namespace Structure.Scene
{
    public interface ISceneLoader
    {
        UniTask LoadAsync(string sceneName);
    }
}
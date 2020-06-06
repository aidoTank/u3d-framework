/***
 * ISceneState.cs
 * 
 * @author administrator
 */
namespace GameEngine
{
    public abstract class ISceneState
    {
        public virtual void OnAwake() { }

        public virtual void OnStart() { }

        public virtual void OnUpdate() { }

        public abstract void OnDestroy();
    }
}


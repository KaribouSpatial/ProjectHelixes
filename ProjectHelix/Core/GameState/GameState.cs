using Microsoft.Xna.Framework;

namespace ProjectHelix.Core.GameState
{
    public abstract class GameState
    {
        public static GameState Instance;

        public float ElapsedTime { get; set; }

        public abstract void Update(GameTime time);
        public abstract void ProcessMousePressed();
        public abstract void ProcessMouseRelease();
        public abstract void ProcessMouseDrag();
        public abstract void ProcessMouseMove();
    }
}

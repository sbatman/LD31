using System;

namespace LD31.Objects
{
    /// <summary>
    /// This is the base type all objects in the game derive from.
    /// </summary>
    public abstract class GameObject : IDisposable
    {
        public GameObject()
        {
            Game._GameObjects.Add(this);
        }

        public abstract void Update();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Game._GameObjects.Remove(this);
        }
    }
}

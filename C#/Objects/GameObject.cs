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

        public abstract void Update(Double msSinceLastUpdate);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Game._GameObjects.Remove(this);
        }

        /// <summary>
        /// allow all gameobject types to implement their own draw calls.
        /// </summary>
        public virtual void Draw()
        {

        }

        /// <summary>
        /// This boolean states if the gameobject is alive or not.
        /// </summary>
        public virtual Boolean Alive
        {
            get { return true; }
        }
    }
}

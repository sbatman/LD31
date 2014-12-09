using System;

namespace LD31.Objects
{
    /// <summary>
    ///     This is the base type all objects in the game derive from.
    /// </summary>
    public abstract class GameObject : IDisposable
    {
        private bool _Disposed;

        /// <summary>
        ///     Ctor of the gameobject object. Adds the new instance to the global game objects list.
        /// </summary>
        protected GameObject()
        {
            Game.GameObjects.Add(this);
        }

        /// <summary>
        ///     This boolean states if the gameobject is alive or not.
        /// </summary>
        public virtual Boolean Disposed
        {
            get { return _Disposed; }
            protected set { _Disposed = value; }
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            _Disposed = true;
        }

        /// <summary>
        ///     allow all gameobject types to implement their own update calls.
        /// </summary>
        public virtual void Update(Double msSinceLastUpdate)
        {
        }

        /// <summary>
        ///     allow all gameobject types to implement their own draw calls.
        /// </summary>
        public virtual void Draw()
        {
        }
    }
}
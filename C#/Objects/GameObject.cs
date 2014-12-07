using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace LD31.Objects
{
    /// <summary>
    /// This is the base type all objects in the game derive from.
    /// </summary>
    public abstract class GameObject : IDisposable
    {
        /// <summary>
        /// Ctor of the gameobject object. Adds the new instance to the global game objects list.
        /// </summary>
        public GameObject()
        {
            Game._GameObjects.Add(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Game._GameObjects.Remove(this);
        }

        /// <summary>
        /// allow all gameobject types to implement their own update calls.
        /// </summary>
        public virtual void Update(Double msSinceLastUpdate)
        {

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

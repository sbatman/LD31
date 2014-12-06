﻿using LD31.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD31.Objects
{
    /// <summary>
    /// This class represents the player
    /// </summary>
    public class Player : Combatant
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public Player(Vector2 position)
            : base(position)
        {
        }
    }
}

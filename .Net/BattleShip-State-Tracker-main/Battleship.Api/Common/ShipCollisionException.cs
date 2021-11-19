using System;
using System.Collections.Generic;

namespace Battleship.Api.Common
{
    public class ShipCollisionException : Exception
    {
        public ShipCollisionException(IList<string> collisions)
            : base($"Collision detected at {string.Join(" ", collisions)} when adding new ship.")
        {
        }
    }
}

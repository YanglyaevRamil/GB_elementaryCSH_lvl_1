using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    class GameObjectException : ArgumentException
    {
        public string Name { get; }
        public GameObjectException(string message, string name) : base(message)
        {
            Name = name; 
        }
    }
}

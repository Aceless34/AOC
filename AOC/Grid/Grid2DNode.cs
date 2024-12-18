using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Grid
{
    public class Grid2DNode<T>
    {
        public Grid2DNode(Vector2 vector2, T? t)
        {
            Position = vector2;
            this.Value = t;
        }

        public Vector2 Position { get; }
        public T? Value { get; set; }
        private Dictionary<string, object> _properties { get; }

        public object this[string attr]
        {
            get => _properties[attr];
            set => _properties[attr] = value;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rpg.Interfaces
{
    public interface IMovable
    {
        float Speed {get;}

        float MovingSpeed { get; }

        void Update();

    }
}

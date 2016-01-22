using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Interfaces
{
    public interface IBot : IPlayer
    {
        public string Name { get; set; }

        void AI();

        void Check();

        void Fold();

        void Call();

        void Raised();
    }
}

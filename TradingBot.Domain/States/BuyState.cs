using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingBot.Domain.Interfaces.Account;

namespace TradingBot.Domain.States
{
    internal class BuyState : State
    {
        public BuyState(State state) 
        {
            
        }

        public override void MakeTrade()
        {
            throw new NotImplementedException();
        }

        private void StateChangeCheck()
        {

        }
    }
}

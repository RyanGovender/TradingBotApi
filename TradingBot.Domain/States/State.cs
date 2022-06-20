using TradingBot.Domain.Interfaces.Account;

namespace TradingBot.Domain.States
{
    internal abstract class State
    {
        protected IAccount account;

        //Sell thresholds(if in buy state)
        protected const double PROFIT_THRESHOLD = 1.25;
        protected const double STOP_LOSS_THRESHOLD = -2.0;

        //Buy thresholds(if the bot is in Sell state)
        protected const double DIP_THRESHOLD = - 2.25;
        protected const double UPWARD_TREND_THRESHOLD = 1.5;

        public IAccount Account
        {
            get { return account; }
            set { account = value; }
        }

        public State(IAccount account)
        {
            Account = account;
        }

        public abstract void MakeTrade();
    }
}

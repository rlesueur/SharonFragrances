using System;
using System.Collections.Generic;

namespace SharonFragrances.API
{
    public class StackFeeder : iStackFeeder
    {
        private int _stackPosition;
        private int _binPosition;
        protected readonly IList<IStack> _stacks;

        public StackFeeder(IList<IStack> stacks, string stock)
        {
            _stacks = stacks;
            PopulateStock(stock);
        }

        private void PopulateStock(string stock)
        {
            foreach (var stockItem in stock.Split(';'))
            {
                PlaceStockItem(stockItem);
            }
        }

        private void PlaceStockItem(string stockItem)
        {
            if (!string.IsNullOrEmpty(stockItem))
            {
                var stockData = stockItem.Split(':');
                _stacks[int.Parse(stockData[0])].Bins[int.Parse(stockData[1])].ContentID = stockData[2];
            }
        }

        public string Command(string command)
        {
            string result = String.Empty;

            foreach (var c in command)
            {
                if (c == 'r')
                {
                    _stackPosition = ++_stackPosition;
                }
                if (c == 's')
                {
                    result = _stackPosition.ToString();
                }
                if (c == 'q')
                {
                    _stackPosition = 0;
                }
                if (c == 'b')
                {
                    result = _binPosition.ToString();
                }
                if (c == 'd')
                {
                    if (_stackPosition != 0)
                    {
                        _binPosition = ++_binPosition;
                    }
                    else
                    {
                        throw new FeederNotOnStack();
                    }
                }
                if (c == 'u')
                {
                    if (_stackPosition != 0)
                    {
                        _binPosition = --_binPosition;
                    }
                    else
                    {
                        throw new FeederNotOnStack();
                    }
                }
                if (c == 'i')
                {
                    result = _stacks[_stackPosition].Bins[_binPosition].ContentID;
                }
            }
            return result;
        }
    }
}
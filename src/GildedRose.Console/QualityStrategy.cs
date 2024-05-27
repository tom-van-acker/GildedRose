using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public interface IQualityStrategy
    {
        IQualityStrategy UpdateQuality(Item item);
        IQualityStrategy UpdateSellIn(Item item);

    }


    public class DefaultQualityStrategy : IQualityStrategy
    {
        public static void CheckBounds(Item item,int lowest, int highest)
        {
            if (item.Quality < lowest)
            {
                item.Quality = lowest;
            }
            if (item.Quality > highest)
            {
                item.Quality = highest;
            }
        }

        public static int CalcDecrease(int sellIn)
        {
            if (sellIn <= 0)
            {
                return 2;
            }
            return 1;
        }

        public IQualityStrategy UpdateQuality(Item item)
        {
            item.Quality -= CalcDecrease(item.SellIn);
            CheckBounds(item, 0, 50);
            return this;
        }

        public IQualityStrategy UpdateSellIn(Item item)
        {
            item.SellIn--;
            return this;
        }
    }

    public class AgedBrieQualityStrategy : IQualityStrategy
    {
        public IQualityStrategy UpdateQuality(Item item)
        {
            item.Quality++;
            DefaultQualityStrategy.CheckBounds(item, 0, 50);
            return this;
        }

        public IQualityStrategy UpdateSellIn(Item item)
        {
            item.SellIn--;
            return this;
        }
    }

    public class SulfurasQualityStrategy : IQualityStrategy
    {

        public SulfurasQualityStrategy()
        {
        }
        public IQualityStrategy UpdateQuality(Item item)
        {
            // Sulfras never decreases in quality, and should never be sold
            item.Quality = 80;
            return this;
        }
        public IQualityStrategy UpdateSellIn(Item item)
        {
            // Sulfras should never be sold
            return this;
        }
    }

    public class BackstagePassQualityStrategy : IQualityStrategy
    {
        /// <summary>
        /// This strategy causes an increase in Quality as it's SellIn value approaches; 
        /// Quality increases by 2 when there are 10 days or less and by 3 when there 
        /// are 5 days or less but Quality drops to 0 after the concert
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="NotImplementedException"></exception>
        public IQualityStrategy UpdateQuality(Item item)
        {
            if (item.SellIn <= 0)
            {
                item.Quality = 0;
            }
            if (item.SellIn >0 && item.SellIn <= 5)
            {
                item.Quality += 3;
            }
            if (item.SellIn > 5 && item.SellIn <= 10)
            {
                item.Quality += 2;
            }
            if (item.SellIn > 10)
                item.Quality++;

            DefaultQualityStrategy.CheckBounds(item, 0, 50);
            return this;
        }

        public IQualityStrategy UpdateSellIn(Item item)
        {
            item.SellIn--;
            return this;
        }
    }

    public class ConjuredQualityStrategy : IQualityStrategy
    {
        /// <summary>
        /// "Conjured" items degrade in Quality twice as fast as normal items
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="NotImplementedException"></exception>
        public IQualityStrategy UpdateQuality(Item item)
        {
            item.Quality -= 2*DefaultQualityStrategy.CalcDecrease(item.SellIn);
            DefaultQualityStrategy.CheckBounds(item, 0, 50);
            return this;
        }

        public IQualityStrategy UpdateSellIn(Item item)
        {
            item.SellIn--;
            return this;
        }
    }

}

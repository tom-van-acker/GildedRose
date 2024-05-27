using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public static class QualityStrategyLocator
    {
        public static IQualityStrategy GetQualityStrategy(Item item)
        {
            if (item.Name == "Aged Brie")
            {
                return new AgedBrieQualityStrategy();
            }
            if (item.Name == "Sulfuras, Hand of Ragnaros")
            {
                return new SulfurasQualityStrategy();
            }
            if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                return new BackstagePassQualityStrategy();
            }
            if (item.Name.StartsWith("Conjured"))
            {
                return new ConjuredQualityStrategy();
            }
            return new DefaultQualityStrategy();
        }
    }
}

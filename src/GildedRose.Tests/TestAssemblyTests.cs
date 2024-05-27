using GildedRose.Console;
using Xunit;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        [Fact]
        public void TestTheTruth()
        {
            Assert.True(true);
        }

        [Fact]
        public void TestQualityStrategyLocator()
        {
            // We have different strategies for decreasing quality, first we need to get the right strategy

            var item = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };
            var strategy = QualityStrategyLocator.GetQualityStrategy(item);
            Assert.IsType<DefaultQualityStrategy>(strategy);

            item = new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 };
            strategy = QualityStrategyLocator.GetQualityStrategy(item);
            Assert.IsType<AgedBrieQualityStrategy>(strategy);

            item = new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 };
            strategy = QualityStrategyLocator.GetQualityStrategy(item);
            Assert.IsType<DefaultQualityStrategy>(strategy);

            item = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 };
            strategy = QualityStrategyLocator.GetQualityStrategy(item);
            Assert.IsType<SulfurasQualityStrategy>(strategy);

            item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 };
            strategy = QualityStrategyLocator.GetQualityStrategy(item);
            Assert.IsType<BackstagePassQualityStrategy>(strategy);

            item = new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 };
            strategy = QualityStrategyLocator.GetQualityStrategy(item);
            Assert.IsType<ConjuredQualityStrategy>(strategy);

        }

        [Fact]
        public void TestDefaultQualityStrategy()
        { 
            // The default strategy is to decrease quality by 1
            var item = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };
            var strategy = new DefaultQualityStrategy();
            strategy.UpdateQuality(item);
            Assert.Equal(19, item.Quality);

            // Quality should never be negative
            item = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 0 };
            strategy.UpdateQuality(item);
            Assert.Equal(0, item.Quality);

            // Quality should decrease by 2 after sell by date
            item = new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20 };
            strategy.UpdateQuality(item);
            Assert.Equal(18, item.Quality);

            // But never be negative
            strategy = new DefaultQualityStrategy();
            item = new Item { Name = "+5 Dexterity Vest", SellIn = 0, Quality = 1 };
            strategy.UpdateQuality(item);
            Assert.Equal(0, item.Quality);


        }
     }
}
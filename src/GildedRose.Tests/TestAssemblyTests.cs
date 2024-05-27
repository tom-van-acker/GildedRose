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
            // The default strategy is to decrease quality everytime by 1
            var item = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };
            var strategy = new DefaultQualityStrategy();
            strategy.UpdateQuality(item);
            Assert.Equal(19, item.Quality);
            strategy.UpdateQuality(item);
            Assert.Equal(18, item.Quality);

            // Quality should never be negative
            item = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 0 };
            strategy.UpdateQuality(item);
            Assert.Equal(0, item.Quality);

            // Quality should never increase above 50
            item = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 60 };
            strategy.UpdateQuality(item);
            Assert.Equal(50, item.Quality);

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

        [Fact]
        public void TestAgedBrieQualityStrategy()
        {
            // Aged Brie increases in quality as it gets older
            var item = new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 };
            var strategy = new AgedBrieQualityStrategy();
            strategy.UpdateQuality(item);
            Assert.Equal(1, item.Quality);
            strategy.UpdateQuality(item);
            Assert.Equal(2, item.Quality);

            // Quality should never increase above 50
            item = new Item { Name = "Aged Brie", SellIn = 2, Quality = 50 };
            strategy.UpdateQuality(item);
            Assert.Equal(50, item.Quality);

        }

        [Fact]
        public void TestSulfurasQualityStrategy()
        {
            // Sulfuras never decreases in quality
            var item = new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 0 };
            var strategy = new SulfurasQualityStrategy();
            strategy.UpdateQuality(item);
            Assert.Equal(80, item.Quality);
        }

        [Fact]  
        public void TestBackstagePassQualityStrategy()
        {
            // Backstage passes increase in quality as the concert approaches
            var item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 };
            var strategy = new BackstagePassQualityStrategy();
            strategy.UpdateQuality(item);
            Assert.Equal(21, item.Quality);

            // Quality should increase by 2 when there are 10 days or less
            item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 20 };
            strategy.UpdateQuality(item);
            Assert.Equal(22, item.Quality);
            strategy.UpdateQuality(item);
            Assert.Equal(24, item.Quality);

            // Quality should increase by 3 when there are 5 days or less
            item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 20 };
            strategy.UpdateQuality(item);
            Assert.Equal(23, item.Quality);
            strategy.UpdateQuality(item);
            Assert.Equal(26, item.Quality);

            // Quality should never increase above 50
            item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 4, Quality = 48 };
            strategy.UpdateQuality(item);
            Assert.Equal(50, item.Quality);
            strategy.UpdateQuality(item);
            Assert.Equal(50, item.Quality);

            // Quality should drop to 0 after the concert
            item = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 20 };
            strategy.UpdateQuality(item);
            Assert.Equal(0, item.Quality);
        }

        [Fact]
        public void TestConjuredQualityStrategy()
        {
            // Conjured items degrade in Quality twice as fast as normal items
            var item = new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 };
            var strategy = new ConjuredQualityStrategy();
            strategy.UpdateQuality(item);
            Assert.Equal(4, item.Quality);
            strategy.UpdateQuality(item);
            Assert.Equal(2, item.Quality);

            // Quality should never be negative
            item = new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 1 };
            strategy.UpdateQuality(item);
            Assert.Equal(0, item.Quality);

            // Quality should decrease by 4 after sell by date
            item = new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 20 };
            strategy.UpdateQuality(item);
            Assert.Equal(16, item.Quality);
            strategy.UpdateQuality(item);
            Assert.Equal(12, item.Quality);

            // But never be negative
            strategy = new ConjuredQualityStrategy();
            item = new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 1 };
            strategy.UpdateQuality(item);
            Assert.Equal(0, item.Quality);
        }

     }
}
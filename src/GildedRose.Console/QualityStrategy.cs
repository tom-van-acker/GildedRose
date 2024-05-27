using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public interface IQualityStrategy
    {
        void UpdateQuality(Item item); 

    }


    public class DefaultQualityStrategy : IQualityStrategy
    {
        public void UpdateQuality(Item item)
        {
            throw new NotImplementedException();
        }
    }

    public class AgedBrieQualityStrategy : IQualityStrategy
    {
        public void UpdateQuality(Item item)
        {
            throw new NotImplementedException();
        }
    }

    public class SulfurasQualityStrategy : IQualityStrategy
    {
        public void UpdateQuality(Item item)
        {
            throw new NotImplementedException();
        }
    }

    public class BackstagePassQualityStrategy : IQualityStrategy
    {
        public void UpdateQuality(Item item)
        {
            throw new NotImplementedException();
        }
    }

    public class ConjuredQualityStrategy : IQualityStrategy
    {
        public void UpdateQuality(Item item)
        {
            throw new NotImplementedException();
        }
    }

}

using Jh.Abp.Common;
using Jh.Abp.Common.Entity;
using Jh.Abp.Common.Linq;
using System;
using Xunit;

namespace Jh.Abp.QuickComponents.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var t1 = new TradeLeadUpdateInputDto() { ClickNum = 1, DateTime=DateTime.Now, Num=1,ClickNum2=2 };
            var t2 = new TradeLead() { Num = 2 };
            //var t= LinqExpression.ConvetToExpression<TradeLeadUpdateInputDto, TradeLead>(t1);
            EntityOperator.UpdatePortionToEntity(t1,t2);
            Assert.True(t2.ClickNum2 > 0);
        }

       
    }

    public class Test
    { 
        public decimal num { get; set; }
        public int num2 { get; set; }
    }
}

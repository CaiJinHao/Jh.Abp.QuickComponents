using System;
namespace Jh.Abp.QuickComponents.Tests
{
    public class TradeLeadUpdateInputDto
	{
		/// <summary>
		/// 货物数量
		/// <summary>
		public Decimal? Num { get; set; }
		
		/// <summary>
		/// 点击量
		/// <summary>
		public Int64? ClickNum { get; set; }
		public Int32? ClickNum2 { get; set; }
		public DateTime? DateTime { get; set; }
	}
}

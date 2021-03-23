using Jh.SourceGenerator.Common.GeneratorAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jh.Abp.QuickComponents.Tests
{
    [GeneratorClass]
    [Description("供求信息")]
    public class TradeLead
    {
        public TradeLead() { }


        [CreateOrUpdateInputDto]
        [Description("货物数量")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Num { get; set; }


        [CreateOrUpdateInputDto]
        [Description("点击量")]
        public long? ClickNum { get; set; }

		public Int32? ClickNum2 { get; set; }
        public DateTime? DateTime { get; set; }

    }
}

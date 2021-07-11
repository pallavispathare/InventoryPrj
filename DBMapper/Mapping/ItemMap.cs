using DBMapper.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMapper.Mapping
{
    public class ItemMap:EntityTypeConfiguration<Item>
    {
        public ItemMap()
        {
            HasKey(i => i.ID);
            Property(i => i.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(i => i.ItemName).IsRequired();
            Property(i => i.ItemDescription).IsRequired();
            Property(i => i.ItemPrice).HasPrecision(18, 2).IsRequired();
            ToTable("tb_mstItemMaster");
        }
    }
}

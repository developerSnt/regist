using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace regist
{
    public class FAppData : DbContext
    {
        public FAppData(DbContextOptions<FAppData> options) : base(options)
        {
        }
        public DbSet<DataInfo> DataInfos { get; set; }
    }
}

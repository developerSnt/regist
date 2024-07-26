using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace regist
{
    public class AppData : DbContext
    {
        public AppData(DbContextOptions<AppData> options) : base(options)
        {
        }
        public DbSet<Regis> Regist { get; set; }
        public DbSet<DataInfo> DataInfos { get; set; }
    }
}

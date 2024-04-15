﻿using Microsoft.EntityFrameworkCore;

namespace Logging
{
    public class AppDbContext: DbContext
    {
        public DbSet<SettingModel> SettingModels { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }
    }
}

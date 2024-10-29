﻿using Microsoft.EntityFrameworkCore;

namespace Backend.Models {

    public class StoreContext: DbContext {
    
        public StoreContext (DbContextOptions<StoreContext> options)
            : base(options) { 
        }

        public DbSet<Juice> Juices { get; set; }

        public DbSet<Brand> Brands { get; set; }

    }

}

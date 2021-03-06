﻿using System;
using DataAccess.Base.Infrastructure;
using DataAccess.Base.Interfaces;
using DataAccess.Base.Repositories;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class ActivityLogRepository : BaseEntityRepository<ActivityLog, ApplicationDbContext>, IActivityLogRepository
    {
        public ActivityLogRepository(IDatabaseFactory<ApplicationDbContext> databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}

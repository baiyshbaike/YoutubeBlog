﻿using Blog.Data.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstractioins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Services.Contrete
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<int>> GetYearlyArtileCount()
        {
            var articles = await unitOfWork.GetRepository<Article>().GetAllAsync(x => !x.IsDeleted);
            var startDate = DateTime.Now.Date;
            startDate = new DateTime(startDate.Year,1,1);
            List<int> datas = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                var startedDate = new DateTime(startDate.Year,i,1);
                var endedDate = startDate.AddMonths(1);
                var data = articles.Where(x=>x.CreatedDate>=startedDate && x.CreatedDate<=endedDate).Count();
                datas.Add(data);
            }
            return datas;
        }
        public async Task<int> GetTotalArticleCount()
        {
            var articleCount = await unitOfWork.GetRepository<Article>().CountAsync();
            return articleCount;
        }
        public async Task<int> GetTotalCategoryCount()
        {
            var articleCount = await unitOfWork.GetRepository<Category>().CountAsync();
            return articleCount;
        }
    }
}

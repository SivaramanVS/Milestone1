using BusinessService.Data.DBModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessService.Data.Repository
{
    public class SchoolsRepository : ISchoolsRepository
    {
        private readonly DefaultContext _context;
        private readonly IDistributedCache _distributedCache;
        private readonly Settings _settings;

        public SchoolsRepository(DefaultContext context, IConfiguration configuration, IDistributedCache distributedCache)
        {
            _context = context;
            _distributedCache = distributedCache;
            _settings = new Settings(configuration);
        }

        public async Task<School> GetSchoolsAsync(int schoolsId)
        {
            var jsonData = await _distributedCache.GetStringAsync("GetSchoolById");
            if (jsonData != null)
            {
                var tModel = JsonConvert.DeserializeObject<School>(jsonData);
                var schools = tModel;
                return schools;
            }
            else
            {
                var schools = await _context.Schools.Where(p => p.Id == schoolsId).FirstOrDefaultAsync();
                jsonData = JsonConvert.SerializeObject(schools);
                DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(_settings.PricesExpirationPeriod));
                await _distributedCache.SetStringAsync("GetSchoolById", jsonData, cacheOptions);
                return schools;
            }

        }

        public async Task<School> AddSchoolsAsync(School schoolsId)
        {
            if (schoolsId != null)
            {
                await _context.Schools.AddAsync(schoolsId);

                await _context.SaveChangesAsync();
            }

            return schoolsId;
        }

        public async Task<School> DeleteSchoolsAsync(int schoolsId)
        {
            var school = await GetSchoolsAsync(schoolsId);

            if (school != null)
            {
                _context.Schools.Remove(school);

                await _context.SaveChangesAsync();
            }

            return school;
        }

        public async Task<IEnumerable<School>> FindSchoolsAsync(string schoolName)
        {
            return await _context.Schools.Where(p => p.Name.Contains(schoolName)).ToListAsync();
        }

        public async Task<IEnumerable<School>> GetAllSchoolsAsync()
        {
            var jsonData = await _distributedCache.GetStringAsync("GetAllSchools");
            if (jsonData != null)
            {
                var tModel = JsonConvert.DeserializeObject<IEnumerable<School>>(jsonData);
                var schools = tModel;
                return schools;
            }
            else
            {
                var schools = await _context.Schools.ToListAsync();
                jsonData = JsonConvert.SerializeObject(schools);
                DistributedCacheEntryOptions cacheOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(_settings.PricesExpirationPeriod));
                await _distributedCache.SetStringAsync("GetAllSchools", jsonData, cacheOptions);
                return schools;
            }

        }


        public async Task<School> UpdateSchoolsAsync(int schoolsId, School schools)
        {
            var schId = await _context.Schools.FindAsync(schoolsId);
            if (schId != null)
            {
                schId.Name = schools.Name;

                await _context.SaveChangesAsync();
            }

            return schools;
        }

        class Settings
        {
            public int PricesExpirationPeriod = 1;       //15 minutes by default

            public Settings(IConfiguration configuration)
            {
                int pricesExpirationPeriod;
                if (Int32.TryParse(configuration["Caching:PricesExpirationPeriod"], NumberStyles.Any,
                    NumberFormatInfo.InvariantInfo, out pricesExpirationPeriod))
                {
                    PricesExpirationPeriod = pricesExpirationPeriod;
                }
            }
        }
    }
}
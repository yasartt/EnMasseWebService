using EnMasseWebService.Models.Entities;
using EnMasseWebService.Models.DTOs;
using EnMasseWebService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace EnMasseWebService.Services
{

    public class DailyService
    {
        private readonly EnteractDbContext _enteractDbContext;
        private readonly IMongoCollection<ImageDTO> _imagesCollection;

        public DailyService(EnteractDbContext enteractDbContext, IMongoCollection<ImageDTO> imagesCollection)
        {
            _enteractDbContext = enteractDbContext;
            _imagesCollection = imagesCollection;
        }


        public async Task<Daily> AddNewDailyAsync(DailyDTO dailyDTO)
        {
            try
            {
                var newDaily = new Daily()
                {
                    //DailyTypeId = dailyDTO.DailyTypeId,
                    UserId = dailyDTO.UserId,
                    Caption = dailyDTO.Caption,
                    Created = dailyDTO.Created,
                };

                await _enteractDbContext.Dailies.AddAsync(newDaily);
                await _enteractDbContext.SaveChangesAsync();

                return newDaily;
            }
            catch
            {
                return null;
            }
            
        }

        public async Task<List<ImageDTO>> UploadImagesAsync(List<ImageIncomingDTO> imageIncomingDTOs, Guid dailyId)
        {
            try
            {
                List<ImageDTO> uploadedImages = new List<ImageDTO>();

                foreach (var imageIncomingDTO in imageIncomingDTOs)
                {
                    var imageDTO = new ImageDTO
                    {
                        ImageName = imageIncomingDTO.ImageName,
                        DailyId = dailyId,
                    };

                    await _imagesCollection.InsertOneAsync(imageDTO);
                    uploadedImages.Add(imageDTO);

                   
                    await _enteractDbContext.SaveChangesAsync();
                }

                return uploadedImages;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ImageDTO>> GetImagesByDailyIdAsync(Guid dailyId)
        {

            var filter = Builders<ImageDTO>.Filter.Eq(x => x.DailyId, dailyId);

            var images = await _imagesCollection.FindAsync(filter);

            return await images.ToListAsync();
        }

        public async Task<List<DailyView>> GetContactDailiesByUserIdAsync(Guid userId, DateTime? lastTime, Guid? lastDailyId)
        {
            var limitTime = DateTime.Now.AddDays(-7);

            // Adjusting the query to account for lastDailyId being null.
            var contactDailiesQuery = from userContact in _enteractDbContext.UserContacts
                                      from daily in _enteractDbContext.Dailies
                                      from user in _enteractDbContext.Users
                                      where ((userContact.User1Id == userId && daily.UserId == userContact.User2Id && user.UserId == userContact.User2Id)
                                         || (userContact.User2Id == userId && daily.UserId == userContact.User1Id && user.UserId == userContact.User1Id))
                                          && (lastTime == null || daily.Created >= lastTime)
                                           && (lastDailyId == null || daily.DailyId != lastDailyId)
                                           && daily.Created >= limitTime
                                      select new DailyView
                                      {
                                          UserId = daily.UserId,
                                          Caption = daily.Caption,
                                          Created = daily.Created,
                                          DailyId = daily.DailyId,
                                          UserName = user.UserName,
                                          UserPhotoId = user.UserPhotoId,
                                      };

            var contactDailies = await contactDailiesQuery.ToListAsync();

            foreach (var daily in contactDailies)
            {
                daily.Images = await GetImagesByDailyIdAsync(daily.DailyId);
            }

            return contactDailies;
        }

        public async Task<List<DailyView>> GetEntheriaDailiesByUserIdAsync(Guid userId, DateTime? lastTime, Guid? lastDailyId)
        {
            var limitTime = DateTime.Now.AddDays(-7);

            // Adjusting the query to account for lastDailyId being null.
            var dailies = await (from daily in _enteractDbContext.Dailies
                                 join user in _enteractDbContext.Users on daily.UserId equals user.UserId
                                 where (lastTime == null || daily.Created >= lastTime)
                                       && (lastDailyId == null || daily.DailyId != lastDailyId)
                                       && daily.Created >= limitTime
                                 // Uncomment and adjust the following line if filtering by userId is required.
                                 // && daily.UserId == userId
                                 select new DailyView
                                 {
                                     UserId = daily.UserId,
                                     UserName = user.UserName,
                                     Caption = daily.Caption,
                                     Created = daily.Created,
                                     DailyId = daily.DailyId
                                 }).OrderBy(q => q.Created).ToListAsync();

            foreach (var daily in dailies)
            {
                daily.Images = await GetImagesByDailyIdAsync(daily.DailyId);
            }

            return dailies;
        }

    }
}



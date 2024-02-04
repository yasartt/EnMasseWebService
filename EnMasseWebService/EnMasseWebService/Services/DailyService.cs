using EnMasseWebService.Models.Entities;
using EnMasseWebService.Models.DTOs;
using EnMasseWebService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

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

        public async Task<List<ImageDTO>> UploadImagesAsync(List<ImageIncomingDTO> imageIncomingDTOs, int dailyId)
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
                }

                return uploadedImages;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ImageDTO>> GetImagesByDailyIdAsync(int dailyId)
        {
            var filter = Builders<ImageDTO>.Filter.Eq(x => x.DailyId, dailyId);
            var cursor = await _imagesCollection.FindAsync(filter);

            return await cursor.ToListAsync();
        }

        public async Task<List<DailyView>> GetContactDailiesByUserIdAsync(int userId, DateTime lastTime)
        {
            // Query for the user's contacts' dailies
            var contactDailiesQuery = from contact in _enteractDbContext.UserContacts
                                      join daily in _enteractDbContext.Dailies on contact.UserId equals userId
                                      where daily.UserId == contact.ContactId
                                      select new DailyView
                                      {
                                          UserId = contact.ContactId,
                                          Caption = daily.Caption,
                                          Created = daily.Created,
                                          DailyId = daily.DailyId
                                      };

            // Query for the user's own dailies
            var userDailiesQuery = from daily in _enteractDbContext.Dailies
                                   where daily.UserId == userId
                                   select new DailyView
                                   {
                                       UserId = userId,
                                       Caption = daily.Caption,
                                       Created = daily.Created,
                                       DailyId = daily.DailyId
                                   };

            // Combining both queries
            var combinedQuery = contactDailiesQuery.Union(userDailiesQuery);

            var dailies = await combinedQuery.ToListAsync();

            foreach (var dailie in dailies)
            {
                dailie.Images = await GetImagesByDailyIdAsync(dailie.DailyId);
            }

            return dailies;
        }

    }
}



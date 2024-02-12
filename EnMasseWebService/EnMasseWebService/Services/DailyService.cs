using EnMasseWebService.Models.Entities;
using EnMasseWebService.Models.DTOs;
using EnMasseWebService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

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

                    DailyImage newImageEntity = new DailyImage()
                    {
                        Id = imageDTO.Id,
                        DailyId = dailyId
                    };

                    await _enteractDbContext.DailyImages.AddAsync(newImageEntity);
                    await _enteractDbContext.SaveChangesAsync();
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
            var dailyImages = await _enteractDbContext.DailyImages.Where(q => q.DailyId == dailyId).ToListAsync();

            var imageIds = dailyImages.Select(di => di.Id).ToList();

            var images = new List<ImageDTO>();

            foreach (var imageId in imageIds)
            {
                if (!string.IsNullOrEmpty(imageId))
                {
                    // Convert string Id to ObjectId
                    var objectId = new ObjectId(imageId);
                    // Create a filter to query the document by _id
                    var filter = Builders<ImageDTO>.Filter.Eq("_id", objectId);
                    // Find the document in MongoDB
                    var image = await _imagesCollection.Find(filter).FirstOrDefaultAsync();
                    if (image != null)
                    {
                        images.Add(image);
                    }
                }
            }

            return images;
            /**

            var filter = Builders<ImageDTO>.Filter.Eq(x => x.DailyId, dailyId);
            var cursor = await _imagesCollection.FindAsync(filter);

            return await cursor.ToListAsync();*/
        }

        public async Task<List<DailyView>> GetContactDailiesByUserIdAsync(int userId, DateTime lastTime)
        {
            // Query for the user's contacts' dailies
            var contactDailiesQuery = from userContact in _enteractDbContext.UserContacts
                                      join daily in _enteractDbContext.Dailies on userContact.UserId equals userId
                                      join user in _enteractDbContext.Users on userContact.ContactId equals user.UserId
                                      where daily.UserId == userContact.ContactId
                                      select new DailyView
                                      {
                                          UserId = userContact.ContactId,
                                          Caption = daily.Caption,
                                          Created = daily.Created,
                                          DailyId = daily.DailyId,
                                          UserName = user.UserName,
                                          UserPhotoId = user.UserPhotoId,
                                      };

            // Query for the user's own dailies
            var userDailiesQuery = from daily in _enteractDbContext.Dailies
                                   join user in _enteractDbContext.Users on daily.UserId equals user.UserId
                                   where daily.UserId == userId
                                   select new DailyView
                                   {
                                       UserId = userId,
                                       Caption = daily.Caption,
                                       Created = daily.Created,
                                       DailyId = daily.DailyId,
                                       UserName = user.UserName,
                                       UserPhotoId = user.UserPhotoId,
                                   };

            // Combining both queries
            var combinedQuery = contactDailiesQuery.Union(userDailiesQuery).OrderBy(q => q.Created);

            var dailies = await combinedQuery.ToListAsync();

            foreach (var dailie in dailies)
            {
                dailie.Images = await GetImagesByDailyIdAsync(dailie.DailyId);
            }

            return dailies;
        }

        public async Task<List<DailyView>> GetEntheriaDailiesByUserIdAsync(int userId, DateTime? lastTime, int? lastDailyId)
        {
            // Adjusting the query to account for lastDailyId being null.
            var dailies = await (from daily in _enteractDbContext.Dailies
                                 join user in _enteractDbContext.Users on daily.UserId equals user.UserId
                                 where (lastTime == null || daily.Created >= lastTime)
                                       && (lastDailyId == null || daily.DailyId != lastDailyId)
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



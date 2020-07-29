using GenericAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECare.Data.DAL
{
    public class PhotoData
    {
        private readonly IUnitOfWork unitOfWork;

        public PhotoData(string CSName)
        {
            this.unitOfWork = new UnitOfWork(CSName);
        }

        public List<Photo> GetAllPhotos()
        {
            return unitOfWork.PhotoRepository.Get().ToList();
        }

        public List<Photo> GetAllAlbumPhotos(int albumId)
        {
            return unitOfWork.PhotoRepository.Get(x => x.ALBUM_ID == albumId)
                .ToList();
        }

        public void InsertPhoto(Photo dto)
        {
            var entity = new Photo()
            {
                ID = dto.ID,
                PHOTO_NAME = dto.PHOTO_NAME,
                PHOTO_IMG = dto.PHOTO_IMG,
                ALBUM_ID = dto.ALBUM_ID
            };
            unitOfWork.PhotoRepository.Insert(entity);
            unitOfWork.Save();
        }
    }
}
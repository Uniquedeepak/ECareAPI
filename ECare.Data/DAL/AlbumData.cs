using GenericAPI.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECare.Data.DAL
{
    public class AlbumData
    {
        private readonly IUnitOfWork unitOfWork;

        public AlbumData(string CSName)
        {
            this.unitOfWork = new UnitOfWork(CSName);
        }

        public Album GetAlbum(int id)
        {
            Album dto = new Album();
            if (id != 0)
            {
                dto = unitOfWork.AlbumRepository.Get(x => x.ID == id)
                     .SingleOrDefault();
            }

            return dto;
        }

        public List<Album> GetAllAlbums()
        {
            return unitOfWork.AlbumRepository.Get()
                .ToList();
        }

        public List<Album> GetLatestAlbums(int n)
        {
            return unitOfWork.AlbumRepository.Get()
                .OrderByDescending(x => x.ID)
                .Take(n)
                .ToList();
        }

        public void InsertAlbum(Album dto)
        {
            var entity = new Album()
            {
                ID = dto.ID,
                ALBUM_NAME = dto.ALBUM_NAME,
                ALBUM_IMG = dto.ALBUM_IMG,
                SchoolCode = dto.SchoolCode,
            };
            unitOfWork.AlbumRepository.Insert(entity);
            unitOfWork.Save();
        }

        public void DeleteAlbum(int Id)
        {
            if (Id != 0)
            {
                var selectedAlbum = unitOfWork.AlbumRepository.Get(x => x.ID == Id)
                     .SingleOrDefault();
                var selectedPhotos = unitOfWork.PhotoRepository.Get(x => x.ALBUM_ID == Id).ToList();
                if (selectedAlbum != null)
                {
                    foreach (var item in selectedPhotos)
                    {
                        unitOfWork.PhotoRepository.Delete(item.ID);
                    }
                    unitOfWork.AlbumRepository.Delete(Id);
                    unitOfWork.Save();
                }
            }
        }
    }
}
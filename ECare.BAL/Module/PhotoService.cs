using ECare.Data;
using ECare.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public class PhotoService : IPhotoService
    {
        readonly PhotoData data = null;
        public PhotoService(string CS_Name)
        {
            data = new PhotoData(CS_Name);
        }

        public List<Photo> GetAllPhotos()
        {
            return data.GetAllPhotos();
        }

        public List<Photo> GetAllAlbumPhotos(int albumId)
        {
            return data.GetAllAlbumPhotos(albumId);
        }

        public void InsertPhoto(Photo dto)
        {
            data.InsertPhoto(dto);
        }
    }
}

using ECare.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public interface IPhotoService
    {
        List<Photo> GetAllPhotos();

        List<Photo> GetAllAlbumPhotos(int albumId);

        void InsertPhoto(Photo dto);
    }
}

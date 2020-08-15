using ECare.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public interface IAlbumService
    {
        List<Album> GetAllAlbums();

        List<Album> GetLatestAlbums(int n);

        Album GetAlbum(int id);

        void InsertAlbum(Album dto);

        void DeleteAlbum(int Id);
    }
}

using ECare.Data;
using ECare.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECare.BAL.Module
{
    public class AlbumService : IAlbumService
    {
        readonly AlbumData data = null;
        public AlbumService(string CS_Name)
        {
            data = new AlbumData(CS_Name);
        }

        public Album GetAlbum(int id)
        {
            Album dto = new Album();
            if (id != 0)
            {
                dto = data.GetAlbum(id);
            }

            return dto;
        }

        public List<Album> GetAllAlbums()
        {
            return data.GetAllAlbums();
        }

        public List<Album> GetLatestAlbums(int n)
        {
            return data.GetLatestAlbums(n);
        }

        public void InsertAlbum(Album dto)
        {
            data.InsertAlbum(dto);
        }

        public void DeleteAlbum(int Id)
        {
            data.DeleteAlbum(Id);
        }
    }
}

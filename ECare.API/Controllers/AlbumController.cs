using ECare.API.Models;
using ECare.API.Services;
using ECare.BAL.Module;
using ECare.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ECare.API.Controllers
{
    [RoutePrefix("api/school")]
    public class AlbumController : ApiController
    {
        private readonly IAlbumService _albumService;
        private readonly IPhotoService _photoService;

        public AlbumController()
        {
            _albumService = new AlbumService(ConnectionStringNames.DBEntityName);
            _photoService = new PhotoService(ConnectionStringNames.DBEntityName);
        }

        // GET: Album/AllAlbums
        [HttpGet]
        [Route("Albums")]
        public async Task<IHttpActionResult> AllAlbums()
        {
            var model = new AlbumSearchViewModel();
            model.SearchResults = _albumService.GetAllAlbums();
            
            Response res = new Response()
            {
                ResponseCode = HttpStatusCode.OK.ToString(),
                ResponseMessage = "Success",
                Result = model.SearchResults
            };
            return Ok(res);
         }

        // POST: Album/AllAlbums
        [HttpPost]
        [Route("Albums")]
        public async Task<IHttpActionResult> AllAlbums(AlbumSearchViewModel model)
        {
            var albums = _albumService.GetAllAlbums();

            if (!string.IsNullOrWhiteSpace(model.SearchWord))
            {
                albums = albums.Where(b => b.ALBUM_NAME.ToLower().Contains(model.SearchWord.ToLower())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(model.SchoolCode))
            {
                albums = albums.Where(b => b.SchoolCode.ToLower().Contains(model.SchoolCode.ToLower())).ToList();
            }

            model.SearchResults = albums;
            Response res = new Response()
            {
                ResponseCode = HttpStatusCode.OK.ToString(),
                ResponseMessage = "Success",
                Result = model.SearchResults
            };
            return Ok(res);
        }

        // POST: Album/CreateAlbum
        [HttpPost]
        [Route("CreateAlbum")]
        public async Task<IHttpActionResult> CreateAlbum()
        {
            Response res;
            List<Photo> AlbumPhotoList= new List<Photo>();
            // Check if the request contains multipart/form-data.  
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
            //access form data  
            NameValueCollection formData = provider.FormData;
            //access files  
            IList<HttpContent> files = provider.Files;

            AlbumCreateViewModel acvm = new AlbumCreateViewModel()
            {
                AlbumName = formData["AlbumName"].ToString(),
                SchoolCode = formData["SchoolCode"].ToString(),
            };
           
            foreach (HttpContent docfile in files)
            {
                string thisFileName = docfile.Headers.ContentDisposition.FileName.Trim('\"');
                string fileFormat = docfile.Headers.ContentType.ToString();
                if (fileFormat.Equals("image/jpeg") || fileFormat.Equals("image/png"))
                {
                    Stream input = await docfile.ReadAsStreamAsync();
                    
                    string baseUrl =$"{HttpContext.Current.Request.Url.Scheme}://{HttpContext.Current.Request.Url.Authority}{HttpContext.Current.Request.ApplicationPath.TrimEnd('/')}/";
                    string ServerDocsPath = $"{baseUrl}wwwroot/image/gallery/{acvm.SchoolCode}/{acvm.AlbumName}/";
                    string ServerDocsURL = ServerDocsPath + thisFileName;

                    string DocsPath = HttpContext.Current.Server.MapPath($"~/wwwroot/image/gallery/{acvm.SchoolCode}/{acvm.AlbumName}/");
                    string URL = DocsPath + thisFileName;
                    
                    if (!Directory.Exists(DocsPath))
                    {
                        Directory.CreateDirectory(DocsPath);
                    }

                    if (File.Exists(URL))
                    {
                        File.Delete(URL);
                    }

                    using (Stream file = File.OpenWrite(URL))
                    {
                        input.CopyTo(file);
                        file.Close();
                    }
                    Photo photo = new Photo()
                    {
                        PHOTO_NAME = thisFileName,
                        PHOTO_IMG = ServerDocsURL
                    };
                    AlbumPhotoList.Add(photo);
                }
            }

            if (AlbumPhotoList.Count > 0)
            {
                #region Insert Or Update Into DB
                int lastAlbumId;
                var CheckAlbum = _albumService.GetAllAlbums().
                    Where(
                    x => x.SchoolCode.ToLower().Equals(acvm.SchoolCode.ToLower()) && 
                    x.ALBUM_NAME.ToLower().Equals(acvm.AlbumName.ToLower())
                    ).FirstOrDefault();

                if (CheckAlbum ==null)
                {
                    Album InsertAlbum = new Album()
                    {
                        ALBUM_IMG = AlbumPhotoList?[0].PHOTO_IMG,
                        ALBUM_NAME = acvm.AlbumName,
                        SchoolCode = acvm.SchoolCode
                    };
                    _albumService.InsertAlbum(InsertAlbum);
                    lastAlbumId = _albumService.GetLatestAlbums(1).FirstOrDefault().ID;
                }
                else
                {
                    lastAlbumId = CheckAlbum.ID;
                }
                
                foreach (var item in AlbumPhotoList)
                {
                    item.ALBUM_ID = lastAlbumId;
                    var checkPhoto = _photoService.GetAllPhotos().Where(
                    x => x.ALBUM_ID.Equals(lastAlbumId) &&
                    x.PHOTO_NAME.ToLower().Equals(item.PHOTO_NAME.ToLower())
                    ).FirstOrDefault();
                    if (checkPhoto == null)
                    {
                        _photoService.InsertPhoto(item);
                    }
                }
                #endregion

                res = new Response()
                {
                    ResponseCode = HttpStatusCode.OK.ToString(),
                    ResponseMessage = "Success",
                    Result = "Album Created Successfully."
                };
            }
            else
            {
                res = new Response()
                {
                    ResponseCode = HttpStatusCode.OK.ToString(),
                    ResponseMessage = "Success",
                    Result = "Album Not Created."
                };
            }
           
            return Ok(res);
        }

        // DELETE: api/Album/Delete/5
        [Route("Album/Delete/{Id}")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> Delete(int Id)
        {
            Response res = null;
            try
            {
                _albumService.DeleteAlbum(Id);
                res = new Response()
                {
                    ResponseCode = "200",
                    ResponseMessage = "Success",
                    Result = "Album deleted successfully"
                };
            }
            catch (Exception ex)
            {
                res = new Response()
                {
                    ResponseCode = HttpStatusCode.InternalServerError.ToString(),
                    ResponseMessage = "Exception",
                    Result = ex.Message.ToString()
                };
            }
            return Ok(res);
        }

        //
        // GET: Album/Details
        [HttpGet]
        [Route("Album/{id}")]
        public async Task<IHttpActionResult> Details(int id)
        {
            var album = _albumService.GetAlbum(id);
            var photos = _photoService.GetAllAlbumPhotos(album.ID);

            var model = new AlbumDetailsViewModel()
            {
                Id = album.ID,
                AlbumName = album.ALBUM_NAME,
                AlbumImage = album.ALBUM_IMG,
                Photos = photos
            };

            Response res = new Response()
            {
                ResponseCode = HttpStatusCode.OK.ToString(),
                ResponseMessage = "Success",
                Result = model
            };
            return Ok(res);
        }
    }
}

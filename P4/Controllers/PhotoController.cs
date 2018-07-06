using Newtonsoft.Json;
using P4.Entities;
using P4.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace P4.Controllers
{
    public class PhotoController : Controller
    {
        public ActionResult Photo()
        {
            channelEntities dbPhoto = new channelEntities();
            classPro1 Photo = new classPro1();

            
            Photo.rooms = (from users in dbPhoto.rooms.AsEnumerable()
                                 select new
                                 {
                                     NamePhoto = users.NamePhoto
                                 }).ToList();
            return View(Photo.rooms);
        }
        public JsonResult ImageUpload(string Account)
        {
            try
            {


                channelEntities pro1Entities = new channelEntities();
                dynamic data = JsonConvert.DeserializeObject(Account);
                room albums = new room();
                albums.NamePhoto = data.Album;
                albums.cName = data.id.ToString();
                albums.cPass = data.pass.ToString();
                //albums.CreateAt = DateTime.Now;
                pro1Entities.rooms.Add(albums);
                pro1Entities.SaveChanges();
                Directory.CreateDirectory(Server.MapPath("~/Photos/" + albums.cID));


                int i = 1;
                foreach (dynamic items in data.img)
                {
                    // Convert Base64 String to byte[]
                    string[] words = (items.fileName).ToString().Split(',');
                    byte[] imageBytes = Convert.FromBase64String(words[1]);
                    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                    // Convert byte[] to Image
                    ms.Write(imageBytes, 0, imageBytes.Length);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                    string newFile = i + ".jpg";
                    string filePath = Path.Combine(Server.MapPath("~/Photos/" + albums.cID), newFile);
                    //string filePath = Path.Combine(Server.MapPath(@"~C:\Photos\" + albums.IDAlbum), newFile);
                    //string vPath = filePath.Replace(@"C:\code", "~").Replace(@"\", "/");
                    string dbPathImageMore = "/Photos/" + albums.cID.ToString()+"/"+newFile;




                    image.Save(filePath, ImageFormat.Jpeg);
                    //HttpContextBase
                    string currentUrl = System.Web.HttpContext.Current.Request.Url.ToString().ToLower();
                    if (currentUrl.Contains("localhost"))   //  เป็น  version ทำในเครื่องหรือป่าว
                    {
                        //trucur = "yes";   // ถ้าทำในเครื่อง
                    }
                    else
                    {
                        //nocur = "no";     // version deploy
                    }
                    //rooms photostock = new rooms();
                    //photostock.NamePhoto1 = dbPathImageMore;
                    //photostock.cID1 = albums.cID;
                    //pro1Entities.rooms.Add(photostock);
                    //user model = p1.users.FirstOrDefault(x => x.userName == fName && x.userPass == lName);
                    //model.status = true;
                    //Session["login"] = true;
                    //p1.SaveChanges();
                    i++;
                }
                pro1Entities.SaveChanges();
                return Json(new { status = "200", message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "500", message = ex.Message });
            }
        }
        public ActionResult ShowAlbum()
        {
            channelEntities dbPhoto = new channelEntities();

            return View(dbPhoto.rooms.ToList());
        }

        //public JsonResult Show(string PhotoData) //คำสั่ง action โฟกัสให้ทำงานที่หน้าไหน
        //{
        //    channelEntities dbphoto = new channelEntities();
        //    classPro1 photos = new classPro1();
        //    dynamic data = JsonConvert.DeserializeObject(PhotoData);
        //    int Idphoto = data.Id;
        //    //สร้างการดึงข้อมูล
        //    photos.rooms = (from room in dbphoto.rooms.AsEnumerable()
        //                          where room.IDAlbum == Idphoto
        //                          select new
        //                          {
        //                              NamePhoto = room.NamePhoto,

        //                          }).First();
        //    return Json(photos.Photostocks);
        //}
    }

}
using Service.DBContext;
using System.Data.Entity;
using System.Net.Http;
using System.Web.Http;
using System.Linq;

namespace Service.Controllers
{
    public class UserAPIController : BaseAPIController
    {
        //public HttpResponseMessage Get()
        //{
        //    return ToJson(UserDB.TblUsers.AsEnumerable());
        //}

        //public HttpResponseMessage Post([FromBody]TblUser value)
        //{
        //    UserDB.TblUsers.Add(value);
        //    return ToJson(UserDB.SaveChanges());
        //}

        //public HttpResponseMessage Put(int id, [FromBody]TblUser value)
        //{
        //    UserDB.Entry(value).State = EntityState.Modified;
        //    return ToJson(UserDB.SaveChanges());
        //}
        //public HttpResponseMessage Delete(int id)
        //{
        //    UserDB.TblUsers.Remove(UserDB.TblUsers.FirstOrDefault(x => x.Id == id));
        //    return ToJson(UserDB.SaveChanges());
        //}
        public IHttpActionResult Get(string name)
        {
            return Ok(new { Success = true, Message = "Name: " + name });
        }
    }
}
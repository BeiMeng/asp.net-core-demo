//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using NetCore.MySqlWebApi.Models;

//namespace NetCore.MySqlWebApi.Controllers
//{
//    public class PageParams
//    {
//        public string OrderBy { get; set; }
//        public int PageSize { get; set; }
//        public int PageIndex { get; set; }
//    }
//    public class QueryPersonsParams : PageParams
//    {
//        public string Name { get; set; }
//        public int? Age { get; set; }
//    }
//    public class PageList<T>
//    {
//        public int TotalCount { get; set; }
//        public List<T> List { get; set; }
//    }
//    public class EntityId
//    {
//        public Guid PersonId { get; set; }
//    }
//    [Route("api/[controller]/[Action]")]
//    [ApiController]
//    public class PersonsController : ControllerBase
//    {
//        private readonly BloggingContext _context;

//        public PersonsController(BloggingContext context)
//        {
//            _context = context;
//        }
//        public async Task<PageList<Person>> QueryList(QueryPersonsParams queryPersonsParams)
//        {
//            PageList<Person> pageList = new PageList<Person>();
//            var query = _context.Persons.OrderBy(p => p.Age).Take(queryPersonsParams.PageSize).Skip((queryPersonsParams.PageIndex - 1) * queryPersonsParams.PageSize);
//            pageList.TotalCount = await query.CountAsync();
//            pageList.List = await query.ToListAsync();
//            return pageList;
//        }
//        //[HttpGet]
//        public async Task<Person> GetByKeyId(EntityId id)
//        {
//            var person = await _context.Persons.FindAsync(id.PersonId);
//            return person;
//        }
//        public async Task Delete(EntityId id)
//        {
//            var person = await _context.Persons.FindAsync(id.PersonId);
//            if (person == null)
//            {
//                return;
//            }

//            _context.Persons.Remove(person);
//            await _context.SaveChangesAsync();

//            return;
//        }
//        public async Task<Guid> CreateOrUpdate(Person person)
//        {
//            if (person.PersonId == Guid.Empty)
//            {
//                return await Create(person);
//            }
//            else
//            {
//                return await Update(person);
//            }
//        }
//        private async Task<Guid> Create(Person person)
//        {
//            _context.Persons.Add(person);
//            await _context.SaveChangesAsync();
//            return person.PersonId;
//        }
//        private async Task<Guid> Update(Person person)
//        {
//            var p = await _context.Persons.FindAsync(person.PersonId);
//            if (p != null)
//            {
//                p.Name = person.Name;
//                p.Age = person.Age;
//                p.Sex = person.Sex;
//                await _context.SaveChangesAsync();
//            }
//            return person.PersonId;
//        }
//        public string GetStringById(string id)
//        {
//            return id;
//        }
//    }
//}
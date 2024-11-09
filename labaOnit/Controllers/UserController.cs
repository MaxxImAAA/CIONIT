using labaOnit.Dtos;
using labaOnit.InterfacesAndRealization;
using labaOnit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace labaOnit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IBaseRepository<User> _userRepository;
        public UserController(IBaseRepository<User> _userRepository)
        {
            this._userRepository = _userRepository;
        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<string>> AddUser(CreateUserDto dto)
        {
            var birArr = dto.BirthdayDate.Split('-');
            var recArr = dto.ReceiptDate.Split('-');

            var b = $"{birArr[2]}.{birArr[1]}.{birArr[0]}";
            var r = $"{recArr[2]}.{recArr[1]}.{recArr[0]}";

            Console.WriteLine("dsfsf");

            //Console.WriteLine($"proverka {newB}, {newR}");
            var birth = DateTime.ParseExact(b, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var rec = DateTime.ParseExact(r, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var user = new User
            {
                FirstName = dto.FirstName,
                MidleName = dto.MidleName,
                LastName = dto.LastName,
                ReceiptDate = DateTime.SpecifyKind(rec, DateTimeKind.Utc),
                BirthdayDate = DateTime.SpecifyKind(birth, DateTimeKind.Utc),

                RecordBook = dto.RecordBook,
            };

            await _userRepository.CreateAsync(user);

            /*var user_ = await _userRepository.GetAll()
                                        .FirstOrDefaultAsync(x=> x.FirstName == user.FirstName &&
                                        x.LastName == user.LastName)*/
            var user_ = await _userRepository.GetAll()
                                             .Where(x => x.FirstName == user.FirstName &&
                                        x.LastName == user.LastName).Select(x => x.Id).FirstOrDefaultAsync();



            return $"{user_}";
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userRepository.GetAll().Select(x => new UserDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                MidleName = x.MidleName,
                LastName = x.LastName,
                RecordBook = x.RecordBook,
                BirthdayDate = x.BirthdayDate.ToShortDateString(),
                ReceiptDate = x.ReceiptDate.ToShortDateString(),

            }).ToListAsync();

            return users;
        }

        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<string>> DeleteUser(int id)
        {
            var user = _userRepository.GetAll()
                                      .FirstOrDefault(x => x.Id == id);

            await _userRepository.Delete(user);


            return "Поьзователь удален";
        }


        [HttpPost("FiltrBirthdayDate")]
        public async Task<ActionResult<List<User>>> FiltrBirthdayDate(FiltrUserDto dto)
        {
            var today = DateTime.Today;

            var datastart = dto.DateTimeStart != null ? dto.DateTimeStart :
            _userRepository.GetAll().Min(x => x.ReceiptDate);

            var dataend = dto.DateTimeEnd != null ? dto.DateTimeEnd :
            _userRepository.GetAll().Max(x => x.ReceiptDate);

            var agestart = dto.AgeStart != null ? today.Year - dto.AgeStart :
             _userRepository.GetAll().Min(x => x.BirthdayDate).Year;

            var ageend = dto.AgeEnd != null ? today.Year - dto.AgeEnd :
           _userRepository.GetAll().Max(x => x.BirthdayDate).Year;

            /*var res = await _userRepository.GetAll().Where(x =>
                x.ReceiptDate >= datastart &&
                x.ReceiptDate <= dataend &&
                DateHelper.GetAge(x.BirthdayDate) >= agestart &&
                  DateHelper.GetAge(x.BirthdayDate) <= ageend).ToListAsync();*/


            var res = _userRepository.GetAll().Where
                (
                x => x.ReceiptDate >= datastart &&
                     x.ReceiptDate <= dataend &&
                     (x.BirthdayDate.Year >= today.Year - dto.AgeStart) &&
                     (x.BirthdayDate.Year <= today.Year - dto.AgeEnd)
             ).ToList();

            return res;
        }

        [HttpGet("FiltrByLastname")]
        public async Task<ActionResult<List<UserDto>>> FiltrByLastName()
        {
            var users = await _userRepository.GetAll()
                                              .OrderBy(x => x.LastName)
                                              .Select(x => new UserDto
                                              {
                                                  Id = x.Id,
                                                  FirstName = x.FirstName,
                                                  MidleName = x.MidleName,
                                                  LastName = x.LastName,
                                                  RecordBook = x.RecordBook,
                                                  BirthdayDate = x.BirthdayDate.ToShortDateString(),
                                                  ReceiptDate = x.ReceiptDate.ToShortDateString(),

                                              })
                                              .ToListAsync();

            return users;
        }

        [HttpGet("FiltrByRecordBook")]
        public async Task<ActionResult<List<UserDto>>> FiltrByRecordBook()
        {
            var users = await _userRepository.GetAll()
                                              .OrderBy(x => x.RecordBook)
                                              .Select(x => new UserDto
                                              {
                                                  Id = x.Id,
                                                  FirstName = x.FirstName,
                                                  MidleName = x.MidleName,
                                                  LastName = x.LastName,
                                                  RecordBook = x.RecordBook,
                                                  BirthdayDate = x.BirthdayDate.ToShortDateString(),
                                                  ReceiptDate = x.ReceiptDate.ToShortDateString(),

                                              }).ToListAsync();

            return users;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBeautyWebApi.Classes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iBeautyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly iBeautyContext _context;

        public UserController(iBeautyContext context)
        {
            _context = context;
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _context.Users.Where(x => x.UserId == id).Select(User => new
            {
                UserId = User.UserId,
                Firstname = User.Firstname,
                Lastname = User.Lastname,
                Email = User.Email,
                Telephone = User.Telephone,
                Picture = User.Picture,
                Password = User.Password,
                VerificationCode = User.VerificationCode,
                Status = User.Status,
                DateAdded = User.DateAdded,
                DateModified = User.DateModified
            }).ToListAsync();

            var validar = user == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("UserLogin")]
        public async Task<ActionResult<Users>> PostCustomerLogin(Users user)
        {
            Users item = new Users()
            {
                Email = user.Email,
                Password = user.Password
            };

            var passencrypted = Encrypter.Encrypt(user.Password);

            var customer = await _context.Users.FirstOrDefaultAsync(q => q.Email == user.Email && q.Password == passencrypted);

            var validar = customer == null;
            if (validar)
            {
                return NotFound();
            }

            return Ok(new
            {
                UserId = customer.UserId,
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                Email = customer.Email,
                Telephone = customer.Telephone,
                Picture = customer.Picture,
                Password = customer.Password,
                VerificationCode = customer.VerificationCode,
                Status = customer.Status,
                DateAdded = customer.DateAdded,
                DateModified = customer.DateModified
            });
        }

        [HttpPost("UserRegistration")]
        public async Task<ActionResult<Users>> PostCustomer(Users user)
        {
            //Random rdm = new Random();
            //var code = rdm.Next(1000, 9000);
            var code = 1111;

            Users item = new Users()
            {
                CityId = 1,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Telephone = user.Telephone,
                Picture = ".png",
                Password = Encrypter.Encrypt(user.Password),
                VerificationCode = code,
                Status = false,
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now
            };

            var validarFirstName = item.Firstname == "";
            if (validarFirstName)
            {
                return BadRequest("El nombre no puede ir vacio.");
            }

            var validarLastName = item.Lastname == "";
            if (validarLastName)
            {
                return BadRequest("El nombre no puede ir vacio.");
            }

            var validarEmail = item.Email == "";
            if (validarEmail)
            {
                return BadRequest("El email no puede ir vacio.");
            }

            var validarTelephone = item.Telephone == "";
            if (validarTelephone)
            {
                return BadRequest("El Telefono no puede ir vacio.");
            }

            var validarPassword = item.Password == "";
            if (validarPassword)
            {
                return BadRequest("La contraseña no puede ir vacia.");
            }

            var emailexist = _context.Users.FirstOrDefault(x => x.Email == item.Email);
            var telephoneexist = _context.Users.FirstOrDefault(x => x.Telephone == item.Telephone);

            var validar = emailexist == null || telephoneexist == null;
            if (validar)
            {
                _context.Users.Add(item);
                await _context.SaveChangesAsync();

                Email obj = new Email();
                obj.SendRegistrationCode(item.Email, item.Firstname, code);

                return Ok(new
                {
                    UserId = item.UserId,
                    Firstname = item.Firstname,
                    Lastname = item.Lastname,
                    Email = item.Email,
                    Telephone = item.Telephone,
                    Picture = item.Picture,
                    Password = item.Password,
                    VerificationCode = item.VerificationCode,
                    Status = item.Status,
                    DateAdded = item.DateAdded,
                    DateModified = item.DateModified
                });
            }
            else
            {
                return BadRequest("El email que registro ya esta asociado a una cuenta existente.");
            }
        }

        [HttpPost("UserVerification")]
        public async Task<ActionResult<Users>> PostCustomerVerification(Users user)
        {
            Users item = new Users()
            {
                UserId = user.UserId,
                VerificationCode = user.VerificationCode
            };

            var users = _context.Users.FirstOrDefault(q => q.UserId == user.UserId && q.VerificationCode == user.VerificationCode);

            var validar = users == null;
            if (validar)
            {
                return NotFound();
            }

            await PutCustomerVerification(user.UserId);

            return Ok(new
            {
                UserId = users.UserId,
                Firstname = users.Firstname,
                Lastname = users.Lastname,
                Email = users.Email,
                Telephone = users.Telephone,
                Picture = users.Picture,
                Password = users.Password,
                VerificationCode = users.VerificationCode,
                Status = users.Status,
                DateAdded = users.DateAdded,
                DateModified = users.DateModified
            });
        }
        public async Task<IActionResult> PutCustomerVerification(int id)
        {
            var data = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            data.Status = true;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("EditUserPassword/{email}/{password}")]
        public async Task<IActionResult> PutCustomer(string email, string password, Users user)
        {
            var passencrypted = Encrypter.Encrypt(password);
            var data = await _context.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == passencrypted);

            var validar = data == null;
            if (validar)
            {
                return BadRequest();
            }

            data.Password = Encrypter.Encrypt(user.Password);
            data.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new
            {
                UserId = data.UserId,
                Firstname = data.Firstname,
                Lastname = data.Lastname,
                Email = data.Email,
                Telephone = data.Telephone,
                Picture = data.Picture,
                Password = data.Password,
                VerificationCode = data.VerificationCode,
                Status = data.Status,
                DateAdded = data.DateAdded,
                DateModified = data.DateModified
            });
        }

        [HttpPut("EditUserInfo/{id}")]
        public async Task<IActionResult> PutCustomer(int id, Users user)
        {
            var data = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            var validar = data == null;
            if (validar)
            {
                return NotFound();
            }

            data.Firstname = user.Firstname;
            data.Lastname = user.Lastname;
            data.Telephone = user.Telephone;
            data.DateModified = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(new
            {
                UserId = data.UserId,
                Firstname = data.Firstname,
                Lastname = data.Lastname,
                Email = data.Email,
                Telephone = data.Telephone,
                Picture = data.Picture,
                Password = data.Password,
                VerificationCode = data.VerificationCode,
                Status = data.Status,
                DateAdded = data.DateAdded,
                DateModified = data.DateModified
            });
        }

        [HttpGet("ForgotPassword/{email}")]
        public async Task<ActionResult> SendNewPassword(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(q => q.Email == email);

            var validar = user == null;
            if (validar)
            {
                return Ok();
            }

            Random rdm = new Random();
            var code = rdm.Next(1000, 9000);

            var temp = "ibeauty" + code;

            await PutCustomerTemporalPassword(user.UserId, temp);

            Email obj = new Email();
            obj.SendTemporalPassword(user.Email, user.Firstname, temp);

            return Ok();
        }

        public async Task<IActionResult> PutCustomerTemporalPassword(int id, string temppassword)
        {
            var customer = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            customer.Password = Encrypter.Encrypt(temppassword);

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

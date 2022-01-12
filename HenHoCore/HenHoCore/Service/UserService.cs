using HenHoCore.Dto;
using HenHoCore.Model;
using HenHoCore.Ulti;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HenHoCore.Service
{
    public class UserService
    {
        protected readonly MyContext context;
        protected readonly IWebHostEnvironment hostEnvironment;

        public UserService(MyContext context, IWebHostEnvironment hostEnvironment)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
        }

        /// <summary>
        /// Lấy thông tin cá nhân của tất cả user theo giới tính
        /// </summary>
        /// <param name="gender"> cần tìm kiếm theo giới tính</param>
        /// <returns>Danh sách thông tin user theo giới tính</returns>
        public List<UserDto> GetUsers(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
                gender = null;

            return this.context.Users
                .Where(x =>x.Gender.Contains(gender))
                 .Select(x => new UserDto()
                 {
                     Code = x.Code,
                     FullName = x.FullName,
                     Address = x.Address,
                     Avatar = x.Avatar,
                     Email = x.Email,
                     Gender = x.Gender,
                     Phone = x.Phone,
                     Dob = x.Dob,
                     LastLogin = x.LastLogin
                 })
                 .ToList();
        }


        public User GetUserWithRole(string userCode)
        {
            return this.context.Users
                .Where(x => x.Code.Equals(userCode))
                 .Select(x => new User()
                 {
                     Code = x.Code,
                     FullName = x.FullName,
                     Address = x.Address,
                     Avatar = x.Avatar,
                     Email = x.Email,
                     Gender = x.Gender,
                     Phone = x.Phone,
                     Dob = x.Dob,
                     Role = x.Role,
                     LastLogin = x.LastLogin
                 }).FirstOrDefault();
        }
        public List<User> GetUsersWithRole() {
            return this.context.Users
                 .Select(x => new User()
                 {
                     Code = x.Code,
                     FullName = x.FullName,
                     Address = x.Address,
                     Avatar = x.Avatar,
                     Email = x.Email,
                     Gender = x.Gender,
                     Phone = x.Phone,
                     Dob = x.Dob,
                     Role = x.Role,
                     LastLogin = x.LastLogin
                 })
                 .ToList();
        }

        /// <summary>
        /// Lấy thông tin cá nhân của user
        /// </summary>
        /// <param name="userCode">User hiện tại đang đăng nhập</param>
        /// <returns>Thông tin user</returns>
        public UserDto GetProfile(string userCode)
        {
            return this.context.Users
                    .Where(x => x.Code.Equals(userCode))
                    .Select(x => new UserDto()
                    {
                        Code = x.Code,
                        FullName = x.FullName,
                        Address = x.Address,
                        Avatar = x.Avatar,
                        Email = x.Email,
                        Gender = x.Gender,
                        Phone = x.Phone,
                        Dob = x.Dob
                    }).FirstOrDefault();
        }

        /// <summary>
        /// Cập nhật thông tin cá nhân
        /// </summary>
        /// <param name="userCode">User hiện tại đang đăng nhập</param>
        /// <param name="user">Thông tin user</param>
        /// <returns></returns>
        public UserDto UpdateProfile(string userCode, UserDto user)
        {
            User us = this.context.Users
                    .FirstOrDefault(x => x.Code.Equals(userCode));

            if (us != null)
            {
                us.FullName = user.FullName;
                us.Dob = user.Dob;
                us.Email = user.Email;

                if (user.Avatar.Contains("data:image/jpeg;base64,"))
                {
                    string pathAvatar = $"Resource/Avatar/{Guid.NewGuid().ToString("N")}";
                    string pathFile = Path.Combine(this.hostEnvironment.ContentRootPath, pathAvatar);
                    DataHelper.Base64ToImage(user.Avatar.Replace("data:image/jpeg;base64,", ""), pathFile);
                    us.Avatar = user.Avatar = pathAvatar;
                }

                us.Address = user.Address;
                us.Phone = user.Phone;
                us.Gender = user.Gender;

                this.context.SaveChanges();
            }
            return user;
        }

        /// <summary>
        /// Lấy danh sách user like user hiện tại đang đăng nhập
        /// </summary>
        /// <param name="userCode">User hiện tại đang đăng nhập</param>
        /// <returns>Danh sách user like user đó</returns>
        public List<UserDto> GetUserLikeMe(string userCode)
        {
            return this.context.Contacts
                     .Where(x => x.ContactCode.Equals(userCode) && x.isMatch.Equals(1))
                     .OrderByDescending(x => x.Created)
                     .Select(x => new UserDto()
                     {
                         Avatar = x.User.Avatar,
                         Code = x.User.Code,
                         FullName = x.User.FullName,
                         Address = x.User.Address,
                         Dob = x.User.Dob,
                         Email = x.User.Email,
                         Gender = x.User.Gender,
                         Phone = x.User.Phone,
                         IsMatch = x.isMatch
                     }).ToList();
        }


        public ContactDto GetLike(string userCode, string contactCode)
        {
            Contact contact = this.context.Contacts
                .FirstOrDefault(x => x.UserCode.Equals(userCode) && x.ContactCode.Equals(contactCode));

            if (contact == null)
            {
                AddContact(userCode, contactCode);
            }

            return this.context.Contacts
                    .Where(x => x.UserCode.Equals(userCode) && x.ContactCode.Equals(contactCode))
                    .Select(x => new ContactDto()
                    {
                        Id = x.Id,
                        UserCode = x.UserCode,
                        ContactCode = x.ContactCode,
                        isMatch = x.isMatch,
                        Created = x.Created
                    }).FirstOrDefault();
        }

        public void AddContact(string userCode, string contactCode)
        {
            Contact contact = new Contact()
            {
                UserCode = userCode,
                ContactCode = contactCode,
                Created = DateTime.Now,
            };
            context.Contacts.Add(contact);

            context.SaveChanges();
        }

        public ContactDto UpdateContact(ContactDto contact)
        {
            Contact ct = this.context.Contacts
                    .FirstOrDefault(x => x.UserCode.Equals(contact.UserCode) && x.ContactCode.Equals(contact.ContactCode));

            if (ct != null)
            {
                ct.isMatch = contact.isMatch;
                ct.Created = DateTime.Now;
                this.context.SaveChanges();
            }
            return contact;
        }
    }
}

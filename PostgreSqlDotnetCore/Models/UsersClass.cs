using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PostgreSqlDotnetCore.Models
{
    [Table("users", Schema = "project")]
    public class UsersClass
    {


        Dictionary<int, string> roles = new Dictionary<int, string>();
        Dictionary<int, string> jobes = new Dictionary<int, string>();
        public UsersClass()
        {
            pupulateRoles();
            pupulateJobs();
        }

        private void pupulateRoles()
        {
            roles.Add(RoleConstants.Admin, "admin");
            roles.Add(RoleConstants.Standard, "standard");
            roles.Add(RoleConstants.Manager, "manager");
        }

        private void pupulateJobs()
        {
            jobes.Add(1, "doctor");
            jobes.Add(2, "secretary");
            jobes.Add(3, "manager");
            jobes.Add(4, "developer");
            jobes.Add(5, "HR");
        }

        public UsersClass(string email, string name, string lastname, string password, string number, int role_id, int? jobs_id)
        {
            pupulateRoles();

            this.email = email;
            this.name = name;
            this.lastname = lastname;
            this.password = password;
            this.number = number;
            this.role_id = role_id;
            this.jobs_id = jobs_id;
        }

        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [Required(ErrorMessage = "The customer name is required")]
        public string name { get; set; }
        [Required(ErrorMessage = "The customer lastname is required")]
        public string lastname { get; set; }
        //[NotMapped]
        public string? password { get; set; }

        [Required(ErrorMessage = "The number is required")]
        public string number { get; set; }
        public int role_id { get; set; }
        public int? jobs_id { get; set; }


        public string GetRoleName
        {
            get
            {
                return roles.ContainsKey(role_id) ? roles[role_id] : role_id.ToString();
            }
        }

        public string? GetJobName
        {
            get
            {
                try
                {
                    if (jobs_id == null)
                    {
                        return "-";
                    }
                    return jobes.ContainsKey((int)jobs_id) ? jobes[(int)jobs_id] : jobs_id.ToString();
                } catch (Exception e)
                {
                    return "-";
                }
            }
        }


    }
}
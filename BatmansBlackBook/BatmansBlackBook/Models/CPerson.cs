using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BatmansBlackBook.Models
{
    public class CPerson
    {
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string MiddleName {get;set;}
        public string HeroName {get;set;}
        public string Email {get;set;}
        public string Birthdate {get;set;}
        public int Age {get;set;}

        private Configuration webConfig = WebConfigurationManager.OpenWebConfiguration("~/web.config");

        private SqlConnection getConnection()
        {
            return new SqlConnection(webConfig.ConnectionStrings.ConnectionStrings[1].ToString());
        }
                    
        public string AddPerson() {
            runCommand("Insert Into dbo.Person('','','','','');");
            return "Finished";
        }

        public CPerson[] GetPersons()
        {
            return runCommand("EXEC GetPersons");            
        }

        private CPerson[] runCommand(string sql)
        {
            CPerson[] PersonArray = new CPerson[15];
            SqlCommand command = new SqlCommand(sql, getConnection());
            command.Connection.Open();
            IDataReader dRead = command.ExecuteReader();
            
            while (dRead.Read())
            {
                CPerson person = new CPerson();
                person.FirstName = dRead.GetString(1);
                person.LastName = dRead.GetString(2);
                person.HeroName = dRead.GetString(3);
                person.MiddleName = dRead.GetString(4);
                person.Birthdate = dRead.GetString(5);
                person.Email = dRead.GetString(6);
                PersonArray[dRead.GetInt32(0)] = person;
            }

            command.Connection.Close();

            return PersonArray;
        }
    }
}
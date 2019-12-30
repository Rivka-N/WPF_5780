using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        #region fields
        private Int32 hostKey;
        private string name;
        private string lastName;
        private System.Net.Mail.MailAddress mail;
        private Int32 phoneNumber;
        private BankAccount bank;
        #endregion
        #region properties
        public Int32 HostKey { get=>hostKey; set { hostKey = value; } }
        public string Name { get => name; set { name = value; } }
        public string LastName { get => lastName; set { lastName = value; } }
        public Int32 Phone { get => phoneNumber; set { phoneNumber = value; } }
        public System.Net.Mail.MailAddress Mail { get => mail; set { mail = value; } }
        public BankAccount Bank { get => bank; set { bank = value; } }
        public bool CollectionClearance { get; set; }
        #endregion
        #region ctors
        public Host(int id, string first, string last, System.Net.Mail.MailAddress mail, Int32 phone, BankAccount bank, bool CollectionClearance=false)
        {
            hostKey = id;
            name = first;
            lastName = last;
            this.mail = mail;
            phoneNumber = phone;
            this.bank = bank;
            this.CollectionClearance = CollectionClearance;
        }

        public Host()
        {
            hostKey = 0;
            CollectionClearance = false;
            mail = null;
            Phone = 0;
            bank = null;
            name = "";
            lastName = "";
        }
        #endregion
        public override string ToString()
        {
            return "Owner id: " + hostKey + "Name: " + name + " " + lastName + "Phone number: " + phoneNumber +
                "Mail address: " + mail + "Bank Account: " + bank + "Collection permission: " + CollectionClearance;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class Host
    {
        #region fields
        private Int32 hostKey;
        private string name;
        private string lastName;
        private string mail;
        private BankAccount bank;
        #endregion
        #region properties
        public Int32 HostKey { get => hostKey; set { hostKey = value; } }
        public string Name { get => name; set { name = value; } }
        public string LastName { get => lastName; set { lastName = value; } }
        public BankAccount Bank { get => bank; set { bank = value; } }
        public bool CollectionClearance { get; set; }

        [XmlIgnore]
        public System.Net.Mail.MailAddress Mail { get => new System.Net.Mail.MailAddress(mail);   set { mail = value.Address; } }

        public string EmailSerializable { get => mail; set { mail = value; } }
        #endregion
        #region ctors
        public Host(int id, string first, string last, System.Net.Mail.MailAddress mail, Int32 phone, BankAccount bank, bool CollectionClearance=false)
        {
            hostKey = id;
            name = first;
            lastName = last;
            this.mail = mail.Address;
            this.bank = bank;
            this.CollectionClearance = CollectionClearance;
            bank = new BankAccount();
        }

        public Host()
        {
            
            
        }
        #endregion
        public override string ToString()
        {
            return "Owner id: " + hostKey.ToString("000000000") + "Name: " + name + " " + lastName +
                "Mail address: " + mail + "Bank Account: " + bank + "Collection permission: " + CollectionClearance+"\n";
        }

    }
}

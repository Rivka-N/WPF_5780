using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        private Int32 hostKey;
        private string name;
        private string lastName;
        private string mail;
        private Int32 phoneNumber;
        private BankAccount bank;
        public  bool CollectionClearance { get; set; }
        public Host() { }
        public Host(int id, string first, string last, string mail, Int32 phone, BankAccount bank, bool CollectionClearance=false)
        {
            hostKey = id;
            name = first;
            lastName = last;
            this.mail = mail;
            phoneNumber = phone;
            this.bank = bank;
            this.CollectionClearance = CollectionClearance;
        }
        public override string ToString()
        {
            return "Owner id: " + hostKey + "Name: " + name + " " + lastName + "Phone number: " + phoneNumber +
                "Mail address: " + mail + "Bank Account: " + bank + "Collection permission: " + CollectionClearance;
        }

    }
}

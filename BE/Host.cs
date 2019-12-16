using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        private int hostKey;
        private string name;
        private string lastName;
        private string mail;
        private int phoneNumber;
        private BankAccount bank;
        public  bool CollectionClearance { get; set; }
        public Host() { }
        public override string ToString()
        {
            return "Owner id: " + hostKey + "Name: " + name + " " + lastName + "Phone number: " + phoneNumber +
                "Mail address: " + mail + "Bank Account: " + bank + "Collection permission: " + CollectionClearance;
        }

    }
}

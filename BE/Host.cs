using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public int HostKey { get; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public int PhoneNumber { get; set; }
        public BankAccount Bank { get; set; }
        public bool CollectionClearance { get; set; }
        public Host() { }
        public override string ToString()
        {
            return "Owner id: " + HostKey + "Name: " + Name + " " + LastName + "Phone number: " + PhoneNumber +
                "Mail address: " + Mail + "Bank Account: " + Bank + "Collection permission: " + CollectionClearance;
        }

    }
}

using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class BankAccount
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public int BankAcountNumber { get; set; }
        public override string ToString()
        {
            return "Bank Name: " + BankName + "Bank Number: " + BankNumber + "Branch Number: " + BranchNumber + "BranchAddress: " +
                BranchCity + BranchAddress + "Bank Acount Number: " + BankAcountNumber;

        }

    }
}

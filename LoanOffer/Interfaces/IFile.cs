using LoanOffer.Moduls.Classes;

namespace LoanOffer.Interfaces
{
    public interface IFile
    {
        string ReadFromFile(string fileName);
        void WriteToFile(string json, string fileName);
        bool CheckIfFileExist(string str);
    }
}

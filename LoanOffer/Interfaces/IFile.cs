using LoanOffer.Moduls.Classes;

namespace LoanOffer.Interfaces
{
    public interface IFile
    {
        string ReadFromFile(string fileName);
        void WriteToFile(string giphyURL, string fileName);
        bool CheckIfFileExist(string str);
    }
}

namespace Heron_Cendre.Services
{
    public class CopierPhoto
    {
        public void CopierP(string filename,string sourcePath,string destination)
        {
            try
            {
                string filenameSource = Path.Combine(sourcePath,filename);
                string filenameTarget = Path.Combine(destination,filename);
                File.Copy(filenameSource,filenameTarget,true);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

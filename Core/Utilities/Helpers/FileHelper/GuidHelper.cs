namespace Core.Utilities.Helpers.FileHelper
{
    public class GuidHelper
    {
        public static string Create()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
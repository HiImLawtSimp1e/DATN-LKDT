using System.Text;

namespace shop.Infrastructure.Utilities;

public static class UtilityServices
{
    // This method will create a random string starts with the day it created in "yyMMdd" add 'length' character in Characters
    public static string RandomString(int length)
    {
        Random Random = new();
        string Characters = "0123456789qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";

        DateTime now = DateTime.Now;
        string dateString = now.ToString("yyMMdd");

        StringBuilder randomString = new();

        for (int i = 0; i < length; i++)
        {
            randomString.Append(Characters[Random.Next(Characters.Length)]);
        }

        return dateString + randomString.ToString();
    }

    //this method will create some random bullshit
    public static string GenerateCode(int length)
    {
        Random random = new();
        string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";


        StringBuilder randomString = new();

        for (int i = 0; i < length; i++)
        {
            randomString.Append(Chars[random.Next(Chars.Length)]);
        }
        return randomString.ToString();
    }

    // public static string GenerateUniqueCode(DbContext context, int length)
    // {
    //     string uniqueCode;
    //     bool existingPromotion;

    //     do
    //     {
    //         uniqueCode = GenerateCode(length);
    //         existingPromotion = context.Set<Promotion>().Any(p => p.PromotionCode.ToLower().Trim() == uniqueCode.ToLower());
    //     }
    //     while (existingPromotion);

    //     return uniqueCode;
    // }

}

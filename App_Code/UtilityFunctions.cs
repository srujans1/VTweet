using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for UtilityFunctions
/// </summary>
public static class UtilityFunctions
{
    public static string GenerateChar()
    {
        StringBuilder randomString = new StringBuilder();
        Random random = new Random();

        for (int i = 0; i <4; i++)
        {
            randomString.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65))).ToString());
        }

        return randomString.ToString();
    }
}
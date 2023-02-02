using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class crypter : MonoBehaviour
{
    //ме лни йнд
    #region Methods
    public static string ToHexString(string str)
    {
        var sb = new StringBuilder();
        var bytes = Encoding.UTF8.GetBytes(str);
        foreach (var t in bytes)
        {
            sb.Append(t.ToString("X2"));
        }

        return sb.ToString(); 
    }

    public static string GenerateId()
    {
        StringBuilder builder = new StringBuilder();
        Enumerable
           .Range(65, 26)
            .Select(e => ((char)e).ToString())
            .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
            .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
            .OrderBy(e => Guid.NewGuid())
            .Take(10)
            .ToList().ForEach(e => builder.Append(e));
        string id = builder.ToString();
        return id.ToUpper();
    }
    #endregion
}

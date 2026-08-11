namespace FoodFlow.Application.Common;

using System;
using System.Text;
using System.Reflection;

public class CursorQueryHelper<T> where T : class
{
    public static string GenerateQueryParams(T obj)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));

        StringBuilder queryParams = new StringBuilder();

        foreach (PropertyInfo property in typeof(T).GetProperties())
        {
            var value = property.GetValue(obj);
            if (value != null)
            {
                if (queryParams.Length > 0)
                {
                    queryParams.Append("&");
                }
                queryParams.AppendFormat("{0}={1}", property.Name, Uri.EscapeDataString(value.ToString()));
            }
        }

        return queryParams.ToString();
    }
}
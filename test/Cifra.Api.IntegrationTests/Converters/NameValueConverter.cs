using System;
using System.Collections.Generic;
using TechTalk.SpecFlow.Assist;

namespace Cifra.Api.IntegrationTests.Converters;

public class NameValueConverter : IValueRetriever
{
    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
    {
        if (!keyValuePair.Key.Equals("Name"))
        {
            return false;
        }

        return true;
    }

    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
    {
        if(keyValuePair.Value == "")
        {
            return " ";
        }
        return keyValuePair.Value;
    }
}
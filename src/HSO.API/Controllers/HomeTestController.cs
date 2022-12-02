using Microsoft.AspNetCore.Mvc;

namespace HSO.API.Controllers;

public class HomeTestController : ApiBaseController
{
    [HttpPost]
    public Task<ActionResult<string>> Post([FromBody] string data)
    {
        var resultParse = CustomParse(data);
        return Task.FromResult<ActionResult<string>>(resultParse);
    }
    
    public string CustomParse(string data)
    {
        var listOfList = new List<List<string>>();
        var dataArray = data.ToCharArray();
        const char splitChar = ',';
        var startChar = dataArray[0];
        
        var row =  new List<string>();
        var countSpout = 0;
        var currentValue = string.Empty;
        
        for (var i = 0; i < dataArray.Length; i++)
        {
            var dataValue = dataArray[i];

            if (dataValue == 13 || dataValue == 10)
                continue;
            
            if (dataValue == splitChar && currentValue.EndsWith(startChar)) {
                row.Add(currentValue);
                currentValue = string.Empty;
                countSpout = 0;
                continue;
            }
            
            if (dataValue == splitChar && !currentValue.Contains(startChar)) {
                row.Add(currentValue);
                currentValue = string.Empty;
                countSpout = 0;
                continue;
            }

            if (countSpout == 2)
            {
                row.Add(currentValue);
                currentValue = string.Empty;
            }
            
            if (dataValue ==  startChar) {
                countSpout++;
            }
            
            currentValue += dataValue;

            if (countSpout >= 3)
            {
                countSpout = 1;
                listOfList.Add(row);
                row = new List<string>();
            }

            if (dataArray.Length - 1 != i) 
                continue;
            
            row.Add(currentValue);
            listOfList.Add(row);
            row = new List<string>();
        }

        var result = string.Empty;
        foreach (var line in listOfList)
        {
            result = line.Aggregate(result, (current, column) => current + $"[{column.Replace(startChar, ' ').Trim()}] ");
            result += Environment.NewLine;
        }
        return result.Trim() ;
    }
}
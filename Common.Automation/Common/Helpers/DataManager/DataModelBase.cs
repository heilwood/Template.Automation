using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Common.Automation.Common.Helpers.DataManager
{
    public abstract class DataModelBase<T> where T : DataModelBase<T>
    {
        
        public string DataName { get; set; }
        protected abstract string JsonFilePath { get; }

        public virtual T SelectData(string name)
        {
            var folderPath = PathHelper.GetWorkingDirectory();
            var fullPath = Path.Combine(folderPath, JsonFilePath);
            var scriptTemplateJson = File.ReadAllText(fullPath, Encoding.UTF8);
            var template = JsonConvert.DeserializeObject<List<T>>(scriptTemplateJson);
            var data = template.FirstOrDefault(item => item.DataName.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            return data ?? throw new Exception($"Data with name {name} not found in {JsonFilePath}");
        }

    }
}

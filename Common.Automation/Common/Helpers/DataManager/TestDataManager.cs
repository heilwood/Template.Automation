namespace Common.Automation.Common.Helpers.DataManager
{
   public class TestDataManager<T> where T : DataModelBase<T>, new()
    {
        public T GetData(string name)
        {
            var data = new T().SelectData(name);
            return data;
        }
    }
}

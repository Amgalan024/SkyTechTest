namespace Services.SavedDataProvider
{
    public interface ISaveDataService 
    {
        T GetData<T>(string key);
        void SetData<T>(string key, T value);
        void DeleteData(string key);
        void DeleteAllData();
    }
}
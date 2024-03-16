using UnityEngine;

namespace Utils.SavedDataProvider
{
    public class PlayerPrefsDataService : ISaveDataService
    {
        public T GetData<T>(string key)
        {
            if (typeof(T) == typeof(float))
            {
                object data = PlayerPrefs.GetFloat(key);
                return (T) data;
            }

            if (typeof(T) == typeof(int))
            {
                object data = PlayerPrefs.GetInt(key);
                return (T) data;
            }

            if (typeof(T) == typeof(string))
            {
                object data = PlayerPrefs.GetString(key);
                return (T) data;
            }

            return default;
        }

        public void SetData<T>(string key, T value)
        {
            if (typeof(T) == typeof(float))
            {
                var data = (object) value;
                PlayerPrefs.SetFloat(key, (float) data);
            }

            if (typeof(T) == typeof(int))
            {
                var data = (object) value;
                PlayerPrefs.SetInt(key, (int) data);
            }

            if (typeof(T) == typeof(string))
            {
                var data = (object) value;
                PlayerPrefs.SetString(key, (string) data);
            }
        }

        public void DeleteData(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }

        public void DeleteAllData()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
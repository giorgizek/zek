using System;
using System.Collections.Generic;

namespace Zek.Model.DTO
{
    public interface IApiResponse
    {
        bool Success { get; set; }
        Dictionary<string, List<string>> Errors { get; set; }
    }

    public class ApiResponse : IApiResponse
    {
        public bool Success { get; set; }

        public Dictionary<string, List<string>> Errors { get; set; }


        public void AddError(Enum @enum) => AddError(string.Empty, @enum.ToString());
        public void AddError(string errorMessage) => AddError(string.Empty, errorMessage);
        public void AddError(string key, Enum @enum) => AddError(key, @enum.ToString());
        public void AddError(string key, string errorMessage)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (errorMessage == null)
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }

            var list = GetOrAddNode(key);
            list.Add(errorMessage);
        }
        public bool Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (Errors != null)
            {
                if (Errors.Remove(key))
                {
                    if (Errors.Count == 0)
                    {
                        Reset();
                    }

                    return true;
                }
            }

            return false;
        }

        private List<string> GetOrAddNode(string key)
        {
            Errors ??= new Dictionary<string, List<string>>();
            if (!Errors.TryGetValue(key, out var list))
            {
                list = new List<string>();
                Errors.Add(key, list);
            }

            return list;
        }

        public void Clear()
        {
            //Errors?.Clear();
            Reset();
        }

        private void Reset()
        {
            Errors = null;
        }
    }



    public interface IApiResponse<T> : IApiResponse
    {
        public T Value { get; set; }
    }

    public class ApiResponse<T> : ApiResponse, IApiResponse<T>
    {
        public ApiResponse() { }
        public ApiResponse(T value)
        {
            Value = value;
        }
        public T Value { get; set; }
    }
}

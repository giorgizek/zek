using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Zek.Model
{
    [Serializable]
    public class ModelStateResult<TModel> : ModelStateResult
    {
        public ModelStateResult(TModel value)
        {
            Value = value;
        }

        public ModelStateResult()
        {
        }


        public TModel Value { get; set; }
    }

    [Serializable]
    public class ModelStateResult :  ModelStateDictionary
    {
        public StatusCode StatusCode { get; set; }

        public void Add(string key, Enum @enum) => Add(key, @enum.ToString());
        public void Add(string key, string errorMessage)
        {
            AddModelError(key, errorMessage);

            if (StatusCode == StatusCode.OK)
                StatusCode = StatusCode.BadRequest;
        }
    }
}
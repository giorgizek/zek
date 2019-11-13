using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Zek.Extensions;

namespace Zek.Model
{
    public static class ModelStateResultExtensions
    {
        //public static void AddToModelState(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary viewState, ModelStateResult saveModelState) => saveModelState.AddToModelState(viewState);

        private static void AddToModelState(this ModelStateResult saveModelState, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary viewState)
        {
            if (saveModelState == null)
                throw new ArgumentNullException(nameof(saveModelState));
            if (viewState == null)
                throw new ArgumentNullException(nameof(viewState));

            foreach (var item in saveModelState)
            {
                foreach (var error in item.Value.Errors)
                {
                    viewState.AddModelError(item.Key, error.ErrorMessage);
                }
            }
        }

        private static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ToModelState(this ModelStateResult saveModelState)
        {
            var modelState = new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary();
            saveModelState.AddToModelState(modelState);
            return modelState;
        }

        public static IActionResult ToActionResult(this ModelStateResult modelStateResult, object content = null)
        {
            switch (modelStateResult.StatusCode)
            {
                case StatusCode.BadRequest:
                    return new BadRequestObjectResult(modelStateResult.ToModelState());

                case StatusCode.Forbidden:
                    return new ForbidResult();

                case StatusCode.NotFound:
                    return new NotFoundObjectResult(modelStateResult.ToModelState());

                case StatusCode.InternalServerError:
                    return new ObjectResult(modelStateResult.ToModelState()) { StatusCode = modelStateResult.StatusCode.ToInt32() };

                //case StatusCode.OK:
                default:
                    return content != null
                        ? new OkObjectResult(content)
                        : (IActionResult)new OkResult();
            }
        }
        public static IActionResult ToActionResult<TModel>(this ModelStateResult<TModel> modelStateResult)
        {
            switch (modelStateResult.StatusCode)
            {
                case StatusCode.BadRequest:
                    return new BadRequestObjectResult(modelStateResult.ToModelState());

                case StatusCode.Forbidden:
                    return new ForbidResult();

                case StatusCode.NotFound:
                    return new NotFoundObjectResult(modelStateResult.ToModelState());

                case StatusCode.InternalServerError:
                    return new ObjectResult(modelStateResult.ToModelState()) { StatusCode = modelStateResult.StatusCode.ToInt32() };

                //case StatusCode.OK:
                default:
                    return new OkObjectResult(modelStateResult.Value);
            }
        }
    }

    //public class ModelStateDictionary : ModelStateDictionary<int?>
    //{
    //    public ModelStateDictionary()
    //    {
    //    }

    //    public ModelStateDictionary(int? key) : base(key)
    //    {
    //    }
    //}


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

    public class ModelStateResult
    {
        public ModelStateResult()
        {
            StatusCode = StatusCode.OK;
        }



        private readonly Dictionary<string, ModelState> _innerDictionary = new Dictionary<string, ModelState>(StringComparer.OrdinalIgnoreCase);

        //public string Message { get; set; }

        //public TKey Key { get; set; }

        //public ICollection<string> Keys => _innerDictionary.Keys;
        //public ICollection<ModelState> Values => _innerDictionary.Values;


        //public int Count => _innerDictionary.Count;

        public StatusCode StatusCode { get; set; }
        public bool IsValid
        {
            get
            {
                return /*StatusCode == StatusCode.OK &&*/ _innerDictionary.Values.All(modelState => modelState.Errors.Count == 0);
            }
        }

        public ModelState this[string key]
        {
            get
            {
                _innerDictionary.TryGetValue(key, out var value);
                return value;
            }
            set => _innerDictionary[key] = value;
        }


        public IEnumerator<KeyValuePair<string, ModelState>> GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }

        //public void Add(string key, Exception exception)
        //{
        //    GetModelStateForKey(key).Errors.Add(exception);
        //}
        public void Add(string key, Enum @enum) => Add(key, @enum.ToString());
        public void Add(string key, string errorMessage)
        {
            GetModelStateForKey(key).Errors.Add(errorMessage);

            if (StatusCode == StatusCode.OK)
                StatusCode = StatusCode.BadRequest;
        }

        public bool Remove(string key)
        {
            var result = _innerDictionary.Remove(key);

            if (StatusCode == StatusCode.BadRequest && IsValid)
                StatusCode = StatusCode.OK;

            return result;
        }
        public void Clear()
        {
            _innerDictionary.Clear();

            if (StatusCode == StatusCode.BadRequest)
                StatusCode = StatusCode.OK;
        }

        private ModelState GetModelStateForKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (!_innerDictionary.TryGetValue(key, out var modelState))
            {
                modelState = new ModelState();
                this[key] = modelState;
            }

            return modelState;
        }
    }

    public class ModelState
    {
        public ModelErrorCollection Errors { get; } = new ModelErrorCollection();
    }

    public class ModelErrorCollection : Collection<ModelError>
    {
        //public void Add(Exception exception)
        //{
        //    Add(new ModelError(exception));
        //}

        public void Add(string errorMessage)
        {
            Add(new ModelError(errorMessage));
        }
    }

    public class ModelError
    {
        //public ModelError(Exception exception, string errorMessage = null)
        //    : this(errorMessage)
        //{
        //    Exception = exception ?? throw new ArgumentNullException(nameof(exception));
        //}

        public ModelError(string errorMessage)
        {
            ErrorMessage = errorMessage ?? string.Empty;
        }

        //public Exception Exception
        //{
        //    get;
        //}

        public string ErrorMessage
        {
            get;
        }
    }
}

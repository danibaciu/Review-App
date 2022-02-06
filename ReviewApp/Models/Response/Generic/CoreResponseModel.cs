using System;
namespace Models.Response.Generic
{
    public interface ICoreResponseModel
    {
        CoreResponseModel getSuccessResponse(string message, object data);
        CoreResponseModel getFailResponse(string message,object data);
    }

    public class CoreResponseModel : ICoreResponseModel
    {
        public int status { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public CoreResponseModel getSuccessResponse(string message, object data)
        {
            this.message = message;
            this.data = data;
            status = 200;
            success = true;
            return this;
        }

        public CoreResponseModel getFailResponse(string message,object data)
        {
            this.message = message;
            this.data = data;
            status = 400;
            success = false;
            return this;
        }

    }
}

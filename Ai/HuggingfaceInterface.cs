using System;
using System.Collections.Generic;
using System.Text;

namespace HuggingFaceApiClient
{
    public interface IHuggingFaceClient
    {
        public Task<string> Query(string query);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Ai
{
    public interface IHuggingFaceClient
    {
        public Task<string> Query(string query);
    }
}

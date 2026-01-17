using System;
using System.Collections.Generic;
using System.Text;

namespace Ai
{
    public interface HuggingfaceInterface
    {
        public Task<string> Query(string query);
    }
}

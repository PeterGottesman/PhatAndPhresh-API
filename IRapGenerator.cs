using System;
using PhatAndPhresh.Models;

namespace PhatAndPhresh
{
    public interface IRapGenerator
    {
        Rap Generate(int verse_count);
    }
}

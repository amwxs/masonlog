﻿namespace SharpMason.Extensions.Utils
{
    public static class GuidUtil
    {

        public static string NewGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}

namespace SharpMason.Core;

public class PackResult
{

    public string Code { get; set; }
    public string Error { get; set; }


    #region 静态简化构建方法

    public static PackResult Ok(string code = PackConst.OK)
    {
        return new PackResult { Code = code };
    }

    public static PackResult Fail(string code, string error = PackConst.Empty)
    {
        return new PackResult { Code = code, Error = error };
    }

    public static PackResult<T> Ok<T>(T data, string code = PackConst.OK)
    {
        return new PackResult<T> { Data = data, Code = code };
    }
    public static PackResult<T> Ok<T>(T data, Paging paging, string code = PackConst.OK)
    {
        return new PackResult<T> { Data = data, Paging = paging, Code = code };
    }

    public static PackResult<T> Fail<T>(string code, string error = PackConst.Empty)
    {
        return new PackResult<T> { Code = code, Error = error };
    }

    #endregion
    public bool IsOk()
    {
        return Code == PackConst.OK;
    }
}


public class PackResult<T> : PackResult
{
    public T Data { get; set; }
    public Paging Paging { get; set; }
}

public class Paging
{
    public int Total { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
    public Paging(int total, int pageSize, int pageIndex)
    {
        Total = total;
        PageSize = pageSize;
        PageIndex = pageIndex;
    }
}

public class PackConst
{
    public const string OK = "1000";
    public const string Empty = "";
}

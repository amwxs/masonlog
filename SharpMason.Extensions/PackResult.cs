namespace SharpMason.Extensions;

public class Pack
{
    public string? Code { get; set; }
    public string? Error { get; set; }

    #region 静态简化构建方法

    public static Pack Ok(string code = PackConst.Ok)
    {
        return new Pack { Code = code };
    }

    public static Pack<T> Ok<T>(T data, string code = PackConst.Ok)
    {
        return new Pack<T> { Data = data, Code = code };
    }
    public static Pack<T> Ok<T>(T data, Pager paging, string code = PackConst.Ok)
    {
        return new Pack<T> { Data = data, Pager = paging, Code = code };
    }
    public static Pack Fail(string code, string error = PackConst.Empty)
    {
        return new Pack { Code = code, Error = error };
    }
    public static Pack<T> Fail<T>(string code, string error = PackConst.Empty)
    {
        return new Pack<T> { Code = code, Error = error };
    }

    #endregion
    public virtual bool IsOk()
    {
        return Code == PackConst.Ok;
    }
}

public class Pack<T> : Pack
{
    public T? Data { get; set; }
    public Pager? Pager { get; set; }
}


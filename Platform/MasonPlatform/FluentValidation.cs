using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SharpMason.Extensions;
using SharpMason.Extensions.Utils;
using System.Reflection;

namespace MasonPlatform
{
    public static class FluentValidation
    {
        private const string invalidCode = "10001";
        /// <summary>
        /// 验证返回统一的格式
        /// </summary>
        /// <param name="mvcBuilder"></param>
        public static IMvcBuilder AddFluentValidation(this IMvcBuilder mvcBuilder)
        {
            var assembly = Assembly.Load("MasonPlatform.Models");

            //注册FluentValidation
            mvcBuilder.Services.AddValidatorsFromAssembly(assembly);
            //模型验证 验证失败统一返回格式
            mvcBuilder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {

                    var msg = new Dictionary<string, string>();
                    foreach (var key in context.ModelState.Keys)
                    {
                        var state = context.ModelState[key];
                        if (state !=null)
                        {
                            var errors = string.Join(",", state.Errors.Select(x => x.ErrorMessage).ToList());
                            msg.Add(key, errors);
                        }

                    }
                    return new ObjectResult(Pack.Fail(invalidCode, msg.ToJson()));
                };
            });

            return mvcBuilder;
        }
    }
}

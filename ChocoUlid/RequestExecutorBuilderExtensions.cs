using ChocoUlid;
using HotChocolate.Data.Filters;
using HotChocolate.Execution.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder AddUlidType(this IRequestExecutorBuilder builder)
        {
            builder.AddType<UlidType>()
                .AddType<UlidOperationFilterInputType>()
                .BindRuntimeType<Ulid, UlidType>()
                .AddConvention<IFilterConvention, UlidFilterConvention>();
            return builder;
        }
    }
}

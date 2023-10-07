using HotChocolate.Data.Filters;
using System;

namespace ChocoUlid;

public class UlidFilterConvention : FilterConventionExtension
{
    protected override void Configure(IFilterConventionDescriptor descriptor)
    {
        descriptor.BindRuntimeType<Ulid, UlidOperationFilterInputType>();
        base.Configure(descriptor);
    }
}

using System;
using HotChocolate.Data.Filters;

namespace ChocoUlid
{
    public class UlidOperationFilterInputType : ComparableOperationFilterInputType<Ulid>
    {
        protected override void Configure(IFilterInputTypeDescriptor descriptor)
        {
            descriptor.Name("UlidOperationFilterInputType");
            descriptor.Operation(DefaultFilterOperations.Equals)
                .Type<UlidType>();
            descriptor.Operation(DefaultFilterOperations.NotEquals)
                .Type<UlidType>();
        }
    }
}

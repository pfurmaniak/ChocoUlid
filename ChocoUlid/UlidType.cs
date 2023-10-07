using HotChocolate.Language;
using HotChocolate.Types;
using System;

namespace ChocoUlid
{
    public class UlidType : ScalarType<Ulid, StringValueNode>
    {
        public UlidType() : base(nameof(Ulid)) { }

        protected override bool IsInstanceOfType(StringValueNode valueSyntax) => Ulid.TryParse(valueSyntax.Value, out var _);
        protected override bool IsInstanceOfType(Ulid runtimeValue) => true;

        public override IValueNode ParseResult(object resultValue)
        {
            if (resultValue is null)
            {
                return NullValueNode.Default;
            }

            if (resultValue is string s)
            {
                return new StringValueNode(s);
            }

            if (resultValue is Ulid ulid)
            {
                return ParseValue(ulid);
            }

            throw new SerializationException($"{Name} cannot parse the provided value. The provided value is not a valid Ulid.", this);
        }

        protected override Ulid ParseLiteral(StringValueNode valueSyntax)
        {
            if (Ulid.TryParse(valueSyntax.Value, out var ulid))
            {
                return ulid;
            }

            throw new SerializationException($"{Name} cannot parse the provided value. The provided value is not a valid Ulid.", this);
        }

        protected override StringValueNode ParseValue(Ulid runtimeValue) => new(runtimeValue.ToString());

        public override bool TryDeserialize(object resultValue, out object runtimeValue)
        {
            if (resultValue is null)
            {
                runtimeValue = null;
                return true;
            }

            if (resultValue is string s && Ulid.TryParse(s, out var ulid))
            {
                runtimeValue = ulid;
                return true;
            }

            if (resultValue is Ulid)
            {
                runtimeValue = resultValue;
                return true;
            }

            runtimeValue = null;
            return false;
        }

        public override bool TrySerialize(object runtimeValue, out object resultValue)
        {
            if (runtimeValue is null)
            {
                resultValue = null;
                return true;
            }

            if (runtimeValue is Ulid ulid)
            {
                resultValue = ulid.ToString();
                return true;
            }

            resultValue = null;
            return false;
        }
    }
}

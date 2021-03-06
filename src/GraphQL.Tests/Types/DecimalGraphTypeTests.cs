﻿using GraphQL.Types;
using Shouldly;
using Xunit;

namespace GraphQL.Tests.Types
{
    public class DecimalGraphTypeTests
    {
        private DecimalGraphType type = new DecimalGraphType();

        [Fact]
        public void coerces_null_to_null()
        {
            type.ParseValue(null).ShouldBe(null);
        }

        [Fact]
        public void coerces_integer_to_decimal()
        {
            type.ParseValue(0).ShouldBe((decimal)0);
        }

        [Fact]
        public void coerces_invalid_string_to_null()
        {
            type.ParseValue("abcd").ShouldBe(null);
        }

        [Fact]
        public void coerces_numeric_string_to_decimal()
        {
            type.ParseValue("12345").ShouldBe((decimal)12345);
        }
    }
}

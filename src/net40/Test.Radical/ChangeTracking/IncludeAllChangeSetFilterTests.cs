﻿namespace Test.Radical.ChangeTracking
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;
    using Topics.Radical.ChangeTracking;
    using Topics.Radical.ComponentModel.ChangeTracking;
    using SharpTestsEx;

    [TestClass]
    public class IncludeAllChangeSetFilterTests
    {
        [TestMethod]
        [TestCategory( "ChangeTracking" )]
        public void filter_is_singleton()
        {
            var expected = IncludeAllChangeSetFilter.Instance;
            var actual = IncludeAllChangeSetFilter.Instance;

            actual.Should().Be.EqualTo( expected );
        }

        [TestMethod]
        [TestCategory( "ChangeTracking" )]
        public void filter_shouldInclude_is_always_true()
        {
            var iChange = MockRepository.GenerateStub<IChange>();
            var target = IncludeAllChangeSetFilter.Instance;

            var actual = target.ShouldInclude( iChange );
            actual.Should().Be.True();
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        [TestCategory( "ChangeTracking" )]
        public void filter_argumentNullException_on_shouldInclude_null_iChange_reference()
        {
            var target = IncludeAllChangeSetFilter.Instance;
            target.ShouldInclude( null );
        }
    }
}
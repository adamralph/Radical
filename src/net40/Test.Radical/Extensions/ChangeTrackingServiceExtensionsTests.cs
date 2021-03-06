﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Topics.Radical.ChangeTracking;

namespace Test.Radical.Extensions
{
    [TestClass]
    public class ChangeTrackingServiceExtensionsTests
    {
        [TestMethod]
        [TestCategory("ChangeTracking")]
        public void isPropertyValueChanged_returns_true_if_property_value_is_changed()
        {
            var memento = new ChangeTrackingService();

            var person = new ChangeTracking.Person(memento);
            person.SetInitialPropertyValue(() => person.FirstName, "Mauro");

            person.FirstName = "Another";

            var actual = person.IsPropertyValueChanged(p => p.FirstName);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        [TestCategory("ChangeTracking")]
        public void isPropertyValueChanged_returns_false_if_property_value_is_unchanged()
        {
            var memento = new ChangeTrackingService();

            var person = new ChangeTracking.Person(memento);
            person.SetInitialPropertyValue(() => person.FirstName, "Mauro");

            person.FirstName = "Another";
            person.FirstName = "Mauro";

            var actual = person.IsPropertyValueChanged(p => p.FirstName);

            Assert.IsFalse(actual);
        }
    }
}

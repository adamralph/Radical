﻿//extern alias tpx;

namespace Test.Radical.Observers
{
    using System;
    using System.ComponentModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;
    using Topics.Radical;
    using Topics.Radical.Observers;

    [TestClass]
    public class PropertyChangedMonitorTests
    {
        class TestStub : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged( String name )
            {
                if( this.PropertyChanged != null )
                {
                    this.PropertyChanged( this, new PropertyChangedEventArgs( name ) );
                }
            }

            private String _value = null;
            public String Value
            {
                get { return this._value; }
                set
                {
                    if( value != this.Value )
                    {
                        this._value = value;
                        this.OnPropertyChanged( "Value" );
                    }
                }
            }

            private readonly Observable<String> _text = new Observable<String>();
            public Observable<String> Text
            {
                get { return this._text; }
            }
        }

        [TestMethod]
        public void propertyChangedMonitor_Observe_using_clr_property_should_behave_as_expected()
        {
            var expected = 1;
            var actual = 0;

            var stub = new TestStub();
            var target = new PropertyChangedMonitor<TestStub>( stub );
            target.Observe( s => s.Value );
            target.Changed += ( s, e ) => actual++;

            stub.Value = "Hello!";

            actual.Should().Be.EqualTo( expected );
        }

        [TestMethod]
        public void propertyChangedMonitor_Observe_using_observable_property_should_behave_as_expected()
        {
            var expected = 1;
            var actual = 0;

            var stub = new TestStub();
            var target = new PropertyChangedMonitor<TestStub>( stub );
            target.Observe( stub.Text );
            target.Changed += ( s, e ) => actual++;

            stub.Text.Value = "Hello!";

            actual.Should().Be.EqualTo( expected );
        }

        [TestMethod]
        public void propertyChangedMonitor_StopObserving_using_observable_property_should_behave_as_expected()
        {
            var expected = 2;
            var actual = 0;

            var stub = new TestStub();

            var target = new PropertyChangedMonitor<TestStub>( stub );
            target.Observe( stub.Text );
            target.Changed += ( s, e ) => actual++;

            stub.Text.Value = "Hello!";
            stub.Text.Value = "Should raise...";

            target.StopObserving( stub.Text );
            stub.Text.Value = "should not raise...";

            actual.Should().Be.EqualTo( expected );
        }

        [TestMethod]
        public void propertyChangedMonitor_ForAllProperties_using_clr_property_should_behave_as_expected()
        {
            var expected = 1;
            var actual = 0;

            var stub = new TestStub();
            var target = new PropertyChangedMonitor( stub );
            target.Changed += ( s, e ) => actual++;

            stub.Value = "Hello!";

            actual.Should().Be.EqualTo( expected );
        }
    }
}

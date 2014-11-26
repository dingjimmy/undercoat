﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Primer.Tests
{
    [TestClass]
    public class ViewModelBaseUnitTests
    {

        [TestMethod]
        [TestCategory("ViewModelBase")]
        public void SetProperty_WhenCurrentAndProposedAreDifferent_SetsCurrentToProposedValue()
        {

            // Arrange
            var viewModelBase = Mock.Of<ViewModelBase>();
            Mock.Get(viewModelBase).CallBase = true;
            var currentValue = "Current Value";


            // Action
            viewModelBase.SetProperty(() => viewModelBase.DisplayName, ref currentValue, "Proposed Value");


            // Assert
            Assert.AreEqual("Proposed Value", currentValue);

        }


        [TestMethod]
        [TestCategory("ViewModelBase")]
        public void SetProperty_WhenCurrentAndProposedAreDifferent_RaisesPropertyChangedEvent()
        {

            // Arrange
            var viewModelBase = Mock.Of<ViewModelBase>();
            Mock.Get(viewModelBase).CallBase = true;
            var currentValue = "Current Value";


            // Action
            viewModelBase.SetProperty(() => viewModelBase.DisplayName, ref currentValue, "Proposed Value");


            // Assert
            Mock.Get(viewModelBase).Verify((vm) => vm.RaisePropertyChanged(It.IsAny<object>(), It.IsAny<string>()));

        }



        [TestMethod]
        [TestCategory("ViewModelBase")]
        public void SetProperty_WhenCurrentAndProposedAreDifferent_BroadcastsPropertyChangedMessage()
        {

            // Arrange
            var viewModelBase = Mock.Of<ViewModelBase>();
            Mock.Get(viewModelBase).CallBase = true;
            var currentValue = "Current Value";


            // Action
            viewModelBase.SetProperty(() => viewModelBase.DisplayName, ref currentValue, "Proposed Value");


            // Assert
            Mock.Get(viewModelBase).Verify((vm) => vm.Channel.Broadcast(It.IsAny<PropertyChangedMessage>()));

        }

    }
}

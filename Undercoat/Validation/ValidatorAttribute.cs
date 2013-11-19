﻿// Copyright (c) James Dingle

using System;

namespace Primer.Validation
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple=true, Inherited=false)]
    public abstract class ValidatorAttribute: Attribute, IComparable<ValidatorAttribute>
    {


#region IsValid Property


        // property backing field
        private bool _IsValid;

        /// <summary>
        /// Gets a value that indicates whether the property this attribute adorns has passed validation.
        /// </summary>
        /// <returns>
        /// True if the property has passed validation; otherwise false.
        /// </returns>
        public bool IsValid
        {
            get { return _IsValid; }
        }


#endregion


#region Message Property


        // property backing field
        string _Message;

        /// <summary>
        /// Gets a string that describes the reason for the property this attribute adorns failing validation. If validation has passed then <see cref="String.Empty"/> is returned.
        /// </summary>
        public string Message
        {
            get { return _Message; }
        }


#endregion


#region ProcessingOrder Property


        // property backing field
        private byte _ProcessingOrder;

        /// <summary>
        /// Gets or Sets the order in which the validation attribute is processed, in relation to other validation attributes that may be adorning the same property.
        /// </summary>
        public byte ProcessingOrder
        {
            get { return _ProcessingOrder; }
            set { _ProcessingOrder = value; }
        }


#endregion


#region Methods


        /// <summary>
        /// Validates the value against a pre-determned set of rules.
        /// </summary>
        /// <param name="value"></param>
        public abstract void Validate(object value);



        /// <summary>
        /// Sets the validation state of this Attribute.
        /// </summary>
        /// <param name="isValid">Sets the value of the <see cref="ValidatorAttribute.IsValid"/> property.</param>
        /// <param name="message">Sets the value of the <see cref="ValidatorAttribute.Message"/> property.</param>
        protected void SetState(bool isValid, string message)
        {
            _IsValid = isValid;
            _Message = message;
        }


        
        /// <summary>
        /// Compares the processing order of this attribute, against another. Used by a ViewModel to sort attrubutes for a particular property into ascending order.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(ValidatorAttribute other)
        {
            return _ProcessingOrder.CompareTo(other._ProcessingOrder);
        }


#endregion

    }

}

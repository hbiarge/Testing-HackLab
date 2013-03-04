// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinorThanAttribute.cs" company="Acheve Solutions">
//   Copyright (c) Hugo Biarge. Todos los derechos reservados.
// </copyright>
// <summary>
//   Defines the MinorThanAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Acheve.UI.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Property)]
    public class MinorThanAttribute : ValidationAttribute, IClientValidatable
    {
        public MinorThanAttribute(string otherProperty)
            : base("{0} debe ser menor que {1}")
        {
            if (otherProperty == null)
            {
                throw new ArgumentNullException("otherProperty");
            }

            this.OtherProperty = otherProperty;
        }

        public string OtherProperty { get; private set; }

        public string OtherPropertyDisplayName { get; internal set; }

        public static string FormatPropertyForClientValidation(string property)
        {
            if (property == null)
            {
                throw new ArgumentException("property");
            }

            return "*." + property;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                this.ErrorMessageString,
                new object[] { name, (this.OtherPropertyDisplayName ?? this.OtherProperty) });
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata, ControllerContext context)
        {
            if (metadata.ContainerType != null && this.OtherPropertyDisplayName == null)
            {
                this.OtherPropertyDisplayName =
                       ModelMetadataProviders.Current.GetMetadataForProperty(
                          () => metadata.Model, metadata.ContainerType, this.OtherProperty).GetDisplayName();
            }

            yield return
                new ModelClientValidationEqualToRule(
                    this.FormatErrorMessage(metadata.GetDisplayName()),
                    CompareAttribute.FormatPropertyForClientValidation(this.OtherProperty));
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (property == null)
            {
                return
                    new ValidationResult(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "No se ha encontrado la propiedad especificada: {0}",
                            new object[] { this.OtherProperty }));
            }

            var objA = value as IComparable;
            var objB = property.GetValue(validationContext.ObjectInstance, null);

            if (objA == null || objB == null)
            {
                return
                    new ValidationResult(
                        string.Format(
                            CultureInfo.CurrentCulture,
                            "La propiedad {0} debe implementar la interfaz IComparable.",
                            new object[] { validationContext.DisplayName }));
            }

            if (objA.CompareTo(objB) < 0)
            {
                return null;
            }

            if (this.OtherPropertyDisplayName == null)
            {
                this.OtherPropertyDisplayName =
                       ModelMetadataProviders.Current.GetMetadataForProperty(
                           () => validationContext.ObjectInstance,
                           validationContext.ObjectType,
                           this.OtherProperty).GetDisplayName();
            }

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
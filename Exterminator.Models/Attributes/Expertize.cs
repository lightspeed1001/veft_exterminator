using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Exterminator.Models.InputModels;

namespace Exterminator.Models.Attributes
{
    //TODO ?????????
    public class Expertize : ValidationAttribute
    {
        private string[] _expertize;

        public Expertize(string[] expertize)
        {
            _expertize = expertize;
        }

        protected override ValidationResult IsValid(
            object value, ValidationContext validationContext)
        {
            var ghostBuster = (GhostbusterInputModel)validationContext.ObjectInstance;

            if (!_expertize.Contains(ghostBuster.Expertize))
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }


        public string GetErrorMessage()
        {
            var availableExpertize = String.Join(", ", _expertize);
            return $"Expertize can only be one of the following expertize: {availableExpertize}.";
        }

    }
}
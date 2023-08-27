﻿using EntityLayer.Concretes;
using FluentValidation;

namespace BusinessLayer.Validators
{
    public class TripValidator : AbstractValidator<Trip>
    {
        public TripValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0).NotEmpty().NotNull();
            RuleFor(x => x.PlannedDate).GreaterThan(DateTime.Now).NotEmpty().NotNull();
            RuleFor(x => x.Title).MinimumLength(5).NotEmpty().NotNull();
            RuleFor(x => x.Limit).GreaterThanOrEqualTo(0).NotEmpty().NotNull();
        }
    }
}
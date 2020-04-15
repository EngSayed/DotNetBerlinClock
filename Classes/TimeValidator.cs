using BerlinClock.Interfaces;
using FluentValidation;

namespace BerlinClock.Classes
{
    internal class TimeValidator : AbstractValidator<ITime>
    {
        public TimeValidator()
        {
            RuleFor(time => time.Hours).InclusiveBetween(0, 24);
            RuleFor(time => time.Minutes).InclusiveBetween(0, 59);
            RuleFor(time => time.Seconds).InclusiveBetween(0, 59);
            RuleFor(time => time.Seconds).Equal(0).When(time => time.Hours == 24).WithMessage("Seconds must be '0' when hours '24'");
            RuleFor(time => time.Minutes).Equal(0).When(time => time.Hours == 24).WithMessage("Minutes must be '0' when hours '24'");
        }
    }
}
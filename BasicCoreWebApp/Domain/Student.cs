using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicCoreWebApp.Domain
{
    public class Student
    {
        public const int NameMaxLength = 60;
        public const int MinAge = 5;

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }

        //There is no public constructor
        private Student() { }

        //The only way to create a new instance of this entity is using this method. I can ensure that this student will be validated, and my app won't be in an inconsistent state at any time.
        public static Student Create(string name, int age)
        {
            Validate(name, age);

            return new Student
            {
                Name = name,
                Age = age
            };
        }

        private static void Validate(string name, int age)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException("Student Name can't be null or empty");
            }
            if (name.Length > NameMaxLength)
            {
                throw new DomainException($"Student Name length can't be greater than {NameMaxLength}");
            }
            if (age < MinAge)
            {
                throw new DomainException($"Student Age must be equal or greater than {MinAge}");
            }
        }
    }
}

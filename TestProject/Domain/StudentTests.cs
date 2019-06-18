using Domain;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject.Domain
{
    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void When_creating_a_student_with_all_needed_parameters_Should_return_a_valid_student()
        {
            //arrange
            var age = 15;
            var name = "Test";

            //act
            var student = Student.Create(name, age);

            //assert
            student.Name.Should().Be(name);
            student.Age.Should().Be(age);
        }

        [TestMethod]
        public void When_creating_a_student_with_empty_name_Should_return_an_exception()
        {
            //arrange
            var age = 15;
            var name = "";

            //act
            Action createStudent = () => Student.Create(name, age);

            //assert
            createStudent.Should().Throw<DomainException>();
        }
    }
}

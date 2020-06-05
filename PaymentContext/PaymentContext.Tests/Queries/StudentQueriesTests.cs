using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentQueriesTests
    {
        private readonly IList<Student> _students;

        public StudentQueriesTests()
        {
            for (int i = 0; i < 5; i++)
            {
                _students.Add(
                    new Student(
                        new Name("Aluno", i.ToString()),
                        new Document("111111111" + i.ToString(), EDocumentType.CPF),
                        new Email(i.ToString() + "@email.com"))
                );
            }
        }

        [TestMethod]
        public void ShouldReturnNullWHenDocumentNoExists()
        {
            var exp = StudentQueries.GetStudent("564654654");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, student);
        }

        [TestMethod]
        public void ShouldReturnNullWHenDocumentExists()
        {
            var exp = StudentQueries.GetStudent("564654654");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(null, student);

        }
    }
}
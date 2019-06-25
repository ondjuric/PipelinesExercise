using NUnit.Framework;
using PipelinesExercise;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            new DoEverything().MakeAllTheViewModels("filename", "username", "password");
        }
    }
}
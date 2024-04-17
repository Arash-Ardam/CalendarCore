using System.Data;

namespace DataStructures.Tests.AffectedByDateCollection
{
    public class UnitTest1
    {
        //[Fact]
        //public void Test1()
        //{
        IAffectedByDateCollection<int> myStructure = new AffectedByDateCollection<int>();
        //    // Arrange
        //    // Sample Date:
        //    myStructure.Add(DateTime.Parse("2024-01-01"), 1);
        //    myStructure.Add(DateTime.Parse("2024-03-01"), 2);
        //    myStructure.Add(DateTime.Parse("2024-05-15"), 3);

        //    // Act
        //    myStructure.Get(DateTime.Parse("2023-01-01")); // => NULL => OutOfRangeException
        //    myStructure.Get(DateTime.Parse("2024-01-01")); // => 1 (Inculsive)
        //    myStructure.Get(DateTime.Parse("2024-01-02")); // => 1
        //    myStructure.Get(DateTime.Parse("2024-03-01")); // => 2 (exclusive)
        //    myStructure.Get(DateTime.Parse("2024-03-02")); // => 2
        //    myStructure.Get(DateTime.Parse("2024-05-20")); // => 3
        //     
        //   myStructure.Remove(DateTime.Parse("2023-01-01"))   

        //    myStructure.Modify(DateTime.Parse("2023-01-01") , 4) // => "Modified"
        //    myStructure.Get(DateTime.Parse("2023-01-01")) => 4
        //    myStruncure.Add("2024-01-15") => OutOfRangeException


        //}

        [Fact]
        public void Test2()
        {
            IAffectedByDateCollection<int> myStructure = ArrangeData();

            var isException = false;

            try
            {
                myStructure.Get(DateTime.Parse("2023-01-01")); // => NULL => OutOfRangeException
            }
            catch (ArgumentOutOfRangeException)
            {
                isException = true;
            }

            //Assert

            Assert.True(isException);
        }

        [Fact]
        public void Test3()
        {
            IAffectedByDateCollection<int> myStructure = ArrangeData();

            var expected = 1;

            var actual = myStructure.Get(DateTime.Parse("2024-01-01"));

            Assert.Equal(expected, actual);
            
        }

        [Theory]
        [InlineData("2024-01-02")]
        [InlineData("2024-01-01")]
        public void Test4(string date)
        {

            IAffectedByDateCollection<int> myStructure = ArrangeData();

            var expected = 1;

            var actual = myStructure.Get(DateTime.Parse(date));

            Assert.Equal(expected, actual);

        }


        [Theory]
        [InlineData("2024-03-01")]
        [InlineData("2024-03-02")]
        public void Test5(string date)
        {
            IAffectedByDateCollection<int> myStructure = ArrangeData();

            var expected = 2;

            var actual = myStructure.Get(DateTime.Parse(date));

            Assert.Equal(expected, actual);
            
        }

        [Theory]
        [InlineData("2024-05-15")]
        [InlineData("2024-05-21")]
        public void Test6(string date)
        {
            IAffectedByDateCollection<int> myStructure = ArrangeData();

            var expected = 3;

            var actual = myStructure.Get(DateTime.Parse(date));

            Assert.Equal(expected, actual);

        }

        [Theory]
        [InlineData("2024-05-15")]
        public void Test7(string date)
        {
            IAffectedByDateCollection<int> myStructure = ArrangeData();

            myStructure.Remove(DateTime.Parse(date));

            var expected = 2;

            Assert.Equal(expected,myStructure.Get(DateTime.Parse(date)));

        }

        [Theory]
        [InlineData("2024-05-15",4)]
        public void Test8(string date,int value)
        {
            IAffectedByDateCollection<int> myStructure = ArrangeData();

            myStructure.Modify(DateTime.Parse(date),value);

            var expected = 4;

            Assert.Equal(expected, myStructure.Get(DateTime.Parse(date)));

        }

        [Theory]
        [InlineData("2024-01-15",7)]
        [InlineData("2024-03-15",8)]
        public void Test9(string date,int value)
        {
            IAffectedByDateCollection<int> myStructure = ArrangeData();

            var isExeption = false;

            try
            {
                myStructure.Add(DateTime.Parse(date),value);
            }
            catch (ArgumentOutOfRangeException)
            {

                isExeption = true;
            }


            Assert.True(isExeption);

        }

        [Theory]
        [InlineData("2024-04-15", 7)]
        [InlineData("2024-03-15", 8)]
        public void Test10(string date, int value)
        {
            IAffectedByDateCollection<int> myStructure = ArrangeData();

            var isExeption = false;
            try
            {
                myStructure.Add(DateTime.Parse(date), value);
            }
            catch (ArgumentOutOfRangeException)
            {
                isExeption = true;
            }

            Assert.True(isExeption);

        }

        [Theory]
        [InlineData("2024-01-01", 7)]
        [InlineData("2024-03-01", 8)]
        public void Test11(string date, int value)
        {
            IAffectedByDateCollection<int> myStructure = ArrangeData();


            var isExeption = false;
            try
            {
                myStructure.Add(DateTime.Parse(date), value);
            }
            catch (DuplicateNameException)
            {
                isExeption = true;
            }

            Assert.True(isExeption);
        }

        private static IAffectedByDateCollection<int> ArrangeData()
        {
            IAffectedByDateCollection<int> myStructure = new AffectedByDateCollection<int>();
            // Arrange
            // Sample Date:
            myStructure.Add(DateTime.Parse("2024-01-01"), 1);
            myStructure.Add(DateTime.Parse("2024-03-01"), 2);
            myStructure.Add(DateTime.Parse("2024-05-15"), 3);
            return myStructure;
        }
    }

   
}
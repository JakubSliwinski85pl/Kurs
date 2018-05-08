using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TestKalkulatora;

namespace TestKalkulatora_test
{
    public class Class1
    {
        [Fact]
        public void SchouldReturnSumOfImputsForAdd()
        {
            //arrange
            var x = 5;
            var y = 10;
            var calc = new TestKalkulatora.ExampleCalculator();
            //act
            var result = calc.Add(x, y);
            //assert
            Assert.Equal(15, result);
        }

        [Fact]
        public void SchouldReturnSubtractionOfImputsForSubtract()
        {
            //arrange
            var x = 10;
            var y = 5;
            var calc = new TestKalkulatora.ExampleCalculator();
            //act
            var result = calc.Subtract(x, y);
            //assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void SchouldReturnMultiplicationOfImputsForMultiply()
        {
            //arrange
            var x = 10;
            var y = 5;
            var calc = new TestKalkulatora.ExampleCalculator();
            //act
            var result = calc.Multiply(x, y);
            //assert
            Assert.Equal(50, result);
        }

        [Fact]
        public void SchouldReturnDivisionOfImputsForDivide()
        {
            //arrange
            var x = 10;
            var y = 5;
            var calc = new TestKalkulatora.ExampleCalculator();
            //act
            var result = calc.Divide(x, y);
            //assert
            Assert.Equal(2, result);
        }


    }
}

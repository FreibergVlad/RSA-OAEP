using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using rsa.util;
using System.Numerics;

namespace RSATest.util
{
    /// <summary>
    ///     Tests for <see cref="PrimeGenerator"/>
    /// </summary>
    [TestClass]
    public class PrimeGeneratorTest
    {
        private PrimeGenerator primeGenerator;

        // First prime numbers between 257 and 409
        private int[] firstPrimes = { 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347,
                                        349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409 };

        // Bell's prime numbers
        private BigInteger[] bellsPrimes = { 877, 27644437, BigInteger.Parse("35742549198872617291353508656626642567"),
                                            BigInteger.Parse("359334085968622831041960188598043661065388726959079837") };

        // Fibonacci prime numbers
        private BigInteger[] fibonacciPrimes = { 1597, 28657, 514229, 433494437, 2971215073, 99194853094755497,
                                                BigInteger.Parse("1066340417491710595814572169"),
                                                BigInteger.Parse("19134702400093278081449423917") };
        // First composite numbers
        private int[] composits = { 9, 15, 25, 21, 35, 49, 27, 45, 63, 81, 33, 55, 77, 99, 121, 39, 65, 91, 117, 143,
                                    169, 45, 75, 105, 135, 165, 195, 225, 51, 85, 119, 153, 187, 221, 255, 289, 57, 95,
                                    133, 171, 209, 247, 285, 323, 361, 63, 105, 147, 189, 231, 273, 315, 357, 399, 441 };

        [TestInitialize]
        public void SetUp()
        {
            primeGenerator = new PrimeGenerator();
        }

        
        [TestMethod]
        public void IsPrime()
        {
            foreach (int prime in firstPrimes)
                Assert.AreEqual(true, primeGenerator.IsPrime(prime));
            foreach (BigInteger prime in bellsPrimes)
                Assert.AreEqual(true, primeGenerator.IsPrime(prime));
            foreach (BigInteger prime in fibonacciPrimes)
                Assert.AreEqual(true, primeGenerator.IsPrime(prime));
            foreach (int composite in composits)
                Assert.AreEqual(false, primeGenerator.IsPrime(composite));
        }
    }
}

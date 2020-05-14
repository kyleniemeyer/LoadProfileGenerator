﻿using System.Threading;
using Automation;
using Common.Tests;
using JetBrains.Annotations;
using NUnit.Framework;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.FullCalc {
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class HouseTestsShort : UnitTestBaseClass {
        [Fact]
        [Category(UnitTestCategories.FullCalcHouses1)]
        public void StartHouse00() {
            FullCalculationStarter.StartHouse(0);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses1)]
        public void StartHouse01() {
            FullCalculationStarter.StartHouse(1);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses1)]
        public void StartHouse02() {
            FullCalculationStarter.StartHouse(2);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses1)]
        public void StartHouse03() {
            FullCalculationStarter.StartHouse(3);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses1)]
        public void StartHouse04() {
            FullCalculationStarter.StartHouse(4);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses1)]
        public void StartHouse05() {
            FullCalculationStarter.StartHouse(5);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses1)]
        public void StartHouse06() {
            FullCalculationStarter.StartHouse(6);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses1)]
        public void StartHouse07() {
            FullCalculationStarter.StartHouse(7);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses1)]
        public void StartHouse08() {
            FullCalculationStarter.StartHouse(8);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses1)]
        public void StartHouse09() {
            FullCalculationStarter.StartHouse(9);
        }

        [Fact]
        [Trait("Category", UnitTestCategories.FullCalcHouses2)]
        public void StartHouse10() {
            FullCalculationStarter.StartHouse(10);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses2)]
        public void StartHouse11() {
            FullCalculationStarter.StartHouse(10);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses2)]
        public void StartHouse12() {
            FullCalculationStarter.StartHouse(11);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses2)]
        public void StartHouse13() {
            FullCalculationStarter.StartHouse(12);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses2)]
        public void StartHouse14() {
            FullCalculationStarter.StartHouse(13);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses2)]
        public void StartHouse15() {
            FullCalculationStarter.StartHouse(14);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses2)]
        public void StartHouse16() {
            FullCalculationStarter.StartHouse(15);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses2)]
        public void StartHouse17() {
            FullCalculationStarter.StartHouse(16);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses2)]
        public void StartHouse18() {
            FullCalculationStarter.StartHouse(17);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses2)]
        public void StartHouse19() {
            FullCalculationStarter.StartHouse(18);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses2)]
        public void StartHouse20() {
            FullCalculationStarter.StartHouse(19);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses3)]
        public void StartHouse21() {
            FullCalculationStarter.StartHouse(20);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses3)]
        public void StartHouse22() {
            FullCalculationStarter.StartHouse(21);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses3)]
        public void StartHouse23() {
            FullCalculationStarter.StartHouse(22);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses3)]
        public void StartHouse24() {
            FullCalculationStarter.StartHouse(23);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses3)]
        public void StartHouse25() {
            FullCalculationStarter.StartHouse(24);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses3)]
        public void StartHouse26() {
            FullCalculationStarter.StartHouse(25);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses3)]
        public void StartHouse27() {
            FullCalculationStarter.StartHouse(26);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses3)]
        public void StartHouse28() {
            FullCalculationStarter.StartHouse(27);
        }

        [Fact]
        [Category(UnitTestCategories.FullCalcHouses3)]
        public void StartHouse29() {
            FullCalculationStarter.StartHouse(28);
        }

        public HouseTestsShort([NotNull] ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }
    }
}
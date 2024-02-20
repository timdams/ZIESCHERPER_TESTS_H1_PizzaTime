using Microsoft.VisualStudio.TestTools.UnitTesting;
using PizzaConsole;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PizzaTimeTest
{
    [TestClass]
    public class PizzaBasisTest
    {


        [TestMethod, Description("Controleert of full prop Toppings aanwezig is")]
        public void ToppingsTest()
        {

            var r = new Pizza();

            bool hasBackfield = r.GetType().GetField("toppings", BindingFlags.Instance | BindingFlags.NonPublic)!= null;
            Assert.AreEqual(true, hasBackfield, "Geen instantie variabele toppings gevonden");

            bool hasProp = r.GetType().GetProperty("Toppings") != null;
            Assert.AreEqual(true, hasProp, "Geen property Toppings gevonden");
            if(hasProp)
            {

                //TODO ZOVEEL HERHALING...HET IS BESCHAMEND
                string starttop = "ananas";
                r.GetType().GetProperty("Toppings").SetValue(r, starttop);
                var backfieldWaarde = r.GetType().GetField("toppings", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(r);
                Assert.AreEqual(backfieldWaarde, starttop, "De instantievariabele krijgt niet de waarde die aan de Toppings property werd meegegeven");
                var propwaarde = r.GetType().GetProperty("Toppings").GetValue(r);
                Assert.AreEqual(propwaarde, starttop, "Get van Toppings geeft niet de waarde terug die met de Set werd ingesteld");
                r.GetType().GetProperty("Toppings").SetValue(r, "");
                Assert.AreEqual(r.GetType().GetProperty("Toppings").GetValue(r), starttop, "Een lege string als topping geven geeft niet gewenste resultaat");
            }

            var isInt = r.GetType().GetProperty("Toppings").GetMethod.ReturnType;
            Assert.AreEqual(typeof(string), isInt, "Property Toppings niet van type string");

            var isAutoProp = IsAutoProp(r.GetType().GetProperty("Toppings"));
            Assert.AreEqual(isAutoProp, false, "Property Toppings is zo te zien een autoprop en geen full prop.");

            

        }

        [TestMethod, Description("Controleert of full prop Price aanwezig is")]
        public void PriceTest()
        {

            var r = new Pizza();

            bool hasBackfield = r.GetType().GetField("price", BindingFlags.Instance | BindingFlags.NonPublic) != null;
            Assert.AreEqual(true, hasBackfield, "Geen instantie variabele price gevonden");

            bool hasProp = r.GetType().GetProperty("Price") != null;
            Assert.AreEqual(true, hasProp, "Geen property Price gevonden");
            if (hasProp)
            {

                //TODO ZOVEEL HERHALING...HET IS BESCHAMEND
                double starttop = 66.68;
                r.GetType().GetProperty("Price").SetValue(r, starttop);
                var backfieldWaarde = r.GetType().GetField("price", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(r);
                Assert.AreEqual(backfieldWaarde, starttop, "De instantievariabele krijgt niet de waarde die aan de Price property werd meegegeven");
                var propwaarde = r.GetType().GetProperty("Price").GetValue(r);
                Assert.AreEqual(propwaarde, starttop, "Get van Price geeft niet de waarde terug die met de Set werd ingesteld");
                r.GetType().GetProperty("Price").SetValue(r, -2.5);
                Assert.AreEqual(r.GetType().GetProperty("Price").GetValue(r), starttop, "Een negatieve waarde als price geven geeft niet gewenste resultaat");
            }

            var isInt = r.GetType().GetProperty("Price").GetMethod.ReturnType;
            Assert.AreEqual(typeof(double), isInt, "Property Price niet van type double");

            var isAutoProp = IsAutoProp(r.GetType().GetProperty("Price"));
            Assert.AreEqual(isAutoProp, false, "Property Price is zo te zien een autoprop en geen full prop.");
        }


        [TestMethod, Description("Controleert of full prop Diameter aanwezig is")]
        public void DiameterTest()
        {

            var r = new Pizza();

            bool hasBackfield = r.GetType().GetField("diameter", BindingFlags.Instance | BindingFlags.NonPublic) != null;
            Assert.AreEqual(true, hasBackfield, "Geen instantie variabele diameter gevonden");

            bool hasProp = r.GetType().GetProperty("Diameter") != null;
            Assert.AreEqual(true, hasProp, "Geen property Diameter gevonden");
            if (hasProp)
            {

                //TODO ZOVEEL HERHALING...HET IS BESCHAMEND
                int starttop = 66;
                r.GetType().GetProperty("Diameter").SetValue(r, starttop);
                var backfieldWaarde = r.GetType().GetField("diameter", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(r);
                Assert.AreEqual(backfieldWaarde, starttop, "De instantievariabele krijgt niet de waarde die aan de Diameter property werd meegegeven");
                var propwaarde = r.GetType().GetProperty("Diameter").GetValue(r);
                Assert.AreEqual(propwaarde, starttop, "Get van Diameter geeft niet de waarde terug die met de Set werd ingesteld");
                r.GetType().GetProperty("Diameter").SetValue(r, -2);
                Assert.AreEqual(r.GetType().GetProperty("Diameter").GetValue(r), starttop, "Een negatieve waarde als diameter geven geeft niet gewenste resultaat");
            }

            var isInt = r.GetType().GetProperty("Diameter").GetMethod.ReturnType;
            Assert.AreEqual(typeof(int), isInt, "Property Diameter niet van type double");

            var isAutoProp = IsAutoProp(r.GetType().GetProperty("Diameter"));
            Assert.AreEqual(isAutoProp, false, "Property Diameter is zo te zien een autoprop en geen full prop.");
        }

        // Bron: https://stackoverflow.com/questions/2210309/how-to-find-out-if-a-property-is-an-auto-implemented-property-with-reflection
        public bool IsAutoProp(PropertyInfo info)
        {
            bool mightBe = info.GetGetMethod()
                               .GetCustomAttributes(
                                   typeof(CompilerGeneratedAttribute),
                                   true
                               )
                               .Any();
            if (!mightBe)
            {
                return false;
            }


            bool maybe = info.DeclaringType
                             .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                             .Where(f => f.Name.Contains(info.Name))
                             .Where(f => f.Name.Contains("BackingField"))
                             .Where(
                                 f => f.GetCustomAttributes(
                                     typeof(CompilerGeneratedAttribute),
                                     true
                                 ).Any()
                             )
                             .Any();

            return maybe;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CSharp7NewFeatures
{
    public partial class CSharp7Features : Form
    {
        public CSharp7Features()
        {
            InitializeComponent();
        }

        #region Out Parameter Enhancements

        void InitializeValue(out int number)
        {
            number = 1;
        }
        private void btnOutC6_Click(object sender, EventArgs e)
        {
            //Need to declare outParameter before it can be passed as argument
            int outParameter;
            InitializeValue(out outParameter);
            Console.WriteLine("Value of outParameter is {0}", outParameter);

        }

        private void btnOutC7_Click(object sender, EventArgs e)
        {
            //out can be declared inside the agument 
            InitializeValue(out int outParameter);
            Console.WriteLine("Value of outParameter is {0}", outParameter);

            //Common scenario where this feature becomes handy
            string someNumberBoxedIntoString = "23";
            if (int.TryParse(someNumberBoxedIntoString, out int someValue))
            {
                //If value is parsed used here 
                someValue = 0;
            }
            //You can still use the variable here ..just like old times 
            var someValueAssigment = someValue;
        }


        #endregion

        #region Tuples

        //C# 6.0 Style
        Tuple<int, int> ReturnRuntimeTimeEncapsulatingTwoValues()
        {
            return new Tuple<int, int>(1, 2);
        }

        private void btnTuple6_Click(object sender, EventArgs e)
        {
            Tuple<int, int> tuple = ReturnRuntimeTimeEncapsulatingTwoValues();
            //Usage in c# 6
            int value1 = tuple.Item1; //No control over member name and its too unintuitive
            int value2 = tuple.Item2;

        }
        //C# 7.0 Style
        (int x, int y) ReturnRuntimeTimeEncapsulatingTwoValuesCSharp7Style()
        {
            return (1, 2);
        }
        private void btnCsharp7_Click(object sender, EventArgs e)
        {
            (int x, int y) tuple = ReturnRuntimeTimeEncapsulatingTwoValuesCSharp7Style();
            //Usage in c# 7
            int value1 = tuple.x; //Full control over member name and its too intuitive
            int value2 = tuple.y;

            //Another way to define tuple
            (int x, int y) anotherTuple = (x: 20, y: 12); //Named parameter syntax

            //Unfortunately this is not allowed
            (int x, int y) anotherTupleWithDifferentOrder = (y: 20, x: 12); //Named parameter syntax

            //Direct depackaging or deconstruction
            (int x, int y) = ReturnRuntimeTimeEncapsulatingTwoValuesCSharp7Style();
            Console.WriteLine(x);
            Console.WriteLine(y);

            //Custom Depacking for types
            var complexNumber = new ComplexNumber(2, 3);          

            (float real, float imaginary) = complexNumber;
            float realValue = real;
            float imaginaryValue = imaginary;

            Tuple<float, float> tupleValue = complexNumber.Deconstruction();
            Console.WriteLine(tupleValue.Item1);
            Console.WriteLine(tupleValue.Item2);
        }



        #endregion


        /// <summary>
        /// Returns a collection of mixed objects which are not related and have differnt properties
        /// </summary>
        /// <returns></returns>
        IEnumerable<object> CreateTestData()
        {
            yield return new PatternMatching.FirstType() { Number = 1 };
            yield return new PatternMatching.SecondType() { Number2 = 1 };
            yield return new PatternMatching.FirstType() { Number = 1 };
            yield return new PatternMatching.SecondType() { Number2 = 1 };
        }

        private void btnPatternMatchingC6_Click(object sender, EventArgs e)
        {
            var sum = 0;
            //Get Mixed Data
            foreach (var item in CreateTestData())
            {
                //Check if item is of type patternmatching.firsttype
                if (item is PatternMatching.FirstType)
                {
                    //If yes cast to appropriate object to get access to property
                    sum += (item as PatternMatching.FirstType).Number;
                }
                else if (item is PatternMatching.SecondType)
                {
                    //If yes cast to appropriate object to get access to property
                    sum += (item as PatternMatching.SecondType).Number2;
                }
            }
        }

        private void btnPatternMatchingC7_Click(object sender, EventArgs e)
        {
            var sum = 0;
            //Pattern matching with If statement and is keyword
            foreach (var item in CreateTestData())
            {
                if (item is PatternMatching.FirstType firstType)
                {
                    sum += firstType.Number;
                }
                else if (item is PatternMatching.SecondType secondType)
                {
                    sum += secondType.Number2;
                }
            }

            sum = 0;
            //Pattern matching with switch statement and When
            foreach (var item in CreateTestData())
            {
                switch (item)
                {
                    case PatternMatching.FirstType firstTypeUsingSwitch when firstTypeUsingSwitch.Number > 0:
                        sum += firstTypeUsingSwitch.Number;
                        break;
                    case PatternMatching.FirstType firstTypeUsingSwitch:
                        sum += firstTypeUsingSwitch.Number;
                        break;
                    case PatternMatching.SecondType secondTypeUsingSwitch:
                        sum += secondTypeUsingSwitch.Number2;
                        break;
                    case null:
                        break;
                    default:
                        break;
                }
                if (item is PatternMatching.FirstType firstType)
                {
                    sum += firstType.Number;
                }
                else if (item is PatternMatching.SecondType secondType)
                {
                    sum += secondType.Number2;
                }
            }

            //When Keyword
        }


        #region Expression Bodied Members

        public string ReadOnlyProperty => "This is an expression bodied property introduced in C# 6";

        public string TestMethod() => "This is an expression bodied method introduced in C# 6";


        public CSharp7Features(string someData) => Console.WriteLine("Constructor called with {0} in c# 7 feature", someData);

        ~CSharp7Features() => Console.WriteLine("Finalizer called using C# 7 feature");

        float someDataMember;
        public float SomeDataMember { get => someDataMember; set => someDataMember = value; }

        #endregion
        private void btnCsharp6ExpressionBodiedMembers_Click(object sender, EventArgs e)
        {
        }

        #region Throw Expressions

        object Getdata()
        {
            //Fetch data from server or database and if data is null return null
            return null;//
        }

        private void btnThrowExpressionAlternateC6_Click(object sender, EventArgs e)
        {
            var data = Getdata();
            if (data == null)
                throw new Exception("No Data found");

        }

        private void btnThrowExpressionAlternateC7_Click(object sender, EventArgs e)
        {
            //Short and Concise . We cannot do that in C# 6 since ?? operator requires types on left hand side and right hand 
            //side to be identical but with expressions this is supported
            var data = Getdata() ?? throw new Exception("No Data found");
        }


        #endregion

        private void btnLiteralsCSharp7_Click(object sender, EventArgs e)
        {
            int number = 123456; //old style

            number = 123_456; //C# 7.0 style seperator

            var binaryNumber = 0b1000_0001_1100_0111_1110_1000_1100;
            var hexNumber = 0x12_d6_87; 
            double realNumber = 2_000.311_1e-1_000;

            Console.WriteLine(number);//Prints 123456 ignoring _
            Console.WriteLine(binaryNumber);//Prints 136085132 ignoring _
            Console.WriteLine(hexNumber);//Prints 1234567 ignoring _
            Console.WriteLine(realNumber);//Prints 0 ignoring _

        }

        private void btnReturnLocalAsRef_Click(object sender, EventArgs e)
        {
            int[] numbers = new[] { 1, 2, 3, 4, 5, 6 };

            try
            {
                int index = numbers.FindReferenceOfNumber(4); //Get Index of number so that we can change value later
                index = 111;//No change in value since destination is not a ref type

                Console.WriteLine("Value at index 3 is {0}", numbers[3]);//Output: Value at index 3 is 4

                //Correct Usage to store returning refs
                ref int refIndex =ref numbers.FindReferenceOfNumber(4); //Get Index of number so that we can change value later
                refIndex = 111;//No change in value since destination is not a ref type
                Console.WriteLine("Value at index 3 is {0}", numbers[3]);//Output: Value at index 3 is 111

            }
            catch (Exception)
            {
                //Do nothing
            }
        }

        private void btnPassVariableAsRefCsharp6_Click(object sender, EventArgs e)
        {
            int[] numbers = new[]{ 1, 2, 3, 4, 5, 6};

            try
            {
                int index = numbers.FindIndexOfNumber(4); //Get Index of number so that we can change value later
                numbers[index] = 111;//change value at returned index
            }
            catch (Exception)
            {
                //Do nothing
            }
            Console.WriteLine("Value at index 3 is {0}", numbers[3]);//Output : Value at index 3 is 111

        }

        private void btnLocalFunctionsCSharp6_Click(object sender, EventArgs e)
        {
            int[] numbers = System.Linq.Enumerable.Range(1, 100).ToArray();

            //We need to find Sum and Subtract for first 100 natural numbers
            //Sum
            var sum = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sum = sum + i;
            }


            //Subtraction
            var subtraction = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                subtraction = subtraction - i;
            }

            //After first level refactoring
            Func<int[],Func<long,int,long>, long> aggregator = (nums,action) =>
             {
                 long output = 0;
                 for (int i = 0; i < nums.Length; i++)
                 {
                     output = action(output,i);
                 }
                 return output;
             };
            //Sum
            var sum2 = aggregator(numbers, (i, j) => i + j); // 2 late binding calls .One for aggregator and one for logic to be passed to aggregator
            //Subtraction
            var subtraction2 = aggregator(numbers, (i, j) => i - j);

        }
        public int Fibonacci(int x)
        {
            if (x < 0) throw new ArgumentException("Less negativity please!", nameof(x));
            return Fib(x).current;

            (int current, int previous) Fib(int i)
            {
                if (i == 0) return (1, 0);
                var (p, pp) = Fib(i - 1);
                return (p + pp, p);
            }
        }
        private void btnLocalFunctionsCsharp7_Click(object sender, EventArgs e)
        {
            int[] numbers = System.Linq.Enumerable.Range(1, 100).ToArray();
            
            //Sum
            var sum = Aggregator(numbers, (i, j) => i + j);// 1 late binding call .Only one for logic to be passed to aggregator
            //Subtraction
            var subtraction = Aggregator(numbers, (i, j) => i - j);

            //With Local functions
            long Aggregator(int[] nums, Func<long, int, long> action)
            {
                long output = 0;
                for (int i = 0; i < nums.Length; i++)
                {
                    output = action(output, i);
                }
                return output;
            };

            var sum2 = Aggregator2(numbers, (i, j) => i + j).output; //Returned Tuple and accessed output value in current scope

            //With Local functions
            (long output,int something) Aggregator2(int[] nums2, Func<long, int, long> action2)
            {
                long output = 0;
                for (int i = 0; i < nums2.Length; i++)
                {
                    output = action2(output, i);
                }
                return (output,0);
            };
        }
        public int SomeLongRunningMethod()
        {
            System.Threading.Tasks.Task.Delay(1000);
            return 10;
        }
               

        int data = -1;
        public async System.Threading.Tasks.Task<int> SomeLongRunningMethodAsync()
        {            
           await System.Threading.Tasks.Task.Delay(1000);
           data = 10;
                        
           return data;
        }

        private void CSharp7Features_Load(object sender, EventArgs e)
        {

        }

        async System.Threading.Tasks.Task<int> GetDataFromCacheOrServer()
        {
            return data == -1 ? await SomeLongRunningMethodAsync() : 10;
            
        }

        private void btnAsyncValueTypeSupport_Click(object sender, EventArgs e)
        {
           
        }
    }

    public static class NumberExtensions
    {
        public static int FindIndexOfNumber(this int[] numbers, int numberToSearch)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == numberToSearch)
                {
                    return i;
                }
            }
            throw new ArgumentException(string.Format("Number {0} not found", numberToSearch));
        }

        public static ref int FindReferenceOfNumber(this int[] numbers, int numberToSearch)
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                if (numbers[i] == numberToSearch)
                {
                    return ref numbers[i];
                }
            }
            throw new ArgumentException(string.Format("Number {0} not found", numberToSearch));
        }
    }

}

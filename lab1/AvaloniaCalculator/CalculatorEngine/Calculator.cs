using System.Reflection;

namespace Calculator
{

	using System;

	public class CalcEngine
	{
		//
		// Operation Constants.
		//
		public enum Operator:int
		{
			eUnknown = 0,
			eAdd = 1,
			eSubtract = 2,
			eMultiply = 3,
			eDivide = 4,
			ePower = 5
		}

		//
		// Module-Level Constants
		//

		private static double negativeConverter = -1;
		private static string versionInfo = "Calculator v3.0.1.1";

		//
		// Module-level Variables.
		//

		private static double numericAnswer;
		private static string stringAnswer;
		private static Operator calcOperation;
		private static double firstNumber;
		private static double secondNumber;
		private static bool secondNumberAdded;
		private static bool decimalAdded;

		//
		// Class Constructor.
		//

		public CalcEngine ()
		{
			decimalAdded = false;
			secondNumberAdded = false;
		}

		//
		// Returns the custom version string to the caller.
		//

		public static string GetVersion ()
		{
			return (versionInfo);
		}
		//
		// Called when the Date key is pressed.
		//

		public static string GetDate ()
		{
			DateTime curDate = new DateTime();
			curDate = DateTime.Now;

			stringAnswer = String.Concat (curDate.ToShortDateString(), " ", curDate.ToLongTimeString());

			return (stringAnswer);
		}

		//
		// Called when a number key is pressed on the keypad.
		//

		public static string CalcNumber (string KeyNumber)
		{
			stringAnswer = stringAnswer + KeyNumber;
			return (stringAnswer);
		}

		//
		// Called when an operator is selected (+, -, *, /, ^)
		//

		public static void CalcOperation (Operator calcOper)
		{
			if (stringAnswer != "" && !secondNumberAdded)
			{
				firstNumber = System.Convert.ToDouble (stringAnswer);
				calcOperation = calcOper;
				stringAnswer = "";
				decimalAdded = false;
			}
		}

		//
		// Called when the +/- key is pressed.
		//

		public static string CalcSign ()
		{
			double numHold;

			if (stringAnswer != "")
			{
				numHold = System.Convert.ToDouble (stringAnswer);
				stringAnswer = System.Convert.ToString(numHold * negativeConverter);
			}

			return (stringAnswer);
		}

		//
		// Called when the . key is pressed.
		//

		public static string CalcDecimal ()
		{
			if (!decimalAdded && !secondNumberAdded)
			{
				if (stringAnswer != "")
					stringAnswer = stringAnswer + ".";
				else
					stringAnswer = "0.";

				decimalAdded = true;
			}

			return (stringAnswer);
		}

		//
		// Called when = is pressed.
		//

		public static string CalcEqual ()
		{
			bool validEquation = false;

			if (stringAnswer != "")
			{
				secondNumber = System.Convert.ToDouble (stringAnswer);
				secondNumberAdded = true;

				switch (calcOperation)
				{
					case Operator.eUnknown:
						validEquation = false;
						break;

					case Operator.eAdd:
						numericAnswer = firstNumber + secondNumber;
						validEquation = true;
						break;

					case Operator.eSubtract:
						numericAnswer = firstNumber - secondNumber;
						validEquation = true;
						break;

					case Operator.eMultiply:
						numericAnswer = firstNumber * secondNumber;
						validEquation = true;
						break;

					case Operator.eDivide:
						numericAnswer = firstNumber / secondNumber;
						validEquation = true;
						break;

					case Operator.ePower:
						numericAnswer = Math.Pow(firstNumber, secondNumber);
						validEquation = true;
						break;

					default:
						validEquation = false;
						break;
				}

				if (validEquation)
					stringAnswer = System.Convert.ToString (numericAnswer);
			}

			return (stringAnswer);
		}

		//
		// Квадратный корень (sqrt)
		//
		public static string CalcSqrt()
		{
			if (stringAnswer != "")
			{
				double num = System.Convert.ToDouble(stringAnswer);
				if (num >= 0)
				{
					numericAnswer = Math.Sqrt(num);
					stringAnswer = System.Convert.ToString(numericAnswer);
				}
				else
				{
					stringAnswer = "Error";
				}
			}
			return stringAnswer;
		}

		//
		// Обратное число (1/x)
		//
		public static string CalcReciprocal()
		{
			if (stringAnswer != "")
			{
				double num = System.Convert.ToDouble(stringAnswer);
				if (num != 0)
				{
					numericAnswer = 1.0 / num;
					stringAnswer = System.Convert.ToString(numericAnswer);
				}
				else
				{
					stringAnswer = "Error: div by 0";
				}
			}
			return stringAnswer;
		}

		//
		// Квадрат числа (x^2)
		//
		public static string CalcSquare()
		{
			if (stringAnswer != "")
			{
				double num = System.Convert.ToDouble(stringAnswer);
				numericAnswer = num * num;
				stringAnswer = System.Convert.ToString(numericAnswer);
			}
			return stringAnswer;
		}

		//
		// Факториал (n!)
		//
		public static string CalcFactorial()
		{
			if (stringAnswer != "")
			{
				int n = System.Convert.ToInt32(System.Convert.ToDouble(stringAnswer));
				if (n < 0)
				{
					stringAnswer = "Error";
					return stringAnswer;
				}
				long result = 1;
				for (int i = 2; i <= n; i++)
					result *= i;
				numericAnswer = result;
				stringAnswer = System.Convert.ToString(numericAnswer);
			}
			return stringAnswer;
		}

		//
		// Кубический корень
		//
		public static string CalcCubeRoot()
		{
			if (stringAnswer != "")
			{
				double num = System.Convert.ToDouble(stringAnswer);
				numericAnswer = Math.Cbrt(num);
				stringAnswer = System.Convert.ToString(numericAnswer);
			}
			return stringAnswer;
		}

		//
		// Решение квадратного уравнения ax^2 + bx + c = 0
		//
		public static string SolveQuadratic(double a, double b, double c)
		{
			double discriminant = b * b - 4 * a * c;
			if (discriminant < 0)
				return "Нет действительных корней (D < 0)";
			else if (discriminant == 0)
			{
				double x = -b / (2 * a);
				return $"x = {x:F4}";
			}
			else
			{
				double x1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
				double x2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
				return $"x1 = {x1:F4}, x2 = {x2:F4}";
			}
		}

		//
		// Resets the various module-level variables for the next calculation.
		//

		public static void CalcReset ()
		{
			numericAnswer = 0;
			firstNumber = 0;
			secondNumber = 0;
			stringAnswer = "";
			calcOperation = Operator.eUnknown;
			decimalAdded = false;
			secondNumberAdded = false;
		}
	}
}

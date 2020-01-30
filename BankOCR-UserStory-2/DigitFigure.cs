using System;
using System.Collections.Generic;
using System.Linq;

namespace Battousai999.CodeDojoKata.BankOcr.UserStory2
{
	public struct DigitFigure : IEquatable<DigitFigure>
	{
		//  0     _ 
		// 123   |_|
		// 456   |_|
		// 
		// 0 = 2^0 + 2^1 + 2^3 + 2^4 + 2^5 + 2^6		= 0b1111011
		// 1 = 2^3 + 2^6								= 0b1001000
		// 2 = 2^0 + 2^2 + 2^3 + 2^4 + 2^5				= 0b0111101
		// 3 = 2^0 + 2^2 + 2^3 + 2^5 + 2^6				= 0b1101101
		// 4 = 2^1 + 2^2 + 2^3 + 2^6					= 0b1001110
		// 5 = 2^0 + 2^1 + 2^2 + 2^5 + 2^6				= 0b1100111
		// 6 = 2^0 + 2^1 + 2^2 + 2^4 + 2^5 + 2^6		= 0b1110111
		// 7 = 2^0 + 2^3 + 2^6							= 0b1001001
		// 8 = 2^0 + 2^1 + 2^2 + 2^3 + 2^4 + 2^5 + 2^6 	= 0b1111111 
		// 9 = 2^0 + 2^1 + 2^2 + 2^3 + 2^5 + 2^6		= 0b1101111
		// 

		private static Dictionary<byte, int> binaryEncodings = new Dictionary<byte, int>
		{
			[(byte)0b1111011] = 0,
			[(byte)0b1001000] = 1,
			[(byte)0b0111101] = 2,
			[(byte)0b1101101] = 3,
			[(byte)0b1001110] = 4,
			[(byte)0b1100111] = 5,
			[(byte)0b1110111] = 6,
			[(byte)0b1001001] = 7,
			[(byte)0b1111111] = 8,
			[(byte)0b1101111] = 9
		};

		public int BinaryValue { get; private set; }
		public int? DigitValue { get; private set; }

		public bool IsValidDigit => DigitValue != null;

		public DigitFigure(byte value)
		{
			this.BinaryValue = value;

			if (binaryEncodings.TryGetValue(value, out var digit))
				this.DigitValue = digit;
			else
				this.DigitValue = null;
		}

		public char ToChar()
		{
			if (DigitValue == null)
				return '?';

			return DigitValue.ToString().Reverse().First();
		}

		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is DigitFigure otherFigure))
				return false;

			return this.Equals(otherFigure);
		}

		public override int GetHashCode()
		{
			return BinaryValue.GetHashCode();
		}

		public static bool operator ==(DigitFigure left, DigitFigure right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(DigitFigure left, DigitFigure right)
		{
			return !(left == right);
		}

		public bool Equals(DigitFigure other)
		{
			return (other.BinaryValue == this.BinaryValue);
		}
	}
}

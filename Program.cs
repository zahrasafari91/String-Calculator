using System;
using System.Collections.Generic;
using System.Text;

namespace Assignment1
{
    // TODO Add supporting classes here

    public class Program
    {
        public static string ProcessCommand(string input)
        {
            try
            {
                // TODO Evaluate the expression and return the result
                char[] c = input.ToCharArray();

                //stack for operands(numbers)
                Stack<float> num = new Stack<float>();

                //stack for operators
                Stack<char> op = new Stack<char>();

                for (int i = 0; i < c.Length; i++)
                {
                    //current character is a whitespace
                    if (c[i] == ' ')
                    {
                        continue;
                    }

                    //current character is number
                    else if (c[i] >= '0' && c[i] <= '9')
                    {
                        
                        StringBuilder sb = new StringBuilder();
                       
                        while (i < c.Length && ((c[i] <= '9' && c[i] >= '0') || (c[i] == '.')))
                            sb.Append(c[i++]);

                        
                        num.Push(float.Parse(sb.ToString()));
                        i--;
                    }

                    else if (c[i] == '(')
                    {
                        op.Push(c[i]);
                    }

                    else if (c[i] == ')')
                    {
                        while (op.Peek() != '(')
                        {
                            num.Push(ApplyOperatorToOperands(op.Pop(), num.Pop(), num.Pop()));
                        }
                        op.Pop();
                    }

                    else if (c[i] == '+' || c[i] == '-' || c[i] == '*' || c[i] == '/')
                    {
                        while (op.Count > 0 && opPrecedence(c[i], op.Peek()))
                        {
                            num.Push(ApplyOperatorToOperands(op.Pop(), num.Pop(), num.Pop()));
                            if (op.Peek() == '/' && c[i] == '-')
                            {
                                op.Pop();
                                num.Push(ApplyOperatorToOperandsNeg(c[i], num.Pop()));
                                c[i] = '/';
                            }

                        }
                        op.Push(c[i]);
                    }
                     
                        
  
 

                }

                // pop all the operators from the stack 
                while (op.Count > 0)
                {
                    num.Push(ApplyOperatorToOperands(op.Pop(), num.Pop(), num.Pop()));
                }

                return num.Pop().ToString();
            }

            catch (Exception e)
            {
                return "Error evaluating expression: " + e;
            }
        }

       

        public static float ApplyOperatorToOperands(char op, float num1, float num2)
        {
            switch (op)
            {
                case '+':
                    return num1 + num2;
                case '-':
                    return num1 - num2;
                case '*':
                    return num1 * num2;
                case '/':
                    if (num2 == 0)
                    {
                        throw new System.NotSupportedException("Cannot divide by zero");
                    }
                    return num1 / num2;
            }
            return 0;
        }

        public static float ApplyOperatorToOperandsNeg(char op, float a)
        {
            switch (op)
            {
                case '+':
                    return a;
                case '-':
                    return -a;
                case '*':
                    return a * 1;
                case '/':
                    return a / 1;
            }
            return 0;
        }

        //when operator2 has higher or same precedence as operator1 returns true
        public static bool opPrecedence(char op1, char op2)
        {

            if (op2 == '(' || op2 == ')')
            {
                return false;
            }
            if ((op1 == '*' || op1 == '/') && (op2 == '+' || op2 == '-'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }




        static void Main(string[] args)
        {
            string input;
            while ((input = Console.ReadLine()) != "exit")
            {
                Console.WriteLine(ProcessCommand(input));
            }
        }
    }
}


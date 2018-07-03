﻿// --- Logger.cs ---
//
// MIT License
//
// Copyright (c) 2018 Aaron Spindler - aaron@xnovax.net
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.IO;

namespace Poseidon
{
    public static class Logger
    {
        static StreamWriter writer;
        static string fileName;

        /// <summary>
        /// Initialize this instance.
        /// </summary>
        public static void Initialize()
        {
            System.IO.Directory.CreateDirectory("Logs");
            var now = DateTime.Now.Ticks;
            fileName = string.Format(@"Logs/{0}.txt", now);
        }

        /// <summary>
        /// Writes the input to the console and to a file
        /// </summary>
        /// <param name="input">Input.</param>
        public static void WriteLine(string input)
        {
            Console.WriteLine(input);

            try{
                writer = new StreamWriter(fileName, true);
                writer.WriteLine(input);
                writer.Close();
            }catch(Exception e){
                Console.WriteLine("Logger Error: " + e.Message);
            }
        }


    }
}

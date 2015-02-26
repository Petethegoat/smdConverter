using System;
using System.IO;
using System.Text.RegularExpressions;

namespace S2GSMDC
{
	class Program
	{
		static void Main(string[] args)
		{
			if(args.Length < 1)
			{
				Console.WriteLine("Either drag and drop a file onto this exe, or use it from the command line with the files to convert as arguments.\nPress any key to exit.\n");
				Console.Read();
			}
			else
			{
				Console.WriteLine("{0} files to convert.\nPress any key to begin conversion.\n", args.Length);
				Console.Read();

				//this is dumb
				/*
				bool sd2ldConversion = false;
				foreach(string a in args)
				{
					if(a == "-ld")
					{
						sd2ldConversion = true;
						break;
					}
				}
				*/

                int successes = 0;
				foreach(string file in args)
				{
					Console.WriteLine("Opening {0} for conversion...\n", file);

					using(StreamReader r = new StreamReader(file))
					{
						String output = file;
						output = output.Insert(output.IndexOf("."), "_fixed");
						using(StreamWriter w = new StreamWriter(output))
						{
							string line;
							while((line = r.ReadLine()) != null)
							{
								//this is dumb
								/*
								line = line.Trim();
								if(sd2ldConversion)
								{
									if(line == "end")
									{
										sd2ldConversion = false;
									}
									else
									Match latch = Regex.Match(line, @"(^.+ )\""(.+)\""(\-*.+)$");
									if(latch.Success)
									{
										switch(latch.Groups[2].Value)
										{
											case "Bip01 L Arm":
												line = latch.Groups[1] + "Bip01 L Clavicle" + latch.Groups[3];
												break;
											case "Bip01 L Arm1":
												line = latch.Groups[1] + "Bip01 L Clavicle" + latch.Groups[3];
												break;
											case "Bip01 L Arm2":
												line = latch.Groups[1] + "Bip01 L Clavicle" + latch.Groups[3];
												break;
										}
									}
									else
									{
										sd2ldConversion = false;
									}
								}
								*/
								//Match match = Regex.Match(line, @"0 (-*\d+\.?\d+ -*\d+\.?\d+ -*\d+\.?\d+ -*\d+\.?\d+ -*\d+\.?\d+ -*\d+\.?\d+ -*\d+\.?\d+ -*\d+\.?\d+) \d+ (\d+) .*$");
                                Match match = Regex.Match(line, @"\d+(?<vertex> +-?\d+\.\d+ +-?\d+\.\d+ +-?\d+\.\d+ +-?\d+\.\d+ +-?\d+\.\d+ +-?\d+\.\d+ +-?\d+\.\d+ +-?\d+\.\d+) +\d+ +(?<bone>\d+) +-?\d+\.\d+");
                                {
									if(match.Success)
									{
										line = match.Groups["bone"].Value + match.Groups["vertex"].Value;
                                        successes++;
									}
								}
								w.WriteLine(line);
							}
						}
						Console.WriteLine("The converted file is located at {0}\n", output);
					}
				}
				Console.WriteLine("All files converted.\nPress any key to exit.");  // {0} successes, {1} failures.", successes, failures);
                Console.WriteLine(successes + " successes.");
				//Console.WriteLine("Converting {0} failed.\n", file);
				Console.Read();
				Console.Read();
			}
		}
	}
}

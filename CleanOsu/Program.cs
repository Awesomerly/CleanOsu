using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace CleanOsu
{
	public class Program
	{
		static void Main()
		{
			string text = Program.FindOsuPath();
			if (text.Length < 3)
			{
				Console.WriteLine("Osu! not found.");
				Console.Read();
				return;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(text, "Songs"));
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Program.FillDictionary(dictionary);
			foreach (DirectoryInfo directoryInfo2 in directories)
			{
				if (directoryInfo2.LastWriteTime > Properties.Settings.Default.lastRun)
				{
					DirectoryInfo[] directories2 = directoryInfo2.GetDirectories();
					foreach (DirectoryInfo directoryInfo3 in directories2)
					{
						Console.WriteLine("Deleting: " + directoryInfo3.FullName);
						directoryInfo3.Delete(true);
					}
					List<string> list = (from file in Directory.GetFiles(directoryInfo2.FullName, "*.*")
										 where file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("png") || file.ToLower().EndsWith("wav")
										 select file).ToList<string>();
					using (List<string>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string path = enumerator.Current;
							if (dictionary.Keys.Any((string e) => path.Contains(e)))
							{
								Console.WriteLine("Deleting: " + path);
								File.Delete(path);
							}
							else if (path.EndsWith("jpg"))
							{
								if (!Program.GetMd5HashFromFile(path).SequenceEqual("d91761440f94a81bb12819ade7581f0b"))
								{
									Console.WriteLine("Replacing: " + path);
									Program.ExtractEmbeddedResource(path, Assembly.GetExecutingAssembly().GetName().Name, "BG.jpg");
								}
							}
							else if (path.EndsWith("png") && !Program.GetMd5HashFromFile(path).SequenceEqual("d91761440f94a81bb12819ade7581f0b"))
							{
								Console.WriteLine("Replacing: " + path);
								Program.ExtractEmbeddedResource(path, Assembly.GetExecutingAssembly().GetName().Name, "BG.png");
							}
						}
					}
				}
			}
			Properties.Settings.Default.lastRun = DateTime.Now;
			Properties.Settings.Default.Save();
			Console.WriteLine("Done");
		}

		static string FindOsuPath()
		{
			try
			{
				RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey("Applications\\osu!.exe\\shell\\open\\command");
				if (registryKey == null)
				{
					registryKey = Registry.ClassesRoot.OpenSubKey("osustable.File.osk\\DefaultIcon");
				}
				if (registryKey != null)
				{
					object value = registryKey.GetValue(null);
					if (value != null)
					{
						Regex regex = new Regex("(?<=\")[^\\\"]*(?=\")");
						return Path.GetDirectoryName(regex.Match(value.ToString()).ToString());
					}
				}
			}
			catch (Exception)
			{
				return "";
			}
			return "";
		}

		static void FillDictionary(Dictionary<string, string> dic)
		{
			dic.Add("approachcircle.", "approachcircle.");
			dic.Add("comboburst", "comboburst");
			dic.Add("count1.", "count1.");
			dic.Add("count2.", "count2.");
			dic.Add("count3.", "count3.");
			dic.Add("cursor.", "cursor.");
			dic.Add("cursortrail.", "cursortrail.");
			dic.Add("default-", "default-");
			dic.Add("fail-", "fail-");
			dic.Add("Folder.", "Folder.");
			dic.Add("followpoint", "followpoint");
			dic.Add("go.", "go.");
			dic.Add("hit0.", "hit0.");
			dic.Add("hit50.", "hit50.");
			dic.Add("hit50k.", "hit50k.");
			dic.Add("hit100.", "hit100.");
			dic.Add("hit100k.", "hit100k.");
			dic.Add("hit300.", "hit300.");
			dic.Add("hit300g.", "hit300g.");
			dic.Add("hit300k.", "hit300k.");
			dic.Add("hitclap", "hitclap.");
			dic.Add("hitfinish", "hitfinish.");
			dic.Add("hitnormal", "hitnormal.");
			dic.Add("hitwhistle", "hitwhistle.");
			dic.Add("hitcircle.", "hitcircle.");
			dic.Add("hitcircleoverlay.", "hitcircleoverlay.");
			dic.Add("lighting.", "lighting.");
			dic.Add("normal-", "normal-");
			dic.Add("pause-", "pause-");
			dic.Add("play-", "play-");
			dic.Add("ready.", "ready.");
			dic.Add("reversearrow.", "reversearrow.");
			dic.Add("scorebar-", "scorebar-");
			dic.Add("soft-", "soft-");
			dic.Add("sliderb.", "sliderb.");
			dic.Add("sliderb0.", "sliderb0.");
			dic.Add("sliderb1.", "sliderb1.");
			dic.Add("sliderb2.", "sliderb1.");
			dic.Add("sliderb3.", "sliderb3.");
			dic.Add("sliderb4.", "sliderb4.");
			dic.Add("sliderb5.", "sliderb5.");
			dic.Add("sliderb6.", "sliderb6.");
			dic.Add("sliderb7.", "sliderb7.");
			dic.Add("sliderb8.", "sliderb8.");
			dic.Add("sliderb9.", "sliderb9.");
			dic.Add("sliderb10.", "sliderb10.");
			dic.Add("sliderb11.", "sliderb11.");
			dic.Add("sliderb12.", "sliderb12.");
			dic.Add("sliderb13.", "sliderb13.");
			dic.Add("sliderb14.", "sliderb14.");
			dic.Add("sliderb15.", "sliderb15.");
			dic.Add("sliderb16.", "sliderb16.");
			dic.Add("sliderb17.", "sliderb17.");
			dic.Add("sliderb18.", "sliderb18.");
			dic.Add("sliderb19.", "sliderb19.");
			dic.Add("sliderb20.", "sliderb20.");
			dic.Add("sliderb21.", "sliderb21.");
			dic.Add("sliderb22.", "sliderb22.");
			dic.Add("sliderb23.", "sliderb23.");
			dic.Add("sliderfollowcircle-", "sliderfollowcircle-");
			dic.Add("sliderpoint", "sliderpoint");
			dic.Add("sliderslide", "sliderslide");
			dic.Add("slidertick", "slidertick");
			dic.Add("sliderwhistle", "sliderwhistle");
			dic.Add("sliderendcircle", "sliderendcircle");
			dic.Add("sliderstartcircle", "sliderstartcircle");
			dic.Add("spinnerbonus", "spinnerbonus");
			dic.Add("score-", "score-");
			dic.Add("section-", "section-");
			dic.Add("spinner-", "spinner-");
			dic.Add("star.", "star.");
			dic.Add("star2.", "star2.");
			dic.Add("sliderfollowcircle.", "sliderfollowcircle.");
			dic.Add("sliderscorepoint.", "sliderscorepoint.");
			dic.Add("screenoverlay.", "screenoverlay.");
			dic.Add("taiko.", "taiko.");
		}

		static void ExtractEmbeddedResource(string outputDir, string resourceLocation, string file)
		{
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceLocation + "." + file))
			{
				using (FileStream fileStream = new FileStream(outputDir, FileMode.Create))
				{
					int num = 0;
					while ((long)num < manifestResourceStream.Length)
					{
						fileStream.WriteByte((byte)manifestResourceStream.ReadByte());
						num++;
					}
				}
			}
		}

		static string GetMd5HashFromFile(string fileName)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.Open);
			MD5 md5 = MD5.Create();
			byte[] array = md5.ComputeHash(fileStream);
			fileStream.Close();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(i.ToString("x2"));
			}
			return stringBuilder.ToString();
		}
	}
}

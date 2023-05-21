using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SAE_Graphe.Biomes;
using SAE_Graphe.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SAE_Graph.Other {
    public class Files {

        public const string path = "";

        public static bool Create(String fileName) {
			try {
				if(!Directory.Exists(path + fileName)) {
					Directory.CreateDirectory(path + fileName);
				}

				File.Create(fileName);

				return true;
			}
			catch (Exception ex) {
				Debug.WriteLine(ex);
				return false;
			}
		}

        public static bool Delete(String fileName) {
			try {
				File.Delete(path + fileName);
				return true;
			}
			catch (Exception ex) {
				Debug.WriteLine(ex);
				return false;
			}
		}

        public static string[] Read(String fileName) {
			try {

				List<string> datas = new List<string>();
				using (StreamReader sr = File.OpenText(path + fileName)) {
					string ligne;
					while((ligne = sr.ReadLine()) != null) {
						datas.Add(ligne);
					}
				}

				return datas.ToArray();
			}
			catch (Exception ex) {
				Debug.WriteLine(ex);
				return null;
			}
		}

        public static bool Write(String fileName, string[] datas) {
			try {
				using (StreamWriter sw = File.CreateText(path + fileName)) {
					foreach(string data in datas) {
						sw.WriteLine(data + "\n");
					}
				}

				return true;
			}
			catch (Exception ex) {
				Debug.WriteLine(ex);
				return false;
			}
		}

        public static void SaveProject(Map map) {
			using (FileStream stream = File.Create(path + "biomes.json")) {
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Biome>));
				serializer.WriteObject(stream, map.biomes);
			}
			Debug.WriteLine("Projet Saved");
		}

		[DataMember]
		private static DataContractJsonSerializer serializer;
		public static Map LoadProject() {
			Map map = new Map();
			serializer = null;

			using (FileStream stream = File.OpenRead(path + "biomes.json")) {
				serializer = new DataContractJsonSerializer(typeof(List<Biome>));

				map.biomes = serializer.ReadObject(stream) as List<Biome>;
			}

			Debug.WriteLine("Projet Loaded");

			return map;
		}
    }
}

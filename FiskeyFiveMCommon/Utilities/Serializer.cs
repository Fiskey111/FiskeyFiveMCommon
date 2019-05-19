﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CommonClient.Utilities
{
    public static class Serializer
    {
        public static void SaveToNode(string file, string node, string value)
        {
            XmlNode n = SelectNodeFromXml(file, node);

            if (n == null) throw new KeyNotFoundException($"{nameof(SaveToNode)}: specified node does not exists!");

            n.InnerText = value;
            var doc = new XmlDocument();
            doc.Save(file);
        }

        public static string ReadFromNode(string file, string node)
        {
            return SelectNodeFromXml(file, node).InnerText;
        }

        private static XmlNode SelectNodeFromXml(string filePath, string node)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException($"{nameof(SelectNodeFromXml)}(): specified file does not exist: {filePath}");

            using (TextReader reader = new StreamReader(filePath))
            {
                var doc = new XmlDocument();
                doc.Load(reader);
                return doc.SelectSingleNode(node);
            }
        }

        public static List<T> LoadAllXML<T>(string dirPath, SearchOption searchOption = SearchOption.AllDirectories)
        {
            if (!Directory.Exists(dirPath)) throw new DirectoryNotFoundException($"{nameof(LoadAllXML)}(): specified directory could not be found: {dirPath}");

            string[] files = Directory.GetFiles(dirPath, "*.xml", searchOption);

            List<T> result = new List<T>();

            Array.ForEach(files, f => result.AddRange(LoadFromXML<T>(f)));

            return result;
        }

        public static void SaveToXML<T>(List<T> list, string filePath)
        {
            SaveItemToXML(list, filePath);
        }

        public static void SaveItemToXML<T>(T item, string path)
        {
            using (TextWriter writer = new StreamWriter(path))
            {
                new XmlSerializer(typeof(T)).Serialize(writer, item);
            }
        }

        public static T LoadItemFromXML<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(
                    $"{nameof(LoadItemFromXML)}(): specified file does not exist: {filePath}");

            using (TextReader reader = new StreamReader(filePath))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
            }
        }

        public static void ModifyItemInXML<T>(string filePath, Action<T> modification)
        {
            T item = LoadItemFromXML<T>(filePath);
            modification(item);
            SaveItemToXML<T>(item, filePath);
        }

        public static T GetSelectedListElementFromXml<T>(string file, Func<List<T>, T> selector)
        {
            List<T> deserialized = LoadItemFromXML<List<T>>(file);
            return selector(deserialized);
        }

        public static List<T> LoadFromXML<T>(string filePath)
        {
            return LoadItemFromXML<List<T>>(filePath);
        }

        public static void AppendToXML<T>(T objectToAdd, string path)
        {
            ModifyItemInXML<List<T>>(path, t => t.Add(objectToAdd));
        }

        /// <summary>
        /// Creates folder in case it doesn't exist and checks file existance.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>false when a file does not exist.</returns>
        private static bool ValidatePath(string path)
        {
            //TODO: implement
            // - check extension
            // - bool param: createDir
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
                return false;
            }

            return File.Exists(path);
        }
    }
}

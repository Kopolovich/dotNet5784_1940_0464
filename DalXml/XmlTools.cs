﻿namespace Dal;

using DO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

static class XMLTools
{
    const string s_xml_dir = @"..\xml\";
    static XMLTools()
    {
        if (!Directory.Exists(s_xml_dir))
            Directory.CreateDirectory(s_xml_dir);
    }

    #region Extension Fuctions
    public static T? ToEnumNullable<T>(this XElement element, string name) where T : struct, Enum =>
        Enum.TryParse<T>((string?)element.Element(name), out var result) ? (T?)result : null;
    public static DateTime? ToDateTimeNullable(this XElement element, string name) =>
        DateTime.TryParse((string?)element.Element(name), out var result) ? (DateTime?)result : null;
    public static double? ToDoubleNullable(this XElement element, string name) =>
        double.TryParse((string?)element.Element(name), out var result) ? (double?)result : null;
    public static int? ToIntNullable(this XElement element, string name) =>
        int.TryParse((string?)element.Element(name), out var result) ? (int?)result : null;
    #endregion

    #region XmlConfig
    public static int GetAndIncreaseNextId(string data_config_xml, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        int nextId = root.ToIntNullable(elemName) ?? throw new FormatException($"can't convert id.  {data_config_xml}, {elemName}");
        root.Element(elemName)?.SetValue((nextId + 1).ToString());
        XMLTools.SaveListToXMLElement(root, data_config_xml);
        return nextId;
    }

    public static void ResetNextId(string data_config_xml, string elemName)
    {
        int resetValue = 0;
        if (elemName == "NextTaskId")
            resetValue = 1000;
        else
            resetValue = 100;
        XElement root = LoadListFromXMLElement(data_config_xml);
        int nextId = root.ToIntNullable(elemName) ?? throw new FormatException($"can't convert id.  {data_config_xml}, {elemName}");
        root.Element(elemName)?.SetValue((resetValue).ToString());
        SaveListToXMLElement(root, data_config_xml);
    }
    public static void SetStartDate(string data_config_xml, string elemName, DateTime? startDate)
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        if(root.Elements(elemName).Any())
            root.Element(elemName)?.SetValue((startDate).ToString());
        else
            root.Add(new XElement(elemName, startDate));
        XMLTools.SaveListToXMLElement(root, data_config_xml);
    }
    public static DateTime? GetStartDate(string data_config_xml, string elemName)
    {
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        if (!root.Elements(elemName).Any())
            return null;
        DateTime? startDate = root.ToDateTimeNullable(elemName) ?? throw new FormatException($"can't convert start date.  {data_config_xml}, {elemName}");
        return startDate;
    }

    public static void RemoveStartDate(string data_config_xml, string elemName)
    {
        const string s_xml_dir = @"..\xml\";
        string filePath = $"{s_xml_dir + data_config_xml}.xml";
        XElement root = XMLTools.LoadListFromXMLElement(data_config_xml);
        if (root.Element(elemName) != null)
            root.Element(elemName)!.Remove();
        root.Save(filePath);
    }

    #endregion

    #region SaveLoadWithXElement
    public static void SaveListToXMLElement(XElement rootElem, string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            rootElem.Save(filePath);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    public static XElement LoadListFromXMLElement(string entity)
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (File.Exists(filePath))
                return XElement.Load(filePath);
            XElement rootElem = new(entity);
            rootElem.Save(filePath);
            return rootElem;
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    #endregion

    #region SaveLoadWithXMLSerializer
    public static void SaveListToXMLSerializer<T>(List<T> list, string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            new XmlSerializer(typeof(List<T>)).Serialize(file, list);
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to create xml file: {s_xml_dir + filePath}, {ex.Message}");
        }
    }
    public static List<T> LoadListFromXMLSerializer<T>(string entity) where T : class
    {
        string filePath = $"{s_xml_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath)) return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T>));
            return x.Deserialize(file) as List<T> ?? new();
        }
        catch (Exception ex)
        {
            throw new DalXMLFileLoadCreateException($"fail to load xml file: {filePath}, {ex.Message}");
        }
    }
    #endregion
}

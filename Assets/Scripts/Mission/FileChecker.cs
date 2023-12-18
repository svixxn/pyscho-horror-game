using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FileChecker : MonoBehaviour
{
    public string fileName;
    public string numberInputString;
    public MonoBehaviour otherScript;

    private string filePath;
    private FileSystemWatcher fileWatcher;

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);

        // �������� �������� �����
        if (!File.Exists(filePath))
        {
            Debug.LogError($"���� '{fileName}' �� ��������. ���� �� �����: {filePath}");
            return;
        }
        else
        {
            Debug.Log($"���� '{fileName}' ��������. ���� �� �����: {filePath}");
        }

        fileWatcher = new FileSystemWatcher(Path.GetDirectoryName(filePath), fileName);
        fileWatcher.NotifyFilter = NotifyFilters.LastWrite;
        fileWatcher.Changed += OnFileChanged;
        fileWatcher.EnableRaisingEvents = true;

        // ��������� ������� otherScript ��� �����
        //if (otherScript != null)
        //{
        //    otherScript.enabled = false;
        //    Debug.Log("Script = false");
        //}
    }

    private void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        // �������� ��� � ���� ��� ���� ���
        if (e.ChangeType == WatcherChangeTypes.Changed)
        {
            Debug.Log("���� ��� �������");
            ReadFileAndCompare(); // ��������� �������� ��� ��� �����
        }
    }

    private void ReadFileAndCompare()
    {
        if (!string.IsNullOrEmpty(numberInputString))
        {
            if (int.TryParse(numberInputString, out int numberToCompare))
            {
                Debug.Log($"numberInputString: {numberInputString}, numberToCompare: {numberToCompare}");

                try
                {
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (string line in lines)
                    {
                        Debug.Log($"Read line: {line}");

                        if (line.Contains("Exit from the room = "))
                        {
                            int equalIndex = line.IndexOf('=');
                            string numberString = line.Substring(equalIndex + 1).Trim();

                            numberString = numberString.Replace(";", "");

                            if (int.TryParse(numberString, out int numberFromFile))
                            {
                                Debug.Log($"numberFromFile: {numberFromFile}");

                                if (numberToCompare == numberFromFile)
                                {
                                    Debug.Log($"numberToCompare == numberFromFile");
                                    otherScript.enabled = true;
                                    Debug.Log($"Script = true");
                                }
                                else
                                {
                                    Debug.Log($"Numbers do not match: numberToCompare = {numberToCompare}, numberFromFile = {numberFromFile}");
                                }
                            }
                            else
                            {
                                Debug.Log($"Failed to parse numberString: {numberString}");
                            }
                        }
                        else
                        {
                            Debug.Log($"Line does not contain 'Exit from the room = '");
                        }
                    }
                }
                catch (IOException e)
                {
                    Debug.LogError($"Error reading file: {e.Message}");
                }
            }
            else
            {
                Debug.Log($"Failed to parse numberInputString: {numberInputString}");
            }
        }
        else
        {
            Debug.Log($"numberInputString is null or empty");
        }
    }

    private void OnDestroy()
    {
        fileWatcher.Dispose();
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

public class Program
{
    public static float toFloat(string str)
    {
        if (!float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out _))
        {
            return 0.0f;
        }
        return float.Parse(str);
    }

    public static string SplitFilePath(string fullPath)
    {
        string directory = Path.GetDirectoryName(fullPath) ?? "";
        //string filename = Path.GetFileName(fullPath);
        return directory;
    }

    public static Dictionary<string, string> GetSettingsValue(string filePath)
    {
        var keyValuePairs = new Dictionary<string, string>();
        try
        {
            foreach (string line in File.ReadLines(filePath, Encoding.UTF8))
            {
                string trimmedLine = line.Trim();
                // 「キー：値」形式でない行は無視する
                if (trimmedLine.Contains('：'))
                {
                    string[] parts = trimmedLine.Split('：', 2);
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();
                        keyValuePairs[key] = value;
                    }
                    else
                    {
                        Console.WriteLine($"警告: 行 '{trimmedLine}' は予期しない形式です。");
                    }
                }
            }
            Console.WriteLine($"ファイル '{filePath}' からキーと値を抽出しました。");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"エラー: ファイル '{filePath}' が見つかりませんでした。");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"エラーが発生しました: {ex.Message}");
        }
        return keyValuePairs;
    }

    public static List<List<string>> ReadCsv(string filePath)
    {
        var data = new List<List<string>>();
        try
        {
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] values = line.Split(',');
                        if (values[0].All(char.IsDigit)) {
                            data.Add(values.ToList());
                        }
                    }
                }
            }
            Console.WriteLine($"CSVファイル '{filePath}' を読み込みました。");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"エラー: ファイル '{filePath}' が見つかりませんでした。");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"エラーが発生しました: {ex.Message}");
        }
        return data;
    }

    public static void ExportCsv(string filePath, List<List<string>> data)
    {
        try
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8)) // false: 上書き, true: 追記, Encoding.UTF8: 文字エンコーディング
            {
                foreach (var row in data)
                {
                    writer.WriteLine(string.Join(",", row));
                }
            }
            Console.WriteLine($"CSVファイル '{filePath}' を書き出しました。");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"エラーが発生しました: {ex.Message}");
        }
    }

    public static void Main(string[] args)
    {
        Dictionary<string, string> settings = GetSettingsValue("./settings.txt");
        string filePath = args[0]; // 読み込むCSVファイルのパス
        string dir = SplitFilePath(filePath);
        List<List<string>> csvData = ReadCsv(filePath);

        List<List<string>> distData = [
            ["Vocaloid Motion Data 0002"],
            ["変換後モーション"],
            ["Motion", "bone", "x", "y", "z", "rx", "ry", "rz", "x_p1x", "x_p1y", "x_p2x", "x_p2y", "y_p1x", "y_p1y", "y_p2x", "y_p2y", "z_p1x", "z_p1y", "z_p2x", "z_p2y", "r_p1x", "r_p1y", "r_p2x", "r_p2y"]
        ];

        List<List<string>> tempData = new List<List<string>>();



        // 読み込んだデータの表示 (オプション)
        int i = 0;
        foreach (var row in csvData)
        {
            float rx = toFloat(row[6]);
            float ry = toFloat(row[7]);
            float rz = toFloat(row[8]);

            float para;
            List<string> addData;

            //頭
            para = toFloat(settings["頭"]);
            addData = [(i + 7).ToString(), "頭", "0", "0", "0", (rx * para).ToString(), (ry * para).ToString(), (rz * para).ToString(), "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107"];
            tempData.Add(addData);

            //首
            para = toFloat(settings["首"]);
            addData = [(i + 5).ToString(), "首", "0", "0", "0", (rx * para).ToString(), (ry * para).ToString(), (rz * para).ToString(), "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107"];
            tempData.Add(addData);

            //上半身
            para = toFloat(settings["上半身"]);
            addData = [(i + 9).ToString(), "上半身", "0", "0", "0", (rx * para).ToString(), (ry * para).ToString(), (rz * para).ToString(), "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107"];
            tempData.Add(addData);

            //上半身2
            para = toFloat(settings["上半身2"]);
            addData = [(i + 11).ToString(), "上半身2", "0", "0", "0", (rx * para).ToString(), (ry * para).ToString(), (rz * para).ToString(), "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107"];
            tempData.Add(addData);

            //センター
            para = toFloat(settings["センター"]);
            addData = [(i + 14).ToString(), "センター", "0", "0", "0", (rx * para).ToString(), (ry * para).ToString(), (rz * para).ToString(), "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107"];
            tempData.Add(addData);

            //左腕
            para = toFloat(settings["左腕"]);
            addData = [(i + 14).ToString(), "左腕", "0", "0", "0", (-ry * para).ToString(), (-rx * para).ToString(), (-toFloat(settings["腕のZ回転"]) -rz * para).ToString(), "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107"];
            tempData.Add(addData);

            //右腕
            para = toFloat(settings["右腕"]);
            addData = [(i + 14).ToString(), "右腕", "0", "0", "0", (ry * para).ToString(), (rx * para).ToString(), (toFloat(settings["腕のZ回転"]) + rz * para).ToString(), "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107"];
            tempData.Add(addData);

            //左肩
            para = toFloat(settings["左肩"]);
            addData = [(i + 15).ToString(), "左肩", "0", "0", "0", (-ry * para).ToString(), (-rx * para).ToString(), (-rz * para).ToString(), "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107"];
            tempData.Add(addData);

            //右肩
            para = toFloat(settings["右肩"]);
            addData = [(i + 15).ToString(), "右肩", "0", "0", "0", (ry * para).ToString(), (rx * para).ToString(), (rz * para).ToString(), "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107", "20", "20", "107", "107"];
            tempData.Add(addData);

            i++;
        }

        foreach (var row in tempData)
        {
            if (settings["トリミング"] == "ON")
            {
                int row0 = int.Parse(row[0]);
                if (row0 < 19)
                {
                    continue;
                }
                else
                {
                    row[0] = (row0 - 19).ToString();
                }
            }
            distData.Add(row);
        }

        ExportCsv(dir + "/outputdata.csv", distData);
    }
}
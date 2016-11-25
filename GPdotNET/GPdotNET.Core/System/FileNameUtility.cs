using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GPdotNET.Core.System
{
    public static class FileNameUtility
    {
        // Read the contents of a text file from the app’s local folder.
        public static string ReadTextFile(string fullPath)
        {
#if WINDOWS_APP
            string contents;

            StorageFile textFile = StorageFile.GetFileFromPathAsync(fullPath).GetAwaiter().GetResult();;

            using (IRandomAccessStream textStream = textFile.OpenReadAsync().GetAwaiter().GetResult())
            {
                using (DataReader textReader = new DataReader(textStream))
                {
                    uint textLength = (uint)textStream.Size;
                    textReader.LoadAsync(textLength).GetAwaiter().GetResult();
                    contents = textReader.ReadString(textLength);
                }
            }
            return contents;
#else
            string buffer = "";
            // open selected file and retrieve the content
            using (StreamReader reader = File.OpenText(fullPath))
            {
                //read TrainingData in to buffer
                buffer = reader.ReadToEnd();
                reader.DiscardBufferedData();
                //reader.Close();
            }
            return buffer;
#endif
        }
    }
}

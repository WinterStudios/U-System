using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace U_System.API.GitHub
{
    public class Release : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("tag_name")]
        public string Tag { get; set; }
        public bool PreRelease { get; set; }
        public Asset[] Assets { get; set; }


        #region Outside of GitHub

        public bool IsInstall { get; set; }

        public string[] filesLocations { get; set; }
        public string LocalZipFile { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
namespace U_System.API.GitHub.Extensions
{
    internal static class ReleaseExtensios
    {
        internal async static Task<Release[]> GetReleases(this Repository repository)
        {
            return await GitHub.GetReleasesAsync(repository);
        }
        internal static string[] GetTags(this Release[] releases)
        {
            string[] tags = new string[releases.Length];
            for (int i = 0; i < releases.Length; i++)
            {
                tags[i] = releases[i].Tag;
            }
            return tags;
        }
    }
}

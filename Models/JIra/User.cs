using jira2ets.Models.JIra.Work;

namespace jira2ets.Models.JIra
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    public class User    {
        public string self { get; set; } 
        public string key { get; set; } 
        public string name { get; set; } 
        public string emailAddress { get; set; } 
        public AvatarUrls avatarUrls { get; set; } 
        public string displayName { get; set; } 
        public bool active { get; set; } 
        public string timeZone { get; set; } 
        public string locale { get; set; } 
        public Groups groups { get; set; } 
        public ApplicationRoles applicationRoles { get; set; } 
        public string expand { get; set; } 
    }


}